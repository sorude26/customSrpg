using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsLeg : UnitPartsMaster<LegData>
{
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
        CurrentMovePower = m_partsData.MovePower;
        CurrentLiftingForce = m_partsData.LiftingForce;
        CurrentAvoidance = m_partsData.Avoidance;
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
