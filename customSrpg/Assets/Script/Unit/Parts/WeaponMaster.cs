using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponMaster : PartsMaster<WeaponData>
{
    /// <summary> 武器攻撃力 </summary>
    public int Power { get => m_partsData.Power; }
    /// <summary> 総攻撃回数 </summary>
    public int MaxAttackNumber { get => m_partsData.MaxAttackNumber; }
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
    public Unit Owner { get; private set; }
    /// <summary> 武装部位 </summary>
    public WeaponPosition WPosition { get; private set; }
    /// <summary>
    /// 武装部位を設定する
    /// </summary>
    /// <param name="position"></param>
    public void SetWeaponPosition(WeaponPosition position,Unit owner)
    {
        WPosition = position;
        Owner = owner;
    }
    public virtual void Attack()
    {

    }
}
