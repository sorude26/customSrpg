using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// ターンの状態
/// </summary>
public enum TurnState
{
    Player,
    Allies,
    Enemy,
    End,
}

/// <summary>
/// 現在のステージの全体を管理するクラス
/// </summary>
public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }
    /// <summary> 現在のターン </summary>
    public TurnState Turn { get; private set; }
    /// <summary> 行動中のユニット </summary>
    public Unit TurnUnit { get; private set; }
    [Tooltip("プレイヤーの全ユニット")]
    [SerializeField] PlayerUnit[] m_players;
    [Tooltip("友軍の全ユニット")]
    [SerializeField] NpcUnit[] m_allies;
    [Tooltip("敵の全ユニット")]
    [SerializeField] NpcUnit[] m_enemys;
    /// <summary> ステージ上の全ユニット </summary>
    List<Unit> m_units;
    [SerializeField] StageMassage m_massage;
    [SerializeField] CursorControl m_cursor;
    [SerializeField] StageUI m_uI;
    [SerializeField] GameObject m_targetMark;
    [SerializeField] WeaponMaster m_testWeapon;
    BattleManager m_battleManager;
    MapData[] m_mapDatas;
    MapData[] m_attackDatas;
    bool m_gameEnd;
    /// <summary> ステージに付随するカーソル </summary>
    public CursorControl Cursor { get => m_cursor; }
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        m_battleManager = BattleManager.Instance;
        m_units = new List<Unit>();
        m_players.ToList().ForEach(p => 
        { 
            m_units.Add(p);
            m_battleManager.BattleEnd += p.ActionEnd;
        });
        m_allies.ToList().ForEach(a => m_units.Add(a));
        m_enemys.ToList().ForEach(e => m_units.Add(e));
        m_units.ForEach(u => u.StartSet());
        m_allies.ToList().ForEach(a => a.GetUnitData().UnitColorChange(Color.blue));
        m_enemys.ToList().ForEach(e => e.GetUnitData().UnitColorChange(Color.red));
        m_players.ToList().ForEach(p => p.WakeUp());
        m_units.ForEach(u => u.GetUnitData().OnDamage += BattleManager.Instance.BattleTargetDataView);
    }
    /// <summary>
    /// 各ユニットの行動終了時に呼ばれ、次のユニットを登録する
    /// </summary>
    public void NextUnit()
    {
        CheckGameEnd();
        if (m_gameEnd)
        {
            return;
        }
        switch (Turn)
        {
            case TurnState.Player:
                PlayerUnit unit = m_players.Where(p => p.State == UnitState.StandBy).FirstOrDefault();
                SetNextUnit(unit);
                break;
            case TurnState.Allies:
                NpcUnit ally = m_allies.Where(a => a.State == UnitState.StandBy).FirstOrDefault();
                SetNextUnit(ally);
                break;
            case TurnState.Enemy:
                NpcUnit enemy = m_enemys.Where(p => p.State == UnitState.StandBy).FirstOrDefault();
                SetNextUnit(enemy);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 次の行動ユニットを設定する、存在しない場合ターンを終了する
    /// </summary>
    /// <param name="unit"></param>
    void SetNextUnit(Unit unit)
    {
        if (!unit)
        {
            TurnEnd();
        }
        else
        {
            TurnUnit = unit;
            unit.StartUp();
            m_cursor.Warp(unit);
            m_cursor.CursorStop();
            BattleManager.Instance.SetAttacker(TurnUnit);
        }
    }
    /// <summary>
    /// ターン終了処理
    /// </summary>
    public void TurnEnd()
    {
        switch (Turn)
        {
            case TurnState.Player:
                Turn = TurnState.Allies;
                m_allies.ToList().ForEach(a => a.WakeUp());
                m_targetMark.SetActive(false);
                NextUnit();
                break;
            case TurnState.Allies:
                Turn = TurnState.Enemy;
                m_players.ToList().ForEach(p => p.TurnEnd());
                m_allies.ToList().ForEach(a => a.TurnEnd());
                m_enemys.ToList().ForEach(p => p.WakeUp());
                StartCoroutine(StageMassage(1, NextUnit));
                break;
            case TurnState.Enemy:
                Turn = TurnState.End;
                m_enemys.ToList().ForEach(p => p.TurnEnd());                
                TurnEnd();
                break;
            case TurnState.End:
                Turn = TurnState.Player;
                m_players.ToList().ForEach(p => p.WakeUp());
                m_allies.ToList().ForEach(a => a.UnitRest());
                StartCoroutine(StageMassage(0, () => NextUnit()));
                break;
            default:
                break;
        }
    }
    public void OpenCommand()
    {
        m_uI.CommandOpen();
    }
    /// <summary>
    /// ステージのメッセージを再生する
    /// </summary>
    /// <param name="massageNum"></param>
    /// <returns></returns>
    IEnumerator StageMassage(uint massageNum,Action action)
    {
        bool view = true;
        while (view)
        {
            yield return m_massage.View(massageNum);
            view = false;
        }
        action?.Invoke();
    }
    IEnumerator LastStageMassage(uint massageNum, Action action)
    {
        bool view = true;
        while (view)
        {
            yield return m_massage.LastMessageView(massageNum);
            view = false;
        }
        action?.Invoke();
    }
    public void TestEnemyTurn()
    {
        Turn = TurnState.Player;
        m_players.ToList().ForEach(p => p.WakeUp());
        m_allies.ToList().ForEach(a => a.WakeUp());
        NextUnit();
    }
    public void PointMove(int x, int z)
    {
        EventManager.AttackSearchEnd();
        if (m_mapDatas == null)
        {
            return;
        }
        var on = m_mapDatas.Where(mx => mx.PosX == x && mx.PosZ == z).FirstOrDefault();
        if (on == null)
        {
            return;
        }
        m_cursor.Warp(x, z);
        TurnUnit.TargetMoveStart(x, z);
        m_targetMark.SetActive(true);
        m_targetMark.transform.position = m_cursor.transform.position;
    }
    /// <summary>
    /// 終了条件の確認、後で勝利・敗北条件で分岐するようにする
    /// </summary>
    private void CheckGameEnd()
    {
        if (PlayerAllDestory())
        {
            return;
        }
        EnemyAllDestory();
    }
    /// <summary>
    /// 終了条件：プレイヤーの全滅
    /// </summary>
    /// <returns></returns>
    private bool PlayerAllDestory()
    {
        foreach (var unit in m_players)
        {
            if (unit.State != UnitState.Destory)
            {
                return false;
            }
        }
        m_gameEnd = true;
        StartCoroutine(LastStageMassage(2, () => Debug.Log("敗北")));
        return true;
    }
    /// <summary>
    /// 終了条件：敵の全滅
    /// </summary>
    /// <returns></returns>
    private bool EnemyAllDestory()
    {
        foreach (var unit in m_enemys)
        {
            if (unit.State != UnitState.Destory)
            {
                return false;
            }
        }
        m_gameEnd = true;
        StartCoroutine(LastStageMassage(3, () => Debug.Log("勝利")));
        return true;
    }
    /// <summary>
    /// 現在の行動中ユニットを行動終了させる
    /// </summary>
    public void UnitActionEnd()
    {
        TurnUnit.UnitRest();
    }
    /// <summary>
    /// 移動範囲を検索し表示する
    /// </summary>
    public void MoveSearch()
    {
        EventManager.StageGuideViewEnd();
        TurnUnit.MoveEnd();
        m_mapDatas = MapManager.Instance.StartSearch(TurnUnit);
        foreach (var mapData in m_mapDatas)
        {
            mapData.StagePanel.ViewMovePanel();
        }
        m_cursor.CursorStart();
        TurnUnit.SetMoveEvent(m_cursor.CursorStop);
        TurnUnit.SetMoveEvent(m_uI.CommandMoveEnd);
    }
    /// <summary>
    /// 攻撃範囲を検索し表示する
    /// </summary>
    public void AttackSearch(int x, int z)
    {
        m_battleManager.SetAttacker(TurnUnit);
        EventManager.AttackSearchEnd();
        m_attackDatas = MapManager.Instance.StartSearch(x, z, m_testWeapon);
        foreach (var target in m_attackDatas)
        {
            target.StagePanel.ViewAttackPanel();
        }
        m_battleManager.SetAttackTargets();
    }
    /// <summary>
    /// 攻撃範囲を検索し表示する
    /// </summary>
    public void AttackSearch(WeaponPosition position)
    {
        m_battleManager.SetAttacker(TurnUnit);
        EventManager.AttackSearchEnd();
        m_attackDatas = MapManager.Instance.StartSearch(TurnUnit.CurrentPosX, TurnUnit.CurrentPosZ, TurnUnit.GetUnitData().GetWeapon(position));
        foreach (var target in m_attackDatas)
        {
            target.StagePanel.ViewAttackPanel();
        }
        m_battleManager.SetAttackTargets();
    }
    /// <summary>
    /// 指定箇所のユニットを返す
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public Unit GetPositionUnit(int p) =>
        m_units.Where(mu => mu.State != UnitState.Destory && MapManager.Instance.GetPosition(mu.CurrentPosX,mu.CurrentPosZ) == p).FirstOrDefault();
    /// <summary>
    /// 指定箇所のユニットを返す
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public Unit GetPositionUnit(int x, int z) => 
        m_units.Where(mu => mu.State != UnitState.Destory && mu.CurrentPosX == x && mu.CurrentPosZ == z).FirstOrDefault();
    /// <summary>
    /// 現在の全ユニットを返す
    /// </summary>
    /// <returns></returns>
    public Unit[] GetStageUnits() => m_units.Where(mu => mu.State != UnitState.Destory).ToArray();
    /// <summary>
    /// 現在のユニットと敵対する全ユニットを返す
    /// </summary>
    /// <returns></returns>
    public Unit[] GetHostileUnits() => m_units.Where(mu => mu.State == UnitState.Stop).ToArray();
    /// <summary>
    /// 攻撃範囲を返す
    /// </summary>
    /// <returns></returns>
    public MapData[] GetAttackTarget() => m_attackDatas;
}
