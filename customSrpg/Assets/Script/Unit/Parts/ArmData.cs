using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 腕のデータ、左右があるため注意
/// </summary>
[CreateAssetMenu]
public class ArmData : UnitPartsData
{
    [Tooltip("命中精度")]
    [SerializeField] int m_hitAccuracy;
    [Tooltip("手の種類")]
    [SerializeField] ArmType m_armType;
    /// <summary> 命中精度 </summary>
    public int HitAccuracy { get => m_hitAccuracy; }
    /// <summary> 手の種類 </summary>
    public ArmType Arm { get => m_armType; }
}
