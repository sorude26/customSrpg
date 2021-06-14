using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地形タイプ
/// </summary>
public enum MapType
{
    Normal,//通常
    NonAggressive,//侵入不可
    Asphalt,//舗装
    Wasteland,//荒地
    Forest,//森
}
public class MapCreater : MonoBehaviour
{
    /// <summary> 地形に合わせたオブジェクトのPrefab </summary>
    [SerializeField] GameObject[] m_stagePrefabs;

    /// <summary>
    /// 地形データ
    /// </summary>
    public class MapDate
    {
        /// <summary> X座標 </summary>
        public int PosX { get; private set; }
        /// <summary> Z座標 </summary>
        public int PosZ { get; private set; }
        /// <summary> 高さ </summary>
        public float Level { get; private set; }
        /// <summary> 地形種 </summary>
        public MapType MapType { get; private set; }
        /// <summary> 移動計算用データ </summary>
        public int MovePoint { get; set; }
        /// <summary>
        /// 初期設定
        /// </summary>
        /// <param name="mapType"></param>
        /// <param name="posX"></param>
        /// <param name="posZ"></param>
        /// <param name="level"></param>
        public MapDate(MapType mapType, int posX, int posZ, float level)
        {
            MapType = mapType;
            PosX = posX;
            PosZ = posZ;
            Level = level;
            MovePoint = 0;
        }
    }
    /// <summary>
    /// 平らな地形を生成しデータを返す
    /// </summary>
    /// <param name="maxX"></param>
    /// <param name="maxZ"></param>
    /// <returns></returns>
    public MapDate[] MapCreate(int maxX, int maxZ)
    {
        MapDate[] mapDates = new MapDate[maxX * maxZ];
        for (int i = 0; i < maxZ; i++)
        {
            for (int j = 0; j < maxX; j++)
            {
                MapDate map = new(MapType.Normal, j, i, 0);
                mapDates[j + i * maxX] = map;
                GameObject mapPanel = Instantiate(m_stagePrefabs[0]);
                mapPanel.transform.position = new Vector3(j * 10, 0, i * 10);
                mapPanel.transform.SetParent(transform);
            }
        }
        return mapDates;
    }
}
