using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 腕パーツ
/// </summary>
public class PartsArm : UnitPartsMaster<ArmData>
{
    [Tooltip("拳の位置")]
    [SerializeField] Transform m_grip;
    [Tooltip("肩の位置")]
    [SerializeField] Transform m_shoulder;
    [Tooltip("腕上部")]
    [SerializeField] Transform m_armTop;
    [Tooltip("腕下部")]
    [SerializeField] Transform m_armBottom;
    /// <summary> 命中精度 </summary>
    public int HitAccuracy { get => m_partsData.HitAccuracy[m_partsID]; }
    /// <summary> 手の種類 </summary>
    public ArmType Arm { get => m_partsData.Arm; }
    /// <summary> 拳の位置 </summary>
    public Transform Grip { get => m_grip; }
    /// <summary> 肩の位置 </summary>
    public Transform Shoulder { get => m_shoulder; }
    /// <summary> 腕上部 </summary>
    public Transform ArmTop { get => m_armTop; }
    /// <summary> 腕下部 </summary>
    public Transform ArmBottom { get => m_armBottom; }
    /// <summary> 手持ち武器 </summary>
    public WeaponMaster GripWeapon { get; private set; }
    
    public void SetGripWeapon(WeaponMaster weapon) { GripWeapon = weapon; }
    public override int GetSize()
    {
        int size = PartsSize;
        if (GripWeapon)
        {
            size += GripWeapon.PartsSize;
        }
        return size;
    }
    public override int Damage(int power)
    {
        if (CurrentPartsHp <= 0)
        {
            return 0;
        }
        if (power == 0)
        {
            m_partsDamage.Add(-1);
            return 0;
        }
        int damage = BattleCalculator.GetDamage(power, Defense);
        CurrentPartsHp -= damage;
        m_partsDamage.Add(damage);
        if (CurrentPartsHp < MaxPartsHp / 3)
        {
            m_damageSmoke.SetActive(true);
        }
        if (CurrentPartsHp <= 0)
        {
            CurrentPartsHp = 0;
            Break = true;
            GripWeapon.SetBreak();
        }
        return damage;
    }
    public override void DestoryParts()
    {
        Transform[] allParts = { m_grip, m_armBottom, m_armTop, m_shoulder };
        foreach (var parts in allParts)
        {
            Destroy(parts.gameObject);
        }
        base.DestoryParts();
    }
}
