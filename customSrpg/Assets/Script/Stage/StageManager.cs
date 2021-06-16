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
    MapData[] m_mapDatas;
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
        m_testUnit.TargetPositionMoveStart(x, z);
    }
    public void OnClickMoveSearch()
    {
        m_mapDatas = MapManager.Instance.StartSearch(m_testUnit);
        foreach (var panel in m_mapDatas)
        {
            panel.StagePanel.ViewMovePanel();
        }
    }
}
