using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユニットを生成する
/// </summary>
public class UnitBuilder : MonoBehaviour
{
    [SerializeField] PartsHead m_head;
    [SerializeField] PartsBody m_body;
    [SerializeField] PartsArm m_rArm;
    [SerializeField] PartsArm m_lArm;
    [SerializeField] PartsLeg m_leg;
    [SerializeField] GameObject m_weapon;
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
    private void Awake()
    {
        BuildUnit();
    }
    /// <summary>
    /// ユニットを生成し、各関節と連携させる。アニメーション設定前に呼ぶ
    /// </summary>
    public void BuildUnit()
    {
        switch (m_body.BodyPartsType)
        {
            case UnitType.Human:
                BuildHuman();
                break;
            case UnitType.Walker:
                BuildWalker();
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
    void BuildHuman()
    {
        var leg = Instantiate(m_leg);
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
        var body = Instantiate(m_body);
        body.transform.position = bodybP.position;
        body.transform.SetParent(bodybP);
        rArmbP.transform.position = body.RArmPos.position;
        lArmbP.transform.position = body.LArmPos.position;
        headP.transform.position = body.HeadPos.position;
        var rArm = Instantiate(m_rArm);
        rArm.transform.position = rArmbP.position;
        rArm.transform.SetParent(rArmbP);
        rArm1P.transform.position = rArm.ArmTop.position;
        rArm2P.transform.position = rArm.ArmBottom.position;
        rArm.ArmTop.SetParent(rArm1P);
        rArm.ArmBottom.SetParent(rArm2P);
        var lArm = Instantiate(m_lArm);
        lArm.transform.position = lArmbP.position;
        lArm.transform.SetParent(lArmbP);
        lArm1P.transform.position = lArm.ArmTop.position;
        lArm2P.transform.position = lArm.ArmBottom.position;
        lArm.ArmTop.SetParent(lArm1P);
        lArm.ArmBottom.SetParent(lArm2P);
        var head = Instantiate(m_head);
        head.transform.position = headP.position;
        head.transform.SetParent(headP);
        var weapon = Instantiate(m_weapon);
        weapon.transform.position = rArm.Grip.position;
        weapon.transform.rotation = Quaternion.Euler(90, 0, 0);
        weapon.transform.SetParent(rArm.Grip);
        var weapon2 = Instantiate(m_weapon);
        weapon2.transform.position = lArm.Grip.position;
        weapon2.transform.rotation = Quaternion.Euler(90, 0, 0);
        weapon2.transform.SetParent(lArm.Grip);
    }
    /// <summary>
    /// 歩行兵器を生成する
    /// </summary>
    void BuildWalker()
    {
        var leg = Instantiate(m_leg);
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
        var body = Instantiate(m_body);
        body.transform.position = bodybP.position;
        body.transform.SetParent(bodybP);
    }
}
