using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LegData : UnitPartsData
{
    [Tooltip("移動力")]
    [SerializeField] int m_movePower = 5;
    [Tooltip("昇降力")]
    [SerializeField] float m_liftingForce = 2;
    [Tooltip("回避力")]
    [SerializeField] int m_avoidance;
    /// <summary> 移動力 </summary>
    public int MovePower { get => m_movePower; }
    /// <summary> 昇降力 </summary>
    public float LiftingForce { get => m_liftingForce; }
    /// <summary> 回避力 </summary>
    public int Avoidance { get => m_avoidance; }
}
