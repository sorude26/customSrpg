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
    [SerializeField] CursorControl m_cursor;
    [SerializeField] WeaponMaster m_testWeapon;
    MapData[] m_mapDatas;
    MapData[] m_attackDatas;
    private void Awake()
    {
        Instance = this;
    }
    public void PointMoveTest(int x, int z)
    {
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
        m_testUnit.TargetPositionMoveStart(x, z);
    }
    /// <summary>
    /// 移動範囲を検索し表示する
    /// </summary>
    public void MoveSearch()
    {
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
        m_attackDatas = MapManager.Instance.StartSearch(x, z, m_testWeapon);
        foreach (var panel in m_attackDatas)
        {
            panel.StagePanel.ViewAttackPanel();
        }
    }

    /// <summary>
    /// 指定箇所のユニットを返す
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public Unit GetPositionUnit(int x, int z)
    {
        Unit[] units = { m_testUnit };
        return units.ToList().Where(mx => mx.CurrentPosX == x).Where(mz => mz.CurrentPosZ == z).FirstOrDefault(); 
    }
}
