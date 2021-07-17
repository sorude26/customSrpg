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
    int[] m_vRoadPattern = { 1, 2, 0, 2, 1, 0, 0, 0, 1 };
    int[] m_hRoadPattern = { 1, 1, 0, 0, 2, 2, 2, 0, 0 };
    /// <summary>
    /// 平らな地形を生成しデータを返す
    /// </summary>
    /// <param name="maxX"></param>
    /// <param name="maxZ"></param>
    /// <returns></returns>
    public MapData[] MapCreate(int maxX, int maxZ,Transform parent,int mapScale)
    {
        ShuffleRoadPattern();
        MapData[] mapDates = new MapData[maxX * maxZ];
        for (int i = 0; i < maxZ; i++)
        {
            for (int j = 0; j < maxX; j++)
            {
                float level = 0f;
                //if (j==5)
                //{
                //    level = 3;
                //}
                StagePanel mapPanel = Instantiate(m_stagePanels[0]);
                mapPanel.transform.position = new Vector3(j * mapScale, level, i * mapScale);
                mapPanel.transform.SetParent(parent);
                MapData map = new MapData(MapType.Normal, j, i, level, mapPanel);
                mapDates[j + i * maxX] = map;
            }
        }
        CreateRoad(maxX, maxZ, mapDates, mapScale);
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
    void CreateRoad(int maxX,int maxZ, MapData[] datas,int mapScale)
    {
        int roadCount = 0;
        for (int i = Random.Range(0, 6); i < maxZ - 1; i++)
        {
            if (roadCount >= m_hRoadPattern.Length - 1)
            {
                break;
            }
            for (int v = 0; v < maxX; v++)
            {
                var road = Instantiate(m_roads[m_hRoadPattern[roadCount]]);
                var road2 = Instantiate(m_roads[m_hRoadPattern[roadCount]]);
                road.transform.position = new Vector3(v * mapScale, 0, i * mapScale);
                road2.transform.position = new Vector3(v * mapScale + mapScale, 0, i * mapScale);
                //Debug.Log(i * mapScale);
                road.transform.rotation = Quaternion.Euler(0, 270, 0);
                road2.transform.rotation = Quaternion.Euler(0, 270, 0);
                for (int c = 0; c < m_hRoadPattern[roadCount]; c++)
                {
                    if (i + c < maxZ)
                    {
                        datas[i + c + maxX * v].SetMapType(MapType.Road);
                    }
                }
            }
            i += (m_hRoadPattern[roadCount] + 1) * 2;
            roadCount++;
        }
        roadCount = 0;
        for (int i = Random.Range(0, 6); i < maxX - 1; i++)
        {
            if (roadCount >= m_vRoadPattern.Length - 1)
            {
                break;
            }
            for (int h = 0; h < maxX; h++)
            {
                var road = Instantiate(m_roads[m_vRoadPattern[roadCount]]);
                var road2 = Instantiate(m_roads[m_vRoadPattern[roadCount]]);
                road.transform.position = new Vector3(i * mapScale, 0, h * mapScale);
                road2.transform.position = new Vector3(i * mapScale , 0, h * mapScale + mapScale);
                for (int c = 0; c < m_vRoadPattern[roadCount]; c++)
                {
                    if (i + c < maxZ)
                    {
                        datas[i + c + maxX * h].SetMapType(MapType.Road);
                    }
                }
            }
            i += (m_vRoadPattern[roadCount] + 1) * 2;
            roadCount++;
        }
    }
}
