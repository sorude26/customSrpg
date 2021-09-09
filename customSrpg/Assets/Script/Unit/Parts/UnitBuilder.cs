using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユニットを生成する
/// </summary>
public class UnitBuilder : MonoBehaviour
{
    PartsHead m_head;
    PartsBody m_body;
    PartsArm m_rArm;
    PartsArm m_lArm;
    PartsLeg m_leg;
    WeaponMaster m_rAWeapon;
    WeaponMaster m_lAWeapon;
    WeaponMaster m_bodyWeapon;
    WeaponMaster m_shoulderWeapon;
    [SerializeField] Transform headP;
    [SerializeField] Transform bodybP;
    [SerializeField] Transform lArmbP;
    [SerializeField] Transform lArm1P;
    [SerializeField] Transform lArm2P;
    [SerializeField] Transform rArmbP;
    [SerializeField] Transform rArm1P;
    [SerializeField] Transform rArm2P;
    [SerializeField] Transform legbP;
    [SerializeField] Transform lLeg1P;
    [SerializeField] Transform lLeg2P;
    [SerializeField] Transform lLeg3P;
    [SerializeField] Transform rLeg1P;
    [SerializeField] Transform rLeg2P;
    [SerializeField] Transform rLeg3P;

    /// <summary>
    /// 機体の構成データを受け取り設定後機体の生成を行う
    /// </summary>
    /// <param name="data"></param>
    /// <param name="unitMaster"></param> 
    public void SetData(UnitBuildData data, UnitMaster unitMaster)
    {
        BuildUnit(data);
        unitMaster.SetParts(m_body);
        unitMaster.SetParts(m_head);
        unitMaster.SetParts(m_rArm);
        unitMaster.SetParts(m_lArm);
        unitMaster.SetParts(m_leg);
        m_rAWeapon?.SetWeaponPosition(WeaponPosition.RArm);
        unitMaster.SetParts(m_rAWeapon);
        m_lAWeapon?.SetWeaponPosition(WeaponPosition.LArm);
        unitMaster.SetParts(m_lAWeapon);
        m_bodyWeapon?.SetWeaponPosition(WeaponPosition.Body);
        unitMaster.SetParts(m_bodyWeapon);
        m_shoulderWeapon?.SetWeaponPosition(WeaponPosition.Shoulder);
        unitMaster.SetParts(m_shoulderWeapon);
    }
    public Transform SetDataModel(UnitBuildData data, UnitMaster unitMaster)
    {
        SetData(data, unitMaster);
        return ModelSet();
    }
    /// <summary>
    /// ユニットを生成し、各関節と連携させる
    /// </summary>
    public void BuildUnit(UnitBuildData data)
    {
        switch (GameManager.Instanse.PartsList.GetBody(data.BodyID).BodyPartsType)
        {
            case UnitType.Human:
                BuildHuman(data);
                break;
            case UnitType.Walker:
                BuildWalker(data);
                break;
            case UnitType.Helicopter:
                break;
            case UnitType.Tank:
                break;
            case UnitType.Giant:
                BuildGiant(data);
                break;
            default:
                break;
        }
    }
    public void ResetBuild(UnitBuildData data, UnitMaster unitMaster)
    {
        DestroyAllParts();
        SetData(data, unitMaster);
    }
    public Transform ResetBuildModel(UnitBuildData data, UnitMaster unitMaster)
    {
        DestroyAllParts();
        return SetDataModel(data, unitMaster);
    }
    public void DestroyAllParts() 
    {
        bodybP.SetParent(legbP);
        IParts[] allParts = { m_lAWeapon, m_rAWeapon, m_bodyWeapon, m_head, m_lArm, m_rArm, m_body, m_leg };
        foreach (var parts in allParts)
        {
            parts?.DestoryParts();
        }
        m_head = null;
        m_body = null;
        m_lArm = null;
        m_rArm = null;
        m_leg = null;
        m_lAWeapon = null;
        m_rAWeapon = null;
        m_bodyWeapon = null;
        lArm2P.rotation = Quaternion.Euler(0, 0, 0);
        rArm2P.rotation = Quaternion.Euler(0, 0, 0);
    }
    Transform ModelSet()
    {
        lArm2P.rotation = Quaternion.Euler(-50, 0, 0);
        rArm2P.rotation = Quaternion.Euler(-50, 0, 0);
        return m_body.BodyPos;
    }
    /// <summary>
    /// 人型の機体を生成する
    /// </summary>
    void BuildHuman(UnitBuildData data)
    {
        m_leg = Instantiate(GameManager.Instanse.PartsList.GetLeg(data.LegID));
        m_leg.transform.position = legbP.position;
        m_leg.transform.SetParent(legbP);
        lLeg1P.transform.position = m_leg.LLeg1.position;
        lLeg2P.transform.position = m_leg.LLeg2.position;
        lLeg3P.transform.position = m_leg.LLeg3.position;
        rLeg1P.transform.position = m_leg.RLeg1.position;
        rLeg2P.transform.position = m_leg.RLeg2.position;
        rLeg3P.transform.position = m_leg.RLeg3.position;
        m_leg.LLeg1.SetParent(lLeg1P);
        m_leg.LLeg2.SetParent(lLeg2P);
        m_leg.LLeg3.SetParent(lLeg3P);
        m_leg.RLeg1.SetParent(rLeg1P);
        m_leg.RLeg2.SetParent(rLeg2P);
        m_leg.RLeg3.SetParent(rLeg3P);
        bodybP.transform.position = m_leg.LegTop.position;
        bodybP.SetParent(m_leg.LegTop);
        m_body = Instantiate(GameManager.Instanse.PartsList.GetBody(data.BodyID));
        m_body.transform.position = bodybP.position;
        m_body.transform.SetParent(bodybP);
        if (m_body.BodyWeapon) { m_bodyWeapon = m_body.BodyWeapon; }
        if (m_body.ShoulderWeapon) { m_shoulderWeapon = m_body.ShoulderWeapon; }
        rArmbP.transform.position = m_body.RArmPos.position;
        lArmbP.transform.position = m_body.LArmPos.position;
        headP.transform.position = m_body.HeadPos.position;
        m_rArm = Instantiate(GameManager.Instanse.PartsList.GetRArm(data.RArmID));
        m_rArm.transform.position = rArmbP.position;
        m_rArm.transform.SetParent(rArmbP);
        rArm1P.transform.position = m_rArm.ArmTop.position;
        rArm2P.transform.position = m_rArm.ArmBottom.position;
        m_rArm.ArmTop.SetParent(rArm1P);
        m_rArm.ArmBottom.SetParent(rArm2P);
        m_lArm = Instantiate(GameManager.Instanse.PartsList.GetLArm(data.LArmID));
        m_lArm.transform.position = lArmbP.position;
        m_lArm.transform.SetParent(lArmbP);
        lArm1P.transform.position = m_lArm.ArmTop.position;
        lArm2P.transform.position = m_lArm.ArmBottom.position;
        m_lArm.ArmTop.SetParent(lArm1P);
        m_lArm.ArmBottom.SetParent(lArm2P);
        m_head = Instantiate(GameManager.Instanse.PartsList.GetHead(data.HeadID));
        m_head.transform.position = headP.position;
        m_head.transform.rotation = m_body.HeadPos.rotation;
        m_head.transform.SetParent(headP);
        m_rAWeapon = Instantiate(GameManager.Instanse.PartsList.GetWeapon(data.WeaponRArmID));
        m_rAWeapon.transform.position = m_rArm.Grip.position;
        m_rAWeapon.transform.rotation = Quaternion.Euler(90, 0, 0);
        m_rAWeapon.transform.SetParent(m_rArm.Grip);
        m_lAWeapon = Instantiate(GameManager.Instanse.PartsList.GetWeapon(data.WeaponLArmID));
        m_lAWeapon.transform.position = m_lArm.Grip.position;
        m_lAWeapon.transform.rotation = Quaternion.Euler(90, 0, 0);
        m_lAWeapon.transform.SetParent(m_lArm.Grip);
    }
    /// <summary>
    /// 歩行兵器を生成する
    /// </summary>
    void BuildWalker(UnitBuildData data)
    {
        m_leg = Instantiate(GameManager.Instanse.PartsList.GetLeg(data.LegID));
        m_leg.transform.position = legbP.position;
        m_leg.transform.SetParent(legbP);
        lLeg1P.transform.position = m_leg.LLeg1.position;
        lLeg2P.transform.position = m_leg.LLeg2.position;
        lLeg3P.transform.position = m_leg.LLeg3.position;
        rLeg1P.transform.position = m_leg.RLeg1.position;
        rLeg2P.transform.position = m_leg.RLeg2.position;
        rLeg3P.transform.position = m_leg.RLeg3.position;
        m_leg.LLeg1.SetParent(lLeg1P);
        m_leg.LLeg2.SetParent(lLeg2P);
        m_leg.LLeg3.SetParent(lLeg3P);
        m_leg.RLeg1.SetParent(rLeg1P);
        m_leg.RLeg2.SetParent(rLeg2P);
        m_leg.RLeg3.SetParent(rLeg3P);
        bodybP.transform.position = m_leg.LegTop.position;
        bodybP.SetParent(m_leg.LegTop);
        m_body = Instantiate(GameManager.Instanse.PartsList.GetBody(data.BodyID));
        m_body.transform.position = bodybP.position;
        m_body.transform.SetParent(bodybP);
        if (m_body.BodyWeapon) { m_bodyWeapon = m_body.BodyWeapon; }
        if (m_body.ShoulderWeapon) { m_shoulderWeapon = m_body.ShoulderWeapon; }
    }
    void BuildGiant(UnitBuildData data)
    {
        m_body = Instantiate(GameManager.Instanse.PartsList.GetBody(data.BodyID)); 
        m_body.transform.position = transform.position;
        m_body.transform.SetParent(transform);
        if (m_body.BodyWeapon) { m_bodyWeapon = m_body.BodyWeapon; }
        if (m_body.ShoulderWeapon) { m_shoulderWeapon = m_body.ShoulderWeapon; }
    }
}
