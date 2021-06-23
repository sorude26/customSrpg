using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 現在のステージの進行を管理するクラス
/// </summary>
public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }
    [SerializeField] Unit m_testUnit;
    [SerializeField] Unit[] m_testEnemys;
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
        foreach (var item in m_testEnemys)
        {
            m_units.Add(item);
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
        foreach (var panel in m_mapDatas)
        {
            panel.StagePanel.ViewMovePanel();
        }
    }
    /// <summary>
    /// 攻撃範囲を検索し表示する
    /// </summary>
    public void AttackSearch(int x, int z)
    {
        m_battleManager.SetAttacker(m_testUnit);
        EventManager.AttackSearchEnd();
        m_attackDatas = MapManager.Instance.StartSearch(x, z, m_testWeapon);
        foreach (var panel in m_attackDatas)
        {
            panel.StagePanel.ViewAttackPanel();
        }
        m_battleManager.SetAttackTargets();
    }

    /// <summary>
    /// 指定箇所のユニットを返す
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public Unit GetPositionUnit(int x, int z)
    {
        return m_units.Where(mu => !mu.DestoryBody).Where(mx => mx.CurrentPosX == x).Where(mz => mz.CurrentPosZ == z).FirstOrDefault(); 
    }
    /// <summary>
    /// 現在の全ユニットを返す
    /// </summary>
    /// <returns></returns>
    public Unit[] GetStageUnits()
    {
        return m_units.Where(mu => !mu.DestoryBody).ToArray();
    }
    /// <summary>
    /// 攻撃範囲を返す
    /// </summary>
    /// <returns></returns>
    public MapData[] GetAttackTarget()
    {
        return m_attackDatas;
    }

}
