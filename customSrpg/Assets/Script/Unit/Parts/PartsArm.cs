using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsArm : UnitPartsMaster<ArmData>
{
    /// <summary> 拳の位置 </summary>
    [SerializeField] Transform m_grip;
    /// <summary> 肩の位置 </summary>
    [SerializeField] Transform m_shoulder;
    
    [SerializeField] Transform m_armTop;
    [SerializeField] Transform m_armBottom;
    /// <summary> 命中精度 </summary>
    public int HitAccuracy { get => m_partsData.HitAccuracy; }
    /// <summary> 手の種類 </summary>
    public ArmType Arm { get => m_partsData.Arm; }
    /// <summary> 拳の位置 </summary>
    public Transform Grip { get => m_grip; }
    /// <summary> 肩の位置 </summary>
    public Transform Shoulder { get => m_shoulder; }
    public Transform ArmTop { get => m_armTop; }
    public Transform ArmBottom { get => m_armBottom; }
    /// <summary> 手持ち武器 </summary>
    public WeaponMaster GripWeapon { get; private set; }
    /// <summary> 肩装備武器 </summary>
    public WeaponMaster ShoulderWeapon { get; private set; }
    
    public void SetGripWeapon(WeaponMaster weapon) { GripWeapon = weapon; }
    public void SetShoulderWeapon(WeaponMaster weapon) { ShoulderWeapon = weapon; }
    public override int GetSize()
    {
        int size = PartsSize;
        if (GripWeapon)
        {
            size += GripWeapon.PartsSize;
        }
        if (ShoulderWeapon)
        {
            size += ShoulderWeapon.PartsSize;
        }
        return size;
    }
}
