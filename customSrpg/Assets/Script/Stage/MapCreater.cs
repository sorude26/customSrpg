using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地形の生成を行う
/// </summary>
public class MapCreater : MonoBehaviour
{
    /// <summary> 地形に合わせたオブジェクトのPrefab </summary>
    [SerializeField] StagePanel[] m_stagePanels;
    [SerializeField] GameObject[] m_roads;
    [SerializeField] int[] m_vRoadPattern = { 1, 2, 0, 2, 1, 0, 0, 0, 1 };
    [SerializeField] int[] m_hRoadPattern = { 1, 1, 0, 0, 2, 2, 2, 0, 0 };
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
        CreateRoad(maxX, maxZ, mapDates, mapScale);
        foreach (var item in mapDates)
        {
            if (item.MapType != MapType.Road)
            {
                item.SetMapType(MapType.Normal);
            }
        }
        CreateBuilding(maxX, maxZ, mapDates, mapScale);
        return mapDates;
    }

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
    void CreateRoad(int maxX, int maxZ, MapData[] datas, int mapScale)
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
    void CreateBuilding(int maxX, int maxZ,MapData[] datas, int mapScale)
    {
        for (int i = 0; i < datas.Length; i++)
        {
            if(BuildCheck(maxX, maxZ, datas, i, mapScale))
            {
                i++;
            }
        }
    }
    bool BuildCheck(int maxX, int maxZ, MapData[] datas,int point,int mapScale)
    {
        for (int h = 0; h < 2; h++)
        {
            for (int v = 0; v < 2; v++)
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
        Instantiate(m_building[0]).transform.position = new Vector3(datas[point].PosX * mapScale, 0, datas[point].PosZ * mapScale);       
        for (int h = 0; h < 2; h++)
        {
            for (int v = 0; v < 2; v++)
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
