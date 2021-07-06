using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponButtons : ButtonMaster
{
    Text[] m_buttonGuides;
    private void Start()
    {
        m_buttonGuides = new Text[m_buttons.Length-1];
        for (int i = 0; i < m_buttons.Length -1; i++)
        {
            m_buttonGuides[i] = m_buttons[i].GetComponentInChildren<Text>();
        }
    }
    public override void Open()
    {
        base.Open();
        for (int i = 0; i < m_buttons.Length - 1; i++)
        {
            WeaponMaster weapon = StageManager.Instance.TurnUnit?.GetUnitData().GetWeapon((WeaponPosition)i);
            if (weapon != null)
            {
                m_buttonGuides[i].text = weapon.PartsName;
            }
            else
            {
                m_buttonGuides[i].text = "None Data";
            }
        }
    }
    protected override void CursorMove()
    {
        base.CursorMove();
        if (m_buttonNum < (int)WeaponPosition.None)
        {
            if (StageManager.Instance.TurnUnit?.GetUnitData().GetWeapon((WeaponPosition)m_buttonNum) != null)
            {
                StageManager.Instance.AttackSearch((WeaponPosition)m_buttonNum);
            }
        }
    }
    public override void Decision()
    {
        if (m_buttonNum < (int)WeaponPosition.None)
        {
            BattleManager.Instance.SetWeaponPos((WeaponPosition)m_buttonNum);
            base.Decision();
        }
    }
}
