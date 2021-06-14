using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Rifle,
    MachineGun,
    Shotgun,
    MShotGun,
    Melee,
}
public class WeaponMaster : PartsMaster
{
    /// <summary> 武器攻撃力 </summary>
    [SerializeField] int m_power = 5;
    /// <summary> 武器攻撃力 </summary>
    public int Power { get => m_power; }
    /// <summary> 最大射程 </summary>
    [SerializeField] float m_range = 4;
    /// <summary> 最大射程 </summary>
    public float Range { get => m_range; }
    /// <summary> 武器種 </summary>
    [SerializeField] WeaponType m_weaponType = WeaponType.Rifle;
    /// <summary> 武器種 </summary>
    public WeaponType Type { get => m_weaponType; }
    /// <summary> 総攻撃回数 </summary>
    [SerializeField] int m_maxAttackNumber = 1;
    /// <summary> 総攻撃回数 </summary>
    public int MaxAttackNumber { get => m_maxAttackNumber; }
}
