using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地形の管理クラス
/// </summary>
public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }
    /// <summary>  </summary>
    [SerializeField] MapCreater m_mapCreater;
    /// <summary> マップの全情報 </summary>
    public MapData[] MapDatas { get; private set; }
    /// <summary> 移動範囲 </summary>
    public List<MapData> MoveList { get; private set; }
    /// <summary> ステージの最大X座標 </summary>
    [SerializeField] int m_maxX = 15;
    /// <summary> ステージの最大Z座標 </summary>
    [SerializeField] int m_maxZ = 15;
    /// <summary> 地形サイズ </summary>
    [SerializeField] int m_mapScale = 10;
    public int MapScale { get => m_mapScale; }
    /// <summary> ステージの最大X座標 </summary>
    public int MaxX { get => m_maxX; }
    /// <summary> ステージの最大Z座標 </summary>
    public int MaxZ { get => m_maxZ; }
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        MapDatas = m_mapCreater.MapCreate(m_maxX, m_maxZ, this.transform, MapScale);
        MoveList = new List<MapData>();
    }
    /// <summary>
    /// 地形タイプごとの移動力補正を返す
    /// </summary>
    /// <param name="mapType">地形タイプ</param>
    /// <returns></returns>
    public int GetMoveCost(MapType mapType)
    {
        int point;
        switch (mapType)
        {
            case MapType.Normal:
                point = 1;
                break;
            case MapType.NonAggressive:
                point = 0;
                break;
            case MapType.Asphalt:
                point = 2;
                break;
            case MapType.Wasteland:
                point = 3;
                break;
            case MapType.Forest:
                point = 4;
                break;
            default:
                point = 0;//０は移動不可
                break;
        }
        return point;
    }
    /// <summary>
    /// 移動可能範囲の配列を返す
    /// </summary>
    /// <param name="moveUnit"></param>
    /// <returns></returns>
    public MapData[] StartSearch(in Unit moveUnit)
    {
        MoveList.Clear();
        for (int i = 0; i < MapDatas.Length; i++)
        {
            MapDatas[i].MovePoint = 0;
        }
        int p = moveUnit.CurrentPosX + (moveUnit.CurrentPosZ * MaxX);
        MapDatas[p].MovePoint = moveUnit.GetUnitData().GetMovePower();
        SearchCross(p, MapDatas[p].MovePoint, moveUnit.GetUnitData().GetLiftingForce());
        return MoveList.ToArray();
    }
    /// <summary>
    /// 指定箇所の十字方向を進行可能か調べる
    /// </summary>
    /// <param name="position"></param>
    /// <param name="movePower"></param>
    /// <param name="liftingForce"></param>
    void SearchCross(int position, int movePower, float liftingForce)
    {
        if (0 <= position && position < MaxX * MaxZ)
        {
            if (MapDatas[position].PosZ > 0 && MapDatas[position].PosZ < MaxZ)
            {
                SearchPos(position - MaxX, movePower, MapDatas[position].Level, liftingForce);
            }
            if (MapDatas[position].PosZ >= 0 && MapDatas[position].PosZ < MaxZ - 1)
            {
                SearchPos(position + MaxX, movePower, MapDatas[position].Level, liftingForce);
            }
            if (MapDatas[position].PosX > 0 && MapDatas[position].PosX < MaxX)
            {
                SearchPos(position - 1, movePower, MapDatas[position].Level, liftingForce);
            }
            if (MapDatas[position].PosX >= 0 && MapDatas[position].PosX < MaxX - 1)
            {
                SearchPos(position + 1, movePower, MapDatas[position].Level, liftingForce);
            }
        }
    }
    /// <summary>
    /// 指定箇所が進行可能であれば記録を行う
    /// </summary>
    /// <param name="position"></param>
    /// <param name="movePower"></param>
    /// <param name="currentLevel"></param>
    /// <param name="liftingForce"></param>
    void SearchPos(int position, int movePower, float currentLevel, float liftingForce)
    {
        if (position < 0 || position >= MaxX * MaxZ)//調査対象がマップ範囲内であるか確認
        {
            return;
        }
        if (GetMoveCost(MapDatas[position].MapType) == 0)//侵入可能か確認、０は侵入不可又は未設定
        {
            return;
        }
        if (Mathf.Abs(MapDatas[position].Level - currentLevel) > liftingForce)//高低差確認
        {
            return;
        }
        if (movePower <= MapDatas[position].MovePoint)//確認済か確認
        {
            return;
        }
        //ユニットがいるか確認
        
        MapDatas[position].MovePoint = 0;
        movePower -= GetMoveCost(MapDatas[position].MapType);//移動力変動
        if (movePower > 0)//移動可能箇所に足跡入力、再度検索
        {
            MapDatas[position].MovePoint = movePower;
            MoveList.Add(MapDatas[position]);
            SearchCross(position, movePower, liftingForce);
        }
    }
}
