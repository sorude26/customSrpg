using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeControl : MonoBehaviour
{
    [SerializeField] Image m_hpGauge;
    [SerializeField] Text m_hpText;

    public int GaugeSet(int currentHp,int maxHp)
    {
        this.gameObject.SetActive(true);
        if (currentHp < 0){ currentHp = 0; }
        m_hpText.text = $"{currentHp}/{maxHp}";
        m_hpGauge.fillAmount = (float)currentHp / maxHp;
        return currentHp;
    }
    public void GaugeNone()
    {
        this.gameObject.SetActive(false);
    }
}
