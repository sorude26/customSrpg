using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地形タイプ
/// </summary>
public enum MapType
{
    /// <summary> 設定なし </summary>
    Normal,
    /// <summary> 侵入不可 </summary>
    NonAggressive,
    /// <summary> 舗装 </summary>
    Asphalt,
    /// <summary> 道路 </summary>
    Road,
    /// <summary> 荒地 </summary>
    Wasteland,
    /// <summary> 森 </summary>
    Forest,
}
public enum MapState
{
    Floor,
    Start,
    Goal,
    Open,
    Close,
}
/// <summary>
/// 地形データ
/// </summary>
public class MapData
{
    /// <summary> X座標 </summary>
    public int PosX { get; }
    /// <summary> Z座標 </summary>
    public int PosZ { get; }
    /// <summary> 一次元での座標番号 </summary>
    public int PosID { get; }
    /// <summary> 高さ </summary>
    public float Level { get; private set; }
    /// <summary> 地形種 </summary>
    public MapType MapType { get; private set; }
    /// <summary> 移動計算用データ </summary>
    public int MovePoint { get; set; }
    /// <summary> 攻撃計算用データ </summary>
    public int AttackPoint { get; set; }
    /// <summary> AIの計算用データ </summary>
    public int MapScore { get; set; }
    public MapState State { get; set; }
    public int SCost { get; set; }
    public int ZCost { get; set; }
    public MapData Parent { get; set; }
    /// <summary> 地形のパネル </summary>
    public StagePanel StagePanel { get; private set; }
    /// <summary>
    /// 初期設定
    /// </summary>
    /// <param name="type"></param>
    /// <param name="posX"></param>
    /// <param name="posZ"></param>
    /// <param name="level"></param>
    public MapData(MapType type, int posX, int posZ,int posID, float level,StagePanel stagePanel)
    {
        MapType = type;
        PosX = posX;
        PosZ = posZ;
        PosID = posID;
        Level = level;
        MovePoint = 0;
        AttackPoint = 0;
        MapScore = 0;
        State = MapState.Floor;
        StagePanel = stagePanel;
        StagePanel.SetPos(posX, posZ);
    }    
    public void SetMapType(MapType type)
    {
        MapType = type;
    }
}
