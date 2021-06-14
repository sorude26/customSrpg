using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ArmType
{
    Left,
    Right,
}
public class PartsArm : UnitPartsMaster
{
    /// <summary> 手の種類 </summary>
    [SerializeField] ArmType m_armType;
    /// <summary> 拳の位置 </summary>
    [SerializeField] Transform m_grip;
    /// <summary> 肩の位置 </summary>
    [SerializeField] Transform m_shoulder;
    /// <summary> 手の種類 </summary>
    public ArmType Arm { get => m_armType; }
    /// <summary> 拳の位置 </summary>
    public Transform Grip { get => m_grip; }
    /// <summary> 肩の位置 </summary>
    public Transform Shoulder { get => m_shoulder; }
    /// <summary> 手持ち武器 </summary>
    public WeaponMaster GripWeapon { get; private set; }
    /// <summary> 肩装備武器 </summary>
    public WeaponMaster ShoulderWeapon { get; private set; }
    void Start()
    {
        StartSet();
    }
    public void SetGripWeapon(WeaponMaster weapon) { GripWeapon = weapon; }
    public void SetShoulderWeapon(WeaponMaster weapon) { ShoulderWeapon = weapon; }
    public override int GetPartsSize()
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
