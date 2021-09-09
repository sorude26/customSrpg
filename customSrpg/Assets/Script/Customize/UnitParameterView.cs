using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Customize
{
    public class UnitParameterView : MonoBehaviour
    {
        [SerializeField] Image m_hpParameter;
        [SerializeField] Image m_dffParameter;
        [SerializeField] Image m_moveParameter;
        [SerializeField] Image m_liftParameter;
        [SerializeField] Image m_adoParameter;
        [SerializeField] Image m_hitParameter;
        [SerializeField] Image m_weightParameter;
        [SerializeField] int[] m_maxParameter = { 1000, 100, 30, 10, 200, 200, 1000 };
        public void SetParameter(UnitMaster unitData)
        {
            m_hpParameter.fillAmount = (float)unitData.GetMaxHP() / m_maxParameter[0];
            m_dffParameter.fillAmount = (float)unitData.GetAmorPoint() / m_maxParameter[1];
            m_moveParameter.fillAmount = (float)unitData.GetMovePower() / m_maxParameter[2];
            m_liftParameter.fillAmount = (float)unitData.GetLiftingForce() / m_maxParameter[3];
            m_adoParameter.fillAmount = (float)unitData.GetAvoidance() / m_maxParameter[4];
            m_hitParameter.fillAmount = (float)unitData.GetHitAccuracy(WeaponPosition.RShoulder) / m_maxParameter[5];
            m_weightParameter.fillAmount = (float)unitData.GetWeight() / m_maxParameter[6];
        }
    }
}