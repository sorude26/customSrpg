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
    /// <summary> 現在の移動力 </summary>
    public int CurrentMovePower { get; private set; }
    /// <summary> 現在の昇降力 </summary>
    public float CurrentLiftingForce { get; private set; }
    /// <summary> 現在の回避力 </summary>
    public int CurrentAvoidance { get; private set; }
    /// <summary> 脚部パーツの頂点 </summary>
    [SerializeField] Transform m_legTop;
    /// <summary> 脚部パーツの頂点 </summary>
    public Transform LegTop { get => m_legTop; }
    protected override void StartSet()
    {
        CurrentMovePower = m_movePower;
        CurrentLiftingForce = m_liftingForce;
        CurrentAvoidance = m_avoidance;
        base.StartSet();
    }
    protected override void PartsBreak()
    {
        CurrentAvoidance /= 5;
        CurrentLiftingForce /= 5;
        CurrentMovePower /= 5;
        Break = true;
    }
}
