using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 脚部データ
/// </summary>
[CreateAssetMenu]
public class LegData : UnitPartsData
{
    [Tooltip("移動力")]
    [SerializeField] int[] m_movePower;
    [Tooltip("昇降力")]
    [SerializeField] float[] m_liftingForce;
    [Tooltip("回避力")]
    [SerializeField] int[] m_avoidance;
    [Tooltip("脚部の種類")]
    [SerializeField] LegType[] m_legType;
    /// <summary> 移動力 </summary>
    public int[] MovePower { get => m_movePower; }
    /// <summary> 昇降力 </summary>
    public float[] LiftingForce { get => m_liftingForce; }
    /// <summary> 回避力 </summary>
    public int[] Avoidance { get => m_avoidance; }
    /// <summary> 脚部の種類 </summary>
    public LegType[] LegType { get => m_legType; }
}
