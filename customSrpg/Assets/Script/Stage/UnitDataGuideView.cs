using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitDataGuideView : MonoBehaviour
{
    [SerializeField] GaugeControl m_headGauge;
    [SerializeField] GaugeControl m_bodyGauge;
    [SerializeField] GaugeControl m_rArmGauge;
    [SerializeField] GaugeControl m_lArmGauge;
    [SerializeField] GaugeControl m_legGauge;
    [SerializeField] GameObject[] m_partsView;
    public void ViewData(Unit unit)
    {
        if (unit == null)
        {
            return;
        }
        gameObject.SetActive(true);
        switch (unit.GetUnitData().Body.BodyPartsType)
        {
            case UnitType.Human:
                DataSetHuman(unit);
                break;
            case UnitType.Walker:
                DataSetWalker(unit);
                break;
            case UnitType.Helicopter:
                break;
            case UnitType.Tank:
                break;
            default:
                break;
        }
    }
    void DataSetHuman(Unit unit)
    {
        if (m_headGauge.GaugeSet(unit.GetUnitData().Head.ViewCurrentHp, unit.GetUnitData().Head.MaxPartsHp) > 0)
            m_partsView[0].SetActive(true);
        if (m_bodyGauge.GaugeSet(unit.GetUnitData().Body.ViewCurrentHp, unit.GetUnitData().Body.MaxPartsHp) > 0)
            m_partsView[1].SetActive(true);
        if (m_rArmGauge.GaugeSet(unit.GetUnitData().RArm.ViewCurrentHp, unit.GetUnitData().RArm.MaxPartsHp) > 0)
            m_partsView[2].SetActive(true);
        if (m_lArmGauge.GaugeSet(unit.GetUnitData().LArm.ViewCurrentHp, unit.GetUnitData().LArm.MaxPartsHp) > 0)
            m_partsView[3].SetActive(true);
        if (m_legGauge.GaugeSet(unit.GetUnitData().Leg.ViewCurrentHp, unit.GetUnitData().Leg.MaxPartsHp) > 0)
            m_partsView[4].SetActive(true);
    }
    void DataSetWalker(Unit unit)
    {
        m_headGauge.GaugeNone();
        m_partsView[0].SetActive(false);
        if (m_bodyGauge.GaugeSet(unit.GetUnitData().Body.ViewCurrentHp, unit.GetUnitData().Body.MaxPartsHp) > 0)
            m_partsView[1].SetActive(true);
        m_rArmGauge.GaugeNone();
        m_partsView[2].SetActive(false);
        m_lArmGauge.GaugeNone();
        m_partsView[3].SetActive(false);
        if (m_legGauge.GaugeSet(unit.GetUnitData().Leg.ViewCurrentHp, unit.GetUnitData().Leg.MaxPartsHp) > 0)
            m_partsView[4].SetActive(true);
    }
    public void ViewEnd()
    {
        StopAllCoroutines();
        foreach (var item in m_partsView)
        {
            item.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
