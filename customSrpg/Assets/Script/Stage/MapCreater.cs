using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地形の生成を行う
/// </summary>
public class MapCreater : MonoBehaviour
{
    /// <summary> 地形に合わせたオブジェクトのPrefab </summary>
    [SerializeField] GameObject[] m_stagePrefabs;
    /// <summary>
    /// 平らな地形を生成しデータを返す
    /// </summary>
    /// <param name="maxX"></param>
    /// <param name="maxZ"></param>
    /// <returns></returns>
    public MapData[] MapCreate(int maxX, int maxZ,Transform parent,int mapScale)
    {
        MapData[] mapDates = new MapData[maxX * maxZ];
        for (int i = 0; i < maxZ; i++)
        {
            for (int j = 0; j < maxX; j++)
            {
                MapData map = new MapData(MapType.Normal, j, i, 0);
                mapDates[j + i * maxX] = map;
                GameObject mapPanel = Instantiate(m_stagePrefabs[0]);
                mapPanel.transform.position = new Vector3(j * mapScale, 0, i * mapScale);
                mapPanel.transform.SetParent(parent);
            }
        }
        return mapDates;
    }
}
