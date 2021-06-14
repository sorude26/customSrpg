using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsLeg : UnitPartsMaster
{
    /// <summary> 移動力 </summary>
    [SerializeField] int m_movePower = 10;
    /// <summary> 昇降力 </summary>
    [SerializeField] float m_liftingForce = 2;
    /// <summary> 回避力 </summary>
    [SerializeField] int m_avoidance;
    /// <summary> 移動力 </summary>
    public int MovePower { get => m_movePower; }
    /// <summary> 昇降力 </summary>
    public float LiftingForce { get => m_liftingForce; }
    /// <summary> 回避力 </summary>
    public int Avoidance { get => m_avoidance; }
    /// <summary> 脚部パーツの頂点 </summary>
    [SerializeField] Transform m_legTop;
    /// <summary> 脚部パーツの頂点 </summary>
    public Transform LegTop { get => m_legTop; }
    void Start()
    {
        StartSet();
    }
    protected override void PartsBreak()
    {
        Break = true;
    }
}
