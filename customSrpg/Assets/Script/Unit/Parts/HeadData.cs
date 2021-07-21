using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 頭部データ
/// </summary>
[CreateAssetMenu]
public class HeadData : UnitPartsData
{
    [Tooltip("命中精度")]
    [SerializeField] int m_hitAccuracy;
    [Tooltip("回避力")]
    [SerializeField] int m_avoidance;
    /// <summary> 命中精度 </summary>
    public int HitAccuracy { get => m_hitAccuracy; }
    /// <summary> 回避力 </summary>
    public int Avoidance { get => m_avoidance; }
}
