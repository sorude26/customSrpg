using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BodyData : UnitPartsData
{
    [Tooltip("機体出力")]
    [SerializeField] int m_unitOutput;
    [Tooltip("昇降力")]
    [SerializeField] float m_liftingForce;
    [Tooltip("移動力")]
    [SerializeField] int m_movePower = 0;
    [Tooltip("回避力")]
    [SerializeField] int m_avoidance;
    [Tooltip("命中精度")]
    [SerializeField] int m_hitAccuracy;
    [Tooltip("機体タイプ")]
    [SerializeField] UnitType m_bodyType = UnitType.Human;
    /// <summary> 機体出力 </summary>
    public int UnitOutput { get => m_unitOutput; }
    /// <summary> 昇降力 </summary>
    public float LiftingForce { get => m_liftingForce; }
    /// <summary> 移動力 </summary>
    public int MovePower { get => m_movePower; }
    /// <summary> 回避力 </summary>
    public int Avoidance { get => m_avoidance; }
    /// <summary> 命中精度 </summary>
    public int HitAccuracy { get => m_hitAccuracy; }
    /// <summary> 機体タイプ </summary>
    public UnitType BodyPartsType { get => m_bodyType; }
}
