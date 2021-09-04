using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 頭部パーツ
/// </summary>
public class PartsHead : UnitPartsMaster<HeadData>
{
    /// <summary> 命中精度 </summary>
    public int HitAccuracy { get => m_partsData.HitAccuracy[m_partsID]; }
    /// <summary> 回避力 </summary>
    public int Avoidance { get => m_partsData.Avoidance[m_partsID]; }    
}
