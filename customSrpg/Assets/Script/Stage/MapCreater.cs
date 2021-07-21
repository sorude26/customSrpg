using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地形の生成を行う
/// </summary>
[CreateAssetMenu]
public class MapCreater : ScriptableObject
{
    [Tooltip("地形に合わせたオブジェクトのPrefab")]
    [SerializeField] StagePanel[] m_stagePanels;
    [Tooltip("道路のPrefab")]
    [SerializeField] GameObject[] m_roads;
    [Tooltip("縦側の生成する道のパターン")]
    [SerializeField] int[] m_vRoadPattern = { 1, 2, 0, 2, 1, 0, 0, 0, 1 };
    [Tooltip("横側の生成する道のパターン")]
    [SerializeField] int[] m_hRoadPattern = { 1, 1, 0, 0, 2, 2, 2, 0, 0 };
    [Tooltip("ビルのPrefab")]
    [SerializeField] GameObject[] m_building;
    /// <summary>
    /// 平らな地形を生成しデータを返す
    /// </summary>
    /// <param name="maxX"></param>
    /// <param name="maxZ"></param>
    /// <returns></returns>
    public MapData[] MapCreate(int maxX, int maxZ, Transform parent, int mapScale)
    {
        ShuffleRoadPattern();
        MapData[] mapDates = new MapData[maxX * maxZ];
        for (int i = 0; i < maxZ; i++)
        {
            for (int j = 0; j < maxX; j++)
            {
                float level = 0f;                
                StagePanel mapPanel = Instantiate(m_stagePanels[0]);
                mapPanel.transform.position = new Vector3(j * mapScale, level, i * mapScale);
                mapPanel.transform.SetParent(parent);
                MapData map = new MapData(MapType.Normal, j, i, level, mapPanel);
                mapDates[j + i * maxX] = map;
            }
        }
        CreateRoad(maxX, maxZ, mapDates, mapScale,parent);
        foreach (var item in mapDates)
        {
            if (item.MapType != MapType.Road)
            {
                item.SetMapType(MapType.Normal);
            }
        }
        CreateBuilding(maxX, maxZ, mapDates, mapScale,parent);
        return mapDates;
    }
    /// <summary>
    /// 道のパターンをシャッフルする
    /// </summary>
    void ShuffleRoadPattern()
    {
        for (int i = 0; i < m_vRoadPattern.Length; i++)
        {
            int r = Random.Range(0, m_vRoadPattern.Length);
            int p = m_vRoadPattern[i];
            m_vRoadPattern[i] = m_vRoadPattern[r];
            m_vRoadPattern[r] = p;
        }
        for (int k = 0; k < m_hRoadPattern.Length; k++)
        {
            int r = Random.Range(0, m_hRoadPattern.Length);
            int p = m_hRoadPattern[k];
            m_hRoadPattern[k] = m_hRoadPattern[r];
            m_hRoadPattern[r] = p;
        }
    }
    /// <summary>
    /// 道を生成する
    /// </summary>
    /// <param name="maxX"></param>
    /// <param name="maxZ"></param>
    /// <param name="datas"></param>
    /// <param name="mapScale"></param>
    void CreateRoad(int maxX, int maxZ, MapData[] datas, int mapScale, Transform parent)
    {
        int roadCount = 0;
        for (int i = Random.Range(0, 3); i < maxZ - 1; i++)
        {
            if (roadCount >= m_hRoadPattern.Length - 1)
            {
                break;
            }
            for (int v = 0; v < maxX; v++)
            {
                var road = Instantiate(m_roads[m_hRoadPattern[roadCount]]);
                road.transform.position = new Vector3(v * mapScale, 0, i * mapScale);
                road.transform.rotation = Quaternion.Euler(0, 270, 0);
                road.transform.SetParent(parent);
                for (int c = 0; c < m_hRoadPattern[roadCount] + 2; c++)
                {
                    if (i + c < maxZ)
                    {
                        datas[v + maxX * (c + i)].SetMapType(MapType.Road);
                    }
                }
            }
            i += (m_hRoadPattern[roadCount] + 1) * 3;
            roadCount++;
        }
        roadCount = 0;
        for (int i = Random.Range(0, 3); i < maxX - 1; i++)
        {
            if (roadCount >= m_vRoadPattern.Length - 1)
            {
                break;
            }
            for (int h = 0; h < maxX; h++)
            {
                var road = Instantiate(m_roads[m_vRoadPattern[roadCount]]);
                road.transform.position = new Vector3(i * mapScale, 0, h * mapScale);
                road.transform.SetParent(parent);
                for (int c = 0; c < m_vRoadPattern[roadCount] + 2; c++)
                {
                    if (i + c < maxZ)
                    {
                        datas[c + i + maxX * h].SetMapType(MapType.Road);
                    }
                }
            }
            i += (m_vRoadPattern[roadCount] + 1) * 3;
            roadCount++;
        }
    }
    /// <summary>
    /// 空地に場所に建造物を建てる
    /// </summary>
    /// <param name="maxX"></param>
    /// <param name="maxZ"></param>
    /// <param name="datas"></param>
    /// <param name="mapScale"></param>
    void CreateBuilding(int maxX, int maxZ,MapData[] datas, int mapScale, Transform parent)
    {
        for (int i = 0; i < datas.Length; i++)
        {
            if (BuildCheck(maxX, maxZ, datas, i, mapScale, 2,parent))
            {
                continue;
            }
        }
    }
    /// <summary>
    /// 空地に対応する建造物を生成する
    /// </summary>
    /// <param name="maxX"></param>
    /// <param name="maxZ"></param>
    /// <param name="datas"></param>
    /// <param name="point"></param>
    /// <param name="mapScale"></param>
    /// <returns></returns>
    bool BuildCheck(int maxX, int maxZ, MapData[] datas,int point,int mapScale,int size, Transform parent)
    {
        for (int h = 0; h < size; h++)
        {
            for (int v = 0; v < size; v++)
            {
                if (point + h + maxZ * v < maxX * maxZ)
                {
                    if (datas[point + h + maxZ * v].MapType != MapType.Normal)
                    {
                        return false;
                    }
                }
            }
        }
        int r = Random.Range(0, m_building.Length);
        var building = Instantiate(m_building[r]);
        building.transform.position = new Vector3(datas[point].PosX * mapScale, 0, datas[point].PosZ * mapScale);
        building.transform.SetParent(parent);
        for (int h = 0; h < size; h++)
        {
            for (int v = 0; v < size; v++)
            {
                if (point + h + maxZ * v < maxX * maxZ)
                {
                    datas[point + h + maxZ * v].SetMapType(MapType.NonAggressive);
                }
            }
        }
        return true;
    }
}
