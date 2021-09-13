using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ステージにユニットを生成する
/// </summary>
[CreateAssetMenu]
public class StageUnitCreater : ScriptableObject
{
    [SerializeField] PlayerUnit m_playerUnitPrefab;
    [SerializeField] NpcUnit m_unitModelPrefab;
    public NpcUnit[] StageUnitCreate(MapData[] positions, SortieUnits units,int start,Transform parent)
    {
        List<NpcUnit> unit = new List<NpcUnit>();
        int i = start;       
        for (int k = 0; k < units.SortiePattern.Length; k++)
        {
            for (int a = 0; a < units.SortiePattern[k]; a++)
            {
                if (i >= positions.Length)
                {
                    break;
                }
                unit.Add(CreateUnit(positions[i], units.SoriteData(k), units.ColorPattern[k], parent));
                i++;
            }
        }
        return unit.ToArray();
    }
    NpcUnit CreateUnit(MapData pos,UnitBuildData buildData, Color color,Transform parent)
    {
        var unit = Instantiate(m_unitModelPrefab);
        unit.StartSet(new Vector2Int(pos.PosX, pos.PosZ), buildData, color);
        unit.transform.SetParent(parent);
        return unit;
    }
    public PlayerUnit PlayerCreate(MapData pos,UnitBuildData buildData, Color color, Transform parent)
    {
        var unit = Instantiate(m_playerUnitPrefab);
        unit.StartSet(new Vector2Int(pos.PosX, pos.PosZ), buildData, color);
        unit.transform.SetParent(parent);
        return unit;
    }
}
