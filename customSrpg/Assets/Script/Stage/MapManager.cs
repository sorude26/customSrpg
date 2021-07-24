using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    /// <summary> 攻撃範囲 </summary>
    public List<MapData> AttackList { get; private set; }
    /// <summary> ステージの最大X座標 </summary>
    [SerializeField] int m_maxX = 15;
    /// <summary> ステージの最大Z座標 </summary>
    [SerializeField] int m_maxZ = 15;
    /// <summary> 地形サイズ </summary>
    [SerializeField] int m_mapScale = 10;
    /// <summary> ステージの最大X座標 </summary>
    public int MaxX { get => m_maxX; }
    /// <summary> ステージの最大Z座標 </summary>
    public int MaxZ { get => m_maxZ; }
    /// <summary> 地形サイズ </summary>
    public int MapScale { get => m_mapScale; }
    private void Awake()
    {
        Instance = this;
        MapDatas = m_mapCreater.MapCreate(m_maxX, m_maxZ, this.transform, MapScale);
        MoveList = new List<MapData>();
        AttackList = new List<MapData>();
    }
    /// <summary>
    /// 2次元座標を1次元座標に変換する
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public int GetPosition(int x, int z)
    {
        return x + z * MaxX;
    }
    /// <summary>
    /// ユニットの1次元座標を返す
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    public int GetPosition(Unit unit)
    {
        return unit.CurrentPosX + unit.CurrentPosZ * MaxX;
    }
    /// <summary>
    /// 指定座標の高度を返す
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public float GetLevel(int x, int z)
    {
        return MapDatas[GetPosition(x, z)].Level;
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
            case MapType.Road:
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
        foreach (var map in MapDatas)
        {
            map.MovePoint = 0;
        }
        int p = GetPosition(moveUnit);
        MoveList.Add(MapDatas[p]);
        MapDatas[p].MovePoint = moveUnit.GetUnitData().GetMovePower();
        SearchCross(p, MapDatas[p].MovePoint, moveUnit.GetUnitData().GetLiftingForce());
        return MoveList.ToArray();
    }
    /// <summary>
    /// 攻撃可能範囲の配列を返す
    /// </summary>
    /// <param name="attackWeapon"></param>
    /// <returns></returns>
    public MapData[] StartSearch(int x, int z, WeaponMaster attackWeapon)
    {
        AttackList.Clear();
        foreach (var map in MapDatas)
        {
            map.AttackPoint = 0;
        }
        int p = GetPosition(x, z);
        MapDatas[p].AttackPoint = attackWeapon.Range;
        SearchCross(p, MapDatas[p].AttackPoint, MapDatas[p].Level, attackWeapon.VerticalRange);
        return AttackList.Where(ap => ap.AttackPoint < attackWeapon.Range - attackWeapon.MinRange).ToArray();
    }
    /// <summary>
    /// 指定箇所の十字方向を進行可能か調べる
    /// </summary>
    /// <param name="position"></param>
    /// <param name="movePower"></param>
    /// <param name="liftingForce"></param>
    void SearchCross(int position, int movePower, float liftingForce)
    {
        foreach (var map in NeighorMap(position))
        {
            SearchPos(map, movePower, MapDatas[position].Level, liftingForce);
        }
    }
    /// <summary>
    /// 指定箇所が進行可能であれば記録を行う
    /// </summary>
    /// <param name="position"></param>
    /// <param name="movePower"></param>
    /// <param name="currentLevel"></param>
    /// <param name="liftingForce"></param>
    void SearchPos(MapData position, int movePower, float currentLevel, float liftingForce)
    {
        if (position == null)//調査対象がマップ範囲内であるか確認
        {
            return;
        }
        if (GetMoveCost(position.MapType) == 0)//侵入可能か確認、０は侵入不可又は未設定
        {
            return;
        }
        if (Mathf.Abs(position.Level - currentLevel) > liftingForce)//高低差確認
        {
            return;
        }
        if (movePower <= position.MovePoint)//確認済か確認
        {
            return;
        }
        if (StageManager.Instance.GetPositionUnit(position.PosID))//他のユニットがいるか確認
        {
            return;
        }
        //ユニットがいるか確認
        movePower -= GetMoveCost(position.MapType);//移動力変動
        if (movePower > 0)//移動可能箇所に足跡入力、再度検索
        {
            position.MovePoint = movePower;
            MoveList.Add(position);
            SearchCross(position.PosID, movePower, liftingForce);
        }
    }
    /// <summary>
    /// 指定箇所の十字方向を攻撃可能か調べる
    /// </summary>
    /// <param name="position"></param>
    /// <param name="attackRange"></param>
    /// <param name="startLevel"></param>
    /// <param name="verticalRang"></param>
    void SearchCross(int position, int attackRange, float startLevel, float verticalRang)
    {
        foreach (var map in NeighorMap(position))
        {
            SearchAttack(map, attackRange, startLevel, verticalRang);
        }
    }
    /// <summary>
    /// 指定箇所が攻撃可能であれば記録を行う
    /// </summary>
    /// <param name="position"></param>
    /// <param name="attackRange"></param>
    /// <param name="startLevel"></param>
    /// <param name="verticalRange"></param>    
    void SearchAttack(MapData position, int attackRange, float startLevel, float verticalRange)
    {
        if (position == null)//調査対象がマップ範囲内であるか確認
        {
            return;
        }
        if (GetMoveCost(position.MapType) == 0)//侵入可能か確認、０は侵入不可又は未設定
        {
            return;
        }
        if (attackRange <= position.AttackPoint)//確認済か確認
        {
            return;
        }
        if (position.Level - startLevel > verticalRange)//上方高低差確認
        {
            return;
        }
        if (startLevel - position.Level > verticalRange + verticalRange / 2)//下方高低差確認
        {
            return;
        }
        if (attackRange > 0)//攻撃可能箇所に足跡入力、再度検索
        {
            attackRange--;//攻撃範囲変動
            position.AttackPoint = attackRange;
            AttackList.Add(position);
            SearchCross(position.PosID, attackRange, startLevel, verticalRange);
        }
    }
    /// <summary>
    /// 最短経路検索を行い、スコアを記録する
    /// </summary>
    /// <param name="searchUnit"></param>
    /// <param name="targetUnit"></param>
    /// <returns></returns>
    public bool AstarSearch(in Unit searchUnit, in Unit targetUnit)
    {
        MoveList.Clear();
        foreach (var map in MapDatas)
        {
            map.State = MapState.Floor;
            map.MapScore = 0;
        }
        MapDatas[GetPosition(searchUnit)].State = MapState.Start;
        MapDatas[GetPosition(targetUnit)].State = MapState.Goal;
        if (!CheckNeighor(searchUnit, MapDatas[GetPosition(searchUnit)], targetUnit))
        {
            foreach (var map in MapDatas)
            {
                map.MapScore = 0;
            }
            return false;
        }
        return true;
    }
    bool CheckNeighor(in Unit unit, MapData mapData, Unit target)
    {
        foreach (var map in NeighorMap(mapData))
        {
            if (CheckPoint(unit, map, mapData, target))
            {
                return true;
            }
        }
        var next = GetOpenMap();
        if (next != null)
        {
            if (mapData.State != MapState.Start)
            {
                mapData.State = MapState.Close;
            }
            return CheckNeighor(unit, next, target);
        }
        return false;
    }
    bool CheckPoint(in Unit unit, MapData position, MapData parent, in Unit target)
    {
        if (position == null)
        {
            return false;
        }
        if (position.State == MapState.Goal)
        {
            return RouteScoreSet(parent);
        }
        if (position.State == MapState.Floor)
        {
            if (GetMoveCost(position.MapType) == 0)//侵入可能か確認、０は侵入不可又は未設定
            {
                position.State = MapState.Close;
                return false;
            }
            if (Mathf.Abs(position.Level - parent.Level) > unit.GetUnitData().GetLiftingForce())//高低差確認
            {
                return false;
            }
            if (StageManager.Instance.GetPositionUnit(position.PosID))//他のユニットがいるか確認
            {
                position.State = MapState.Close;
                return false;
            }
            position.State = MapState.Open;
            position.SCost = Mathf.Abs(position.PosX - target.CurrentPosX) + Mathf.Abs(position.PosZ - target.CurrentPosZ);
            position.ZCost = parent.ZCost + 1;
            position.MapScore = position.SCost + position.ZCost;
            position.Parent = parent;
            MoveList.Add(position);
        }
        return false;
    }
    MapData GetOpenMap()
    {
        MapData target = null;
        int scost = MaxX * MaxZ;
        foreach (var item in MoveList)
        {
            if (item.State != MapState.Open)
            {
                continue;
            }
            if (item.MapScore < scost)
            {
                scost = item.MapScore;
                target = item;
            }
            else if (item.MapScore == scost)
            {
                if (target.ZCost > item.ZCost)
                {
                    target = item;
                }
            }
        }
        return target;
    }
    bool RouteScoreSet(MapData map)
    {
        if (map == null)
        {
            return true;
        }
        if (map.State == MapState.Start)
        {
            return true;
        }
        map.MapScore = MaxX * MaxZ + map.ZCost;
        return RouteScoreSet(map.Parent);
    }
    /// <summary>
    /// 周囲四方向のマップデータ
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    IEnumerable<MapData> NeighorMap(int position)
    {
        if (0 <= position && position < MaxX * MaxZ)
        {
            if (MapDatas[position].PosZ > 0 && MapDatas[position].PosZ < MaxZ)
            {
                yield return MapDatas[position - MaxX];
            }
            if (MapDatas[position].PosZ >= 0 && MapDatas[position].PosZ < MaxZ - 1)
            {
                yield return MapDatas[position + MaxX];
            }
            if (MapDatas[position].PosX > 0 && MapDatas[position].PosX < MaxX)
            {
                yield return MapDatas[position - 1];
            }
            if (MapDatas[position].PosX >= 0 && MapDatas[position].PosX < MaxX - 1)
            {
                yield return MapDatas[position + 1];
            }
        }
    }
    /// <summary>
    /// 周囲四方向のマップデータ
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    IEnumerable<MapData> NeighorMap(MapData position)
    {
        if (position != null)
        {
            if (position.PosZ > 0 && position.PosZ < MaxZ)
            {
                yield return MapDatas[position.PosID - MaxX];
            }
            if (position.PosZ >= 0 && position.PosZ < MaxZ - 1)
            {
                yield return MapDatas[position.PosID + MaxX];
            }
            if (position.PosX > 0 && position.PosX < MaxX)
            {
                yield return MapDatas[position.PosID - 1];
            }
            if (position.PosX >= 0 && position.PosX < MaxX - 1)
            {
                yield return MapDatas[position.PosID + 1];
            }
        }
    }
}
