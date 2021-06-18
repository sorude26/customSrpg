using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器種類
/// </summary>
public enum WeaponType
{
    Rifle,
    MachineGun,
    Shotgun,
    MShotGun,
    Melee,
}
/// <summary>
/// 武器装備部位
/// </summary>
public enum WeaponPosition
{
    Body,
    LArm,
    RArm,
    LShoulder,
    RShoulder,
}
public class WeaponMaster : PartsMaster
{
    /// <summary> 武器攻撃力 </summary>
    [SerializeField] int m_power = 5;
    /// <summary> 武器攻撃力 </summary>
    public int Power { get => m_power; }
    /// <summary> 最大射程 </summary>
    [SerializeField] int m_range = 4;
    /// <summary> 最大射程 </summary>
    public int Range { get => m_range; }
    /// <summary> 最低射程 </summary>
    [SerializeField] int m_minRange = 0;
    /// <summary> 最低射程 </summary>
    public int MinRange { get => m_minRange; }
    /// <summary> 最大対応高低差 </summary>
    [SerializeField] float m_verticalRange = 1f;
    /// <summary> 最大対応高低差 </summary>
    public float VerticalRange { get => m_verticalRange; }
    /// <summary> 武器種 </summary>
    [SerializeField] WeaponType m_weaponType = WeaponType.Rifle;
    /// <summary> 武器種 </summary>
    public WeaponType Type { get => m_weaponType; }
    /// <summary> 総攻撃回数 </summary>
    [SerializeField] int m_maxAttackNumber = 1;
    /// <summary> 総攻撃回数 </summary>
    public int MaxAttackNumber { get => m_maxAttackNumber; }
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
