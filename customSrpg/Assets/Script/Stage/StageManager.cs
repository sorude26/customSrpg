﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// ターンの状態
/// </summary>
public enum TurnState
{
    Player,
    Enemy,
    End,
}

/// <summary>
/// 現在のステージのターン進行を管理するクラス
/// </summary>
public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }
    /// <summary> 現在のターン </summary>
    public TurnState Turn { get; private set; }
    /// <summary> 行動中のユニット </summary>
    public Unit TurnUnit { get; private set; }
    [SerializeField] Unit m_testUnit;
    /// <summary> プレイヤーの全ユニット </summary>
    [SerializeField] Unit[] m_players;
    /// <summary> 敵の全ユニット </summary>
    [SerializeField] Unit[] m_enemys;
    /// <summary> ステージ上の全ユニット </summary>
    List<Unit> m_units;
    [SerializeField] CursorControl m_cursor;
    [SerializeField] GameObject m_targetMark;
    [SerializeField] WeaponMaster m_testWeapon;
    [SerializeField] BattleManager m_battleManager;
    MapData[] m_mapDatas;
    MapData[] m_attackDatas;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        m_units = new List<Unit>();
        m_units.Add(m_testUnit);
        m_enemys.ToList().ForEach(e => m_units.Add(e));
    }
    /// <summary>
    /// 各ユニットの行動終了時に呼ばれ、次のユニットを登録する
    /// </summary>
    public void NextUnit()
    {
        Unit unit = null;
        switch (Turn)
        {
            case TurnState.Player:
                unit = m_players.ToList().Where(p => p.State == UnitState.StandBy).FirstOrDefault();
                SetNextUnit(unit);
                break;
            case TurnState.Enemy:
                unit = m_enemys.ToList().Where(p => p.State == UnitState.StandBy).FirstOrDefault();
                SetNextUnit(unit);
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
            TurnUnit.StartUp();
            m_cursor.CursorWarp(TurnUnit.CurrentPosX, TurnUnit.CurrentPosZ);
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
                Turn = TurnState.Enemy;
                Debug.Log("EnemyTurn");
                m_enemys.ToList().ForEach(p => p.WakeUp());
                NextUnit();
                break;
            case TurnState.Enemy:
                Turn = TurnState.End;
                TurnEnd();
                break;
            case TurnState.End:
                Turn = TurnState.Player;
                Debug.Log("PlayerTurn");
                m_players.ToList().ForEach(p => p.WakeUp());
                NextUnit();
                break;
            default:
                break;
        }
    }
    public void TestAttack()
    {
        m_testUnit.MoveSkep();
        AttackSearch(m_testUnit.CurrentPosX, m_testUnit.CurrentPosZ);
    }
    public void TestTargetAttack()
    {
        m_cursor.CursorWarp(m_battleManager.SetTarget(0));
        m_targetMark.transform.position = m_cursor.transform.position;
        m_battleManager.AttackStart(WeaponPosition.Body);
    }
    public void PointMoveTest(int x, int z)
    {
        EventManager.AttackSearchEnd();
        if (m_mapDatas == null)
        {
            return;
        }
        var on = m_mapDatas.ToList().Where(mx => mx.PosX == x).Where(mz => mz.PosZ == z).FirstOrDefault();
        if (on == null)
        {
            return;
        }
        m_cursor.CursorWarp(x, z);
        m_testUnit.TargetMoveStart(x, z);
        m_targetMark.SetActive(true);
        m_targetMark.transform.position = m_cursor.transform.position;
    }
    /// <summary>
    /// 移動範囲を検索し表示する
    /// </summary>
    public void MoveSearch()
    {
        EventManager.StageGuideViewEnd();
        m_testUnit.MoveEnd();
        m_mapDatas = MapManager.Instance.StartSearch(m_testUnit);
        m_mapDatas.ToList().ForEach(p => p.StagePanel.ViewMovePanel());
    }
    /// <summary>
    /// 攻撃範囲を検索し表示する
    /// </summary>
    public void AttackSearch(int x, int z)
    {
        m_battleManager.SetAttacker(m_testUnit);
        EventManager.AttackSearchEnd();
        m_attackDatas = MapManager.Instance.StartSearch(x, z, m_testWeapon);
        m_attackDatas.ToList().ForEach(p => p.StagePanel.ViewAttackPanel());
        m_battleManager.SetAttackTargets();
    }

    /// <summary>
    /// 指定箇所のユニットを返す
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public Unit GetPositionUnit(int p) =>
        m_units.Where(mu => mu.State != UnitState.Destory).Where(u => MapManager.Instance.GetPosition(u.CurrentPosX,u.CurrentPosZ) == p).FirstOrDefault();
    /// <summary>
    /// 指定箇所のユニットを返す
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public Unit GetPositionUnit(int x, int z) => 
        m_units.Where(mu => mu.State != UnitState.Destory).Where(mx => mx.CurrentPosX == x).Where(mz => mz.CurrentPosZ == z).FirstOrDefault();
    /// <summary>
    /// 現在の全ユニットを返す
    /// </summary>
    /// <returns></returns>
    public Unit[] GetStageUnits() => m_units.Where(mu => mu.State != UnitState.Destory).ToArray();
    /// <summary>
    /// 攻撃範囲を返す
    /// </summary>
    /// <returns></returns>
    public MapData[] GetAttackTarget() => m_attackDatas;
}
