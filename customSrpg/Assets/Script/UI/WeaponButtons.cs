using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponButtons : ButtonMaster
{
    [SerializeField] GameObject[] m_buttonGuard;
    public override void Open()
    {
        base.Open();
       
    }
    protected override void CursorMove()
    {
        base.CursorMove();
        if (m_buttonNum < (int)WeaponPosition.None)
        {
            if (StageManager.Instance.TurnUnit.GetUnitData().GetWeapon((WeaponPosition)m_buttonNum) != null)
            {
                StageManager.Instance.AttackSearch((WeaponPosition)m_buttonNum);
            }
        }
    }
}
