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

[CreateAssetMenu]
public class WeaponData : PartsData
{
    [Tooltip("武器攻撃力")]
    [SerializeField] int m_power = 5;
    [Tooltip("総攻撃回数")]
    [SerializeField] int m_maxAttackNumber = 1;
    [Tooltip("攻撃間隔")]
    [SerializeField] float m_attackInterval = 0; 
    [Tooltip("命中精度")]
    [SerializeField] int m_hitAccuracy;
    [Tooltip("最大射程")]
    [SerializeField] int m_range = 4;
    [Tooltip("最低射程")]
    [SerializeField] int m_minRange = 0;
    [Tooltip("最大対応高低差")]
    [SerializeField] float m_verticalRange = 1f;
    [Tooltip("武器種")]
    [SerializeField] WeaponType m_weaponType = WeaponType.Rifle;
    /// <summary> 武器攻撃力 </summary>
    public int Power { get => m_power; }
    /// <summary> 総攻撃回数 </summary>
    public int MaxAttackNumber { get => m_maxAttackNumber; }
    /// <summary> 攻撃間隔 </summary>
    public float AttackInterval { get => m_attackInterval; }
    /// <summary> 命中精度 </summary>
    public int HitAccuracy { get => m_hitAccuracy; }
    /// <summary> 最大射程 </summary>
    public int Range { get => m_range; }
    /// <summary> 最低射程 </summary>
    public int MinRange { get => m_minRange; }
    /// <summary> 最大対応高低差 </summary>
    public float VerticalRange { get => m_verticalRange; }
    /// <summary> 武器種 </summary>
    public WeaponType Type { get => m_weaponType; }
}
