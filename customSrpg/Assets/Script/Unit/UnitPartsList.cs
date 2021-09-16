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
    public int WeaponRArmID;
    public int WeaponLArmID;
    public UnitBuildData(int head,int body,int rArm,int lArm,int leg,int weaponRA,int weaponLA)
    {
        HeadID = head;
        BodyID = body;
        RArmID = rArm;
        LArmID = lArm;
        LegID = leg;
        WeaponRArmID = weaponRA;
        WeaponLArmID = weaponLA;
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
    public PartsBody GetBody(int id) => m_bodys.Where(parts => parts.PartsID == id).FirstOrDefault();
    public PartsHead GetHead(int id) => m_heads.Where(parts => parts.PartsID == id).FirstOrDefault();
    public PartsArm GetRArm(int id) => m_arms.Where(parts => parts.PartsID == id && parts.Arm == ArmType.Right).FirstOrDefault();
    public PartsArm GetLArm(int id) => m_arms.Where(parts => parts.PartsID == id && parts.Arm == ArmType.Left).FirstOrDefault();
    public PartsLeg GetLeg(int id) => m_legs.Where(parts => parts.PartsID == id).FirstOrDefault();
    public WeaponMaster GetWeapon(int id) => m_weapons.Where(parts => parts.PartsID == id).FirstOrDefault();
    public PartsBody[] GetAllBodys() => m_bodys;
    public PartsHead[] GetAllHeads() => m_heads;
    public PartsArm[] GetAllRArms() => m_arms.Where(parts => parts.Arm == ArmType.Right).ToArray();
    public PartsArm[] GetAllLArms() => m_arms.Where(parts => parts.Arm == ArmType.Left).ToArray();
    public PartsLeg[] GetAllLegs() => m_legs;
    public WeaponMaster[] GetAllWeapons() => m_weapons;
    public PartsBody[] GetHaveAllBody()
    {
        List<PartsBody> allParts = new List<PartsBody>();
        for (int i = 0; i < m_bodys.Length; i++)
        { 
            if (UnitDataMaster.HavePartsDic[PartsType.Body][i] > 0)
            {
                allParts.Add(GetBody(i));
            }
        }
        return allParts.ToArray();
    }
    public PartsHead[] GetHaveAllHead()
    {
        List<PartsHead> allParts = new List<PartsHead>();
        for (int i = 0; i < m_heads.Length; i++)
        {
            if (UnitDataMaster.HavePartsDic[PartsType.Head][i] > 0)
            {
                allParts.Add(GetHead(i));
            }
        }
        return allParts.ToArray();
    }
    public PartsArm[] GetHaveAllRArm()
    {
        List<PartsArm> allParts = new List<PartsArm>();
        for (int i = 0; i < GetAllRArms().Length; i++)
        {
            if (UnitDataMaster.HavePartsDic[PartsType.RArm][i] > 0)
            {
                allParts.Add(GetRArm(i));
            }
        }
        return allParts.ToArray();
    }
    public PartsArm[] GetHaveAllLArm()
    {
        List<PartsArm> allParts = new List<PartsArm>();
        for (int i = 0; i < GetAllLArms().Length; i++)
        {
            if (UnitDataMaster.HavePartsDic[PartsType.LArm][i] > 0)
            {
                allParts.Add(GetLArm(i));
            }
        }
        return allParts.ToArray();
    }
    public PartsLeg[] GetHaveAllLeg()
    {
        List<PartsLeg> allParts = new List<PartsLeg>();
        for (int i = 0; i < m_legs.Length; i++)
        {
            if (UnitDataMaster.HavePartsDic[PartsType.Leg][i] > 0)
            {
                allParts.Add(GetLeg(i));
            }
        }
        return allParts.ToArray();
    }
    public WeaponMaster[] GetHaveAllWeapon()
    {
        List<WeaponMaster> allParts = new List<WeaponMaster>();
        for (int i = 0; i < m_weapons.Length; i++)
        {
            if (UnitDataMaster.HavePartsDic[PartsType.Weapon][i] > 0)
            {
                allParts.Add(GetWeapon(i));
            }
        }
        return allParts.ToArray();
    }
}
