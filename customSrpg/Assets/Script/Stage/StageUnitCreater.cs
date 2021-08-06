using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ステージにユニットを生成する
/// </summary>
[CreateAssetMenu]
public class StageUnitCreater : ScriptableObject
{
    [SerializeField] NpcUnit m_unitModelPrefab;
    public NpcUnit[] StageUnitCreate(MapData[] positions, SortieUnits units,int start)
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
                unit.Add(CreateUnit(positions[i], units.BuildPattern[k], units.ColorPattern[k]));
                i++;
            }
        }
        return unit.ToArray();
    }
    NpcUnit CreateUnit(MapData pos,UnitBuildData buildData, Color color)
    {
        var unit = Instantiate(m_unitModelPrefab);
        unit.StartSet(new Vector2Int(pos.PosX, pos.PosZ), buildData, color);
        return unit;
    }
}
