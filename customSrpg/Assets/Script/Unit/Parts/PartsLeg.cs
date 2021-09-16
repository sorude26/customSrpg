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
    /// <summary> 脚部の種類 </summary>
    public LegType Type { get => m_partsData.LegType[m_partsID]; }
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
        CurrentMovePower = m_partsData.MovePower[m_partsID];
        CurrentLiftingForce = m_partsData.LiftingForce[m_partsID];
        CurrentAvoidance = m_partsData.Avoidance[m_partsID];
        base.StartSet();
    }
    protected override void PartsBreak()
    {
        if (Break) { return; }
        CurrentAvoidance = 0;
        CurrentLiftingForce /= 5;
        CurrentMovePower /= 5;
        Break = true;
    }
    public override void DestoryParts()
    {
        Transform[] allParts = { m_lLeg3, m_lLeg2, m_lLeg1, m_rLeg3, m_rLeg2, m_rLeg1 };
        foreach (var parts in allParts)
        {
            Destroy(parts.gameObject);
        }
        base.DestoryParts();
    }
}
