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
    public UnitBuildData(int head,int body,int rArm,int lArm,int leg,int weaponB,int weaponRA,int weaponLA)
    {
        HeadID = head;
        BodyID = body;
        RArmID = rArm;
        LArmID = lArm;
        LegID = leg;
        WeaponBodyID = weaponB;
        WeaponRArmID = weaponRA;
        WeaponLArmID = weaponLA;
    }
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
}
