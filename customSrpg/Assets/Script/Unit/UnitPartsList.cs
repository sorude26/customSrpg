using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 機体の構成データ
/// </summary>
[System.Serializable]
public struct UnitBuildData 
{
    public int HeadID;
    public int BodyID;
    public int RArmID;
    public int LArmID;
    public int LegID;
    public int WeaponBodyID;
    public int WeaponRArmID;
    public int WeaponLArmID;
    public int WeaponShoulderID;
    public UnitBuildData(int head,int body,int rArm,int lArm,int leg,int weaponB,int weaponRA,int weaponLA,int weaponShoulder)
    {
        HeadID = head;
        BodyID = body;
        RArmID = rArm;
        LArmID = lArm;
        LegID = leg;
        WeaponBodyID = weaponB;
        WeaponRArmID = weaponRA;
        WeaponLArmID = weaponLA;
        WeaponShoulderID = weaponShoulder;
    }
}
public enum PartsType
{
    Body,
    Head,
    RArm,
    LArm,
    Leg,
    Weapon,
}
/// <summary>
/// 全パーツを保有するオブジェクト
/// </summary>
[CreateAssetMenu]
public class UnitPartsList : ScriptableObject
{
    [SerializeField] PartsBody[] m_bodys;
    [SerializeField] PartsHead[] m_heads;
    [SerializeField] PartsArm[] m_arms;
    [SerializeField] PartsLeg[] m_legs;
    [SerializeField] WeaponMaster[] m_weapons;
    public PartsBody GetBody(int id) => m_bodys.Where(parts => parts.GetID() == id).FirstOrDefault();
    public PartsHead GetHead(int id) => m_heads.Where(parts => parts.GetID() == id).FirstOrDefault();
    public PartsArm GetRArm(int id) => m_arms.Where(parts => parts.GetID() == id && parts.Arm == ArmType.Right).FirstOrDefault();
    public PartsArm GetLArm(int id) => m_arms.Where(parts => parts.GetID() == id && parts.Arm == ArmType.Left).FirstOrDefault();
    public PartsLeg GetLeg(int id) => m_legs.Where(parts => parts.GetID() == id).FirstOrDefault();
    public WeaponMaster GetWeapon(int id) => m_weapons.Where(parts => parts.GetID() == id).FirstOrDefault();
    public PartsBody[] GetAllBodys() => m_bodys;
    public PartsHead[] GetAllHeads() => m_heads;
    public PartsArm[] GetAllRArms() => m_arms.Where(parts => parts.Arm == ArmType.Right).ToArray();
    public PartsArm[] GetAllLArms() => m_arms.Where(parts => parts.Arm == ArmType.Left).ToArray();
    public PartsLeg[] GetAllLegs() => m_legs;
    public WeaponMaster[] GetAllWeapons() => m_weapons;
}
