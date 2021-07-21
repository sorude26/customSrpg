using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器の基底クラス
/// </summary>
public class WeaponMaster : PartsMaster<WeaponData>
{
    /// <summary> 武器攻撃力 </summary>
    public int Power { get => m_partsData.Power; }
    /// <summary> 総攻撃回数 </summary>
    public int MaxAttackNumber { get => m_partsData.MaxAttackNumber; }
    /// <summary> 最大攻撃力 </summary>
    public int MaxPower { get => m_partsData.Power * m_partsData.MaxAttackNumber; }
    /// <summary> 命中精度 </summary>
    public int HitAccuracy { get => m_partsData.HitAccuracy; }
    /// <summary> 最大射程 </summary>
    public int Range { get => m_partsData.Range; }
    /// <summary> 最低射程 </summary>
    public int MinRange { get => m_partsData.MinRange; }
    /// <summary> 最大対応高低差 </summary>
    public float VerticalRange { get => m_partsData.VerticalRange; }
    /// <summary> 武器種 </summary>
    public WeaponType Type { get => m_partsData.Type; }
    /// <summary> 武装部位 </summary>
    public WeaponPosition WeaponPos { get; private set; }
    /// <summary> 攻撃開始時のイベント </summary>
    protected Action<WeaponType> m_attackStart;
    /// <summary> 攻撃開始時のイベント </summary>
    public event Action<WeaponType> OnAttackStart { add => m_attackStart += value; remove => m_attackStart -= value; }
    /// <summary> 攻撃のイベント </summary>
    protected Action m_attack;
    /// <summary> 攻撃のイベント </summary>
    public event Action OnAttack { add => m_attack += value; remove => m_attack -= value; }
    /// <summary> 攻撃終了時のイベント </summary>
    protected Action m_attackEnd;
    /// <summary> 攻撃終了時のイベント </summary>
    public event Action OnAttackEnd { add => m_attackEnd += value; remove => m_attackEnd -= value; }
    
    /// <summary>
    /// 武装部位を設定する
    /// </summary>
    /// <param name="position"></param>
    public void SetWeaponPosition(WeaponPosition position)
    {
        WeaponPos = position;
    }
    /// <summary>
    /// 攻撃開始
    /// </summary>
    public virtual void AttackStart() { }
}
