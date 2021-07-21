using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 脚部パーツ
/// </summary>
public class PartsLeg : UnitPartsMaster<LegData>
{
    /// <summary> 現在の移動力 </summary>
    public int CurrentMovePower { get; private set; }
    /// <summary> 現在の昇降力 </summary>
    public float CurrentLiftingForce { get; private set; }
    /// <summary> 現在の回避力 </summary>
    public int CurrentAvoidance { get; private set; }
    [Tooltip("脚部パーツの頂点")]
    [SerializeField] Transform m_legTop;
    [SerializeField] Transform m_lLeg1;
    [SerializeField] Transform m_lLeg2;
    [SerializeField] Transform m_lLeg3;
    [SerializeField] Transform m_rLeg1;
    [SerializeField] Transform m_rLeg2;
    [SerializeField] Transform m_rLeg3;

    /// <summary> 脚部パーツの頂点 </summary>
    public Transform LegTop { get => m_legTop; }
    public Transform LLeg1 { get => m_lLeg1; }
    public Transform LLeg2 { get => m_lLeg2; }
    public Transform LLeg3 { get => m_lLeg3; }
    public Transform RLeg1 { get => m_rLeg1; }
    public Transform RLeg2 { get => m_rLeg2; }
    public Transform RLeg3 { get => m_rLeg3; }
    protected override void StartSet()
    {
        CurrentMovePower = m_partsData.MovePower;
        CurrentLiftingForce = m_partsData.LiftingForce;
        CurrentAvoidance = m_partsData.Avoidance;
        base.StartSet();
    }
    protected override void PartsBreak()
    {
        CurrentAvoidance = 0;
        CurrentLiftingForce /= 5;
        CurrentMovePower /= 5;
        Break = true;
    }
}
