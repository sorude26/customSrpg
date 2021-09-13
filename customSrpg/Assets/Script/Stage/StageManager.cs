using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

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
    [Tooltip("プレイヤーの合計数")]
    [SerializeField] int m_playerNum = 5;
    int m_playerCount = 0;
    /// <summary> プレイヤーの全ユニット </summary>
    List<PlayerUnit> m_players;
    /// <summary> 友軍の全ユニット </summary>
    NpcUnit[] m_allies;
    [SerializeField] SortieUnits m_alliesData;
    /// <summary> 敵の全ユニット </summary>
    NpcUnit[] m_enemys;
    [SerializeField] SortieUnits m_enemysData;
    /// <summary> ステージ上の全ユニット </summary>
    List<Unit> m_units;
    [SerializeField] StageUnitCreater m_unitCreater;
    [SerializeField] StageMassage m_massage;
    public StageMassage Massage { get => m_massage; }
    [SerializeField] CursorControl m_cursor;
    [SerializeField] StageUI m_uI;
    [SerializeField] GameObject m_targetMark;
    BattleManager m_battleManager;
    MapData[] m_startData;
    MapData[] m_mapDatas;
    MapData[] m_attackDatas;
    bool m_gameEnd;
    [SerializeField] Transform m_stage;
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
        m_players = new List<PlayerUnit>();
        m_startData = MapManager.Instance.GetArea(0, 4, 0, 4).Where(p => p.MapType != MapType.NonAggressive).ToArray();
        foreach (var mapData in m_startData)
        {
            mapData.StagePanel.ViewStartPanel();
        }        
        StartSet();
        FadeController.Instance.StartFadeIn();
        GameScene.InputManager.Instance.OnInputDecision += UnitSet;
    }
    void GameStart()
    {
        m_players.ForEach(p =>
        {
            m_units.Add(p);
            m_battleManager.BattleEnd += p.ActionEnd;
        });
        m_units.ForEach(u => u.GetUnitData().OnDamage += BattleManager.Instance.BattleTargetDataView);
        Turn = TurnState.Player;
        m_players.ForEach(p => p.WakeUp());
        m_allies.ToList().ForEach(a => a.WakeUp());
        GameScene.InputManager.Instance.OnInputDecision -= UnitSet;
        NextUnit();
    }
    void StartSet()
    {
        m_units.ForEach(u => u.StartSet());
        m_mapDatas = MapManager.Instance.GetOutArea(0, 4, 0, 4).Where(p => p.MapType != MapType.NonAggressive).ToArray();
        for (int i = 0; i < m_mapDatas.Length; i++)
        {
            int r = UnityEngine.Random.Range(0, m_mapDatas.Length);
            MapData map = m_mapDatas[i];
            m_mapDatas[i] = m_mapDatas[r];
            m_mapDatas[r] = map;
        }
        m_allies = m_unitCreater.StageUnitCreate(m_mapDatas, m_alliesData,0, m_stage);
        m_enemys = m_unitCreater.StageUnitCreate(m_mapDatas, m_enemysData, m_alliesData.AllUnitNumber, m_stage);
        m_allies.ToList().ForEach(a => m_units.Add(a));
        m_enemys.ToList().ForEach(e => m_units.Add(e));
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
                PlayerUnit player = m_players.Where(p => p.State == UnitState.StandBy).FirstOrDefault();
                SetNextUnit(player);
                break;
            case TurnState.Allies:
                NpcUnit ally = m_allies.Where(a => a.State == UnitState.StandBy).FirstOrDefault();
                SetNextUnit(ally);
                break;
            case TurnState.Enemy:
                NpcUnit enemy = m_enemys.Where(e => e.State == UnitState.StandBy).FirstOrDefault();
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
                m_players.ForEach(p => p.TurnEnd());
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
                m_players.ForEach(p => p.WakeUp());
                m_allies.ToList().ForEach(a => a.UnitRest());
                StartCoroutine(StageMassage(0, NextUnit));
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
    IEnumerator StageMassage(uint massageNum, Action action)
    {
        bool view = true;
        while (view)
        {
            yield return m_massage.View(massageNum);
            view = false;
        }
        action?.Invoke();
    }
    IEnumerator LastStageMassage(uint massageNum)
    {
        bool view = true;
        while (view)
        {
            yield return m_massage.LastMessageView(massageNum);
            view = false;
        }
        yield return new WaitForSeconds(1f);
        FadeController.Instance.StartFadeOut(() => { SceneManager.LoadScene("CustomizeScene"); });
    }
    public void TestEnemyTurn()
    {
        Turn = TurnState.Player;
        m_players.ForEach(p => p.WakeUp());
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
    public void PointUnitSet(int x, int z)
    {
        var area = m_startData.Where(p => p.PosX == x && p.PosZ == z).FirstOrDefault();
        if (area == null || !area.StagePanel.StartMode)
        {
            return;
        }
          var unit = m_unitCreater.PlayerCreate(MapManager.Instance[x, z], UnitDataMaster.PlayerUnitBuildDatas[m_playerCount],
            GameManager.Instanse.GetColor(UnitDataMaster.PlayerColors[m_playerCount]), this.transform);
        m_players.Add(unit);
        m_playerCount++;
        area.StagePanel.CloseStartPanel();
        if (m_playerCount >= m_playerNum)
        {
            EventManager.GameStart();
            StartCoroutine(StageMassage(4, GameStart));
        }
    }
    void UnitSet()
    {
        PointUnitSet(m_cursor.CurrentPosX, m_cursor.CurrentPosZ);
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
        StartCoroutine(LastStageMassage(2));
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
        m_cursor.Warp(TurnUnit);
        StartCoroutine(LastStageMassage(3));
        StartCoroutine(m_cursor.Camera.PointFocus());
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
        m_units.Where(mu => mu.State != UnitState.Destory && MapManager.Instance.GetPosition(mu.CurrentPosX, mu.CurrentPosZ) == p).FirstOrDefault();
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
