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
/// <summary>
/// 地形データ
/// </summary>
public class MapData
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
    /// <summary> 攻撃計算用データ </summary>
    public int AttackPoint { get; set; }
    /// <summary> AIの計算用データ </summary>
    public int MapScore { get; set; }
    /// <summary> 地形のパネル </summary>
    public StagePanel StagePanel { get; private set; }
    /// <summary>
    /// 初期設定
    /// </summary>
    /// <param name="mapType"></param>
    /// <param name="posX"></param>
    /// <param name="posZ"></param>
    /// <param name="level"></param>
    public MapData(MapType mapType, int posX, int posZ, float level,StagePanel stagePanel)
    {
        MapType = mapType;
        PosX = posX;
        PosZ = posZ;
        Level = level;
        MovePoint = 0;
        AttackPoint = 0;
        MapScore = 0;
        StagePanel = stagePanel;
        StagePanel.SetPos(posX, posZ);
    }    
}
