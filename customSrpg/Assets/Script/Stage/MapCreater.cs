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
                MapData map = new MapData(MapType.Normal, j, i, j + i * maxX, level, mapPanel);
                mapDates[j + i * maxX] = map;
            }
        }
        return mapDates;
    }
    /// <summary>
    /// 街を生成する
    /// </summary>
    /// <param name="map"></param>
    public void CityCreate(MapManager map)
    {
        CreateRoad(map, map.transform);
        foreach (var point in map.MapDatas)
        {
            if (point.MapType != MapType.Road)
            {
                point.SetMapType(MapType.Normal);
            }
        }
        CreateBuilding(map, map.transform);
        foreach (var point in map.MapDatas)
        {
            if (point.MapType == MapType.Normal)
            {
                point.SetMapType(MapType.Asphalt);
            }
        }
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
    /// <param name="map"></param>
    /// <param name="parent"></param>
    void CreateRoad(MapManager map, Transform parent)
    {
        int roadCount = 0;
        for (int i = Random.Range(0, 3); i < map.MaxZ - 1; i++)
        {
            if (roadCount >= m_hRoadPattern.Length - 1)
            {
                break;
            }
            for (int v = 0; v < map.MaxX; v++)
            {
                var road = Instantiate(m_roads[m_hRoadPattern[roadCount]]);
                road.transform.position = new Vector3(v * map.MapScale, 0, i * map.MapScale);
                road.transform.rotation = Quaternion.Euler(0, 270, 0);
                road.transform.SetParent(parent);
                for (int c = 0; c < m_hRoadPattern[roadCount] + 2; c++)
                {
                    if (i + c < map.MaxZ)
                    {
                        map.MapDatas[v + map.MaxX * (c + i)].SetMapType(MapType.Road);
                    }
                }
            }
            i += (m_hRoadPattern[roadCount] + 1) * 3;
            roadCount++;
        }
        roadCount = 0;
        for (int i = Random.Range(0, 3); i < map.MaxX - 1; i++)
        {
            if (roadCount >= m_vRoadPattern.Length - 1)
            {
                break;
            }
            for (int h = 0; h < map.MaxX; h++)
            {
                var road = Instantiate(m_roads[m_vRoadPattern[roadCount]]);
                road.transform.position = new Vector3(i * map.MapScale, 0, h * map.MapScale);
                road.transform.SetParent(parent);
                for (int c = 0; c < m_vRoadPattern[roadCount] + 2; c++)
                {
                    if (i + c < map.MaxZ)
                    {
                        map.MapDatas[c + i + map.MaxX * h].SetMapType(MapType.Road);
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
    /// <param name="map"></param>
    /// <param name="parent"></param>
    void CreateBuilding(MapManager map, Transform parent)
    {
        for (int i = 0; i < map.MapDatas.Length; i++)
        {
            if (BuildCheck(map,map[i].PosX,map[i].PosZ, 2, 2, parent))
            {
                continue;
            }
        }
    }
    /// <summary>
    /// 空地に対応する建造物を生成する
    /// <summary>
    /// <param name="map"></param>
    /// <param name="startX"></param>
    /// <param name="startZ"></param>
    /// <param name="sizeX"></param>
    /// <param name="sizeZ"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    bool BuildCheck(MapManager map,int startX,int startZ, int sizeX, int sizeZ, Transform parent)
    {
        var area = map.GetArea(startX, sizeX, startZ, sizeZ);
        foreach (var point in area)
        {
            if (point.MapType != MapType.Normal)
            {
                return false;
            }
        }
        int r = Random.Range(0, m_building.Length);
        var building = Instantiate(m_building[r]);
        building.transform.position = new Vector3(map[startX, startZ].PosX * map.MapScale, 0, map[startX, startZ].PosZ * map.MapScale);
        building.transform.SetParent(parent);
        foreach (var point in area)
        {
            point.SetMapType(MapType.NonAggressive);
        }
        return true;
    }
}
