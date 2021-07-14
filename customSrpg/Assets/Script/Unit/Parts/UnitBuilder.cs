using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユニットを生成する
/// </summary>
public class UnitBuilder : MonoBehaviour
{
    PartsHead m_shead;
    PartsBody m_sbody;
    PartsArm m_srArm;
    PartsArm m_slArm;
    PartsLeg m_sleg;
    WeaponMaster m_srAWeapon;
    WeaponMaster m_slAWeapon;
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
    public void SetData(UnitBuildData data, UnitMaster unitMaster)
    {
        BuildUnit(data);
        unitMaster.SetParts(m_sbody);
        unitMaster.SetParts(m_shead);
        unitMaster.SetParts(m_srArm);
        unitMaster.SetParts(m_slArm);
        unitMaster.SetParts(m_sleg);
        m_srAWeapon?.SetWeaponPosition(WeaponPosition.RArm);
        unitMaster.SetParts(m_srAWeapon);
        m_slAWeapon?.SetWeaponPosition(WeaponPosition.LArm);
        unitMaster.SetParts(m_slAWeapon);
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
            default:
                break;
        }
    }
    /// <summary>
    /// 人型の機体を生成する
    /// </summary>
    void BuildHuman(UnitBuildData data)
    {
        m_sleg = Instantiate(GameManager.Instanse.PartsList.GetLeg(data.LegID));
        m_sleg.transform.position = legbP.position;
        m_sleg.transform.SetParent(legbP);
        lLeg1P.transform.position = m_sleg.LLeg1.position;
        lLeg2P.transform.position = m_sleg.LLeg2.position;
        lLeg3P.transform.position = m_sleg.LLeg3.position;
        rLeg1P.transform.position = m_sleg.RLeg1.position;
        rLeg2P.transform.position = m_sleg.RLeg2.position;
        rLeg3P.transform.position = m_sleg.RLeg3.position;
        m_sleg.LLeg1.SetParent(lLeg1P);
        m_sleg.LLeg2.SetParent(lLeg2P);
        m_sleg.LLeg3.SetParent(lLeg3P);
        m_sleg.RLeg1.SetParent(rLeg1P);
        m_sleg.RLeg2.SetParent(rLeg2P);
        m_sleg.RLeg3.SetParent(rLeg3P);
        bodybP.transform.position = m_sleg.LegTop.position;
        m_sbody = Instantiate(GameManager.Instanse.PartsList.GetBody(data.BodyID));
        m_sbody.transform.position = bodybP.position;
        m_sbody.transform.SetParent(bodybP);
        rArmbP.transform.position = m_sbody.RArmPos.position;
        lArmbP.transform.position = m_sbody.LArmPos.position;
        headP.transform.position = m_sbody.HeadPos.position;
        m_srArm = Instantiate(GameManager.Instanse.PartsList.GetRArm(data.RArmID));
        m_srArm.transform.position = rArmbP.position;
        m_srArm.transform.SetParent(rArmbP);
        rArm1P.transform.position = m_srArm.ArmTop.position;
        rArm2P.transform.position = m_srArm.ArmBottom.position;
        m_srArm.ArmTop.SetParent(rArm1P);
        m_srArm.ArmBottom.SetParent(rArm2P);
        m_slArm = Instantiate(GameManager.Instanse.PartsList.GetLArm(data.LArmID));
        m_slArm.transform.position = lArmbP.position;
        m_slArm.transform.SetParent(lArmbP);
        lArm1P.transform.position = m_slArm.ArmTop.position;
        lArm2P.transform.position = m_slArm.ArmBottom.position;
        m_slArm.ArmTop.SetParent(lArm1P);
        m_slArm.ArmBottom.SetParent(lArm2P);
        m_shead = Instantiate(GameManager.Instanse.PartsList.GetHead(data.HeadID));
        m_shead.transform.position = headP.position;
        m_shead.transform.SetParent(headP);
        m_srAWeapon = Instantiate(GameManager.Instanse.PartsList.GetWeapon(data.WeaponRArmID));
        m_srAWeapon.transform.position = m_srArm.Grip.position;
        m_srAWeapon.transform.rotation = Quaternion.Euler(90, 0, 0);
        m_srAWeapon.transform.SetParent(m_srArm.Grip);
        m_slAWeapon = Instantiate(GameManager.Instanse.PartsList.GetWeapon(data.WeaponLArmID));
        m_slAWeapon.transform.position = m_slArm.Grip.position;
        m_slAWeapon.transform.rotation = Quaternion.Euler(90, 0, 0);
        m_slAWeapon.transform.SetParent(m_slArm.Grip);
    }
    /// <summary>
    /// 歩行兵器を生成する
    /// </summary>
    void BuildWalker(UnitBuildData data)
    {
        var leg = Instantiate(GameManager.Instanse.PartsList.GetLeg(data.LegID));
        leg.transform.position = legbP.position;
        leg.transform.SetParent(legbP);
        lLeg1P.transform.position = leg.LLeg1.position;
        lLeg2P.transform.position = leg.LLeg2.position;
        lLeg3P.transform.position = leg.LLeg3.position;
        rLeg1P.transform.position = leg.RLeg1.position;
        rLeg2P.transform.position = leg.RLeg2.position;
        rLeg3P.transform.position = leg.RLeg3.position;
        leg.LLeg1.SetParent(lLeg1P);
        leg.LLeg2.SetParent(lLeg2P);
        leg.LLeg3.SetParent(lLeg3P);
        leg.RLeg1.SetParent(rLeg1P);
        leg.RLeg2.SetParent(rLeg2P);
        leg.RLeg3.SetParent(rLeg3P);
        bodybP.transform.position = leg.LegTop.position;
        var body = Instantiate(GameManager.Instanse.PartsList.GetBody(data.BodyID));
        body.transform.position = bodybP.position;
        body.transform.SetParent(bodybP);
    }
}
