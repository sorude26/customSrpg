using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit
{
    // [SerializeField] protected UnitAI m_unitAI;
    protected bool m_moveMode;
    protected bool m_attackMode;
    WeaponMaster m_attackWeapon;
    bool m_end = false;
    public override void StartUp()
    {
        if (State == UnitState.StandBy)
        {
            State = UnitState.Action;
        }
    }
    protected IEnumerator StartAI()
    {
        m_end = false;
        while (!m_end)
        {
            yield return Move();
            yield return Attack();
            yield return End();
        }
        UnitRest();
        StageManager.Instance.NextUnit();
    }
    protected IEnumerator Move()
    {
        //if (m_unitAI.StartMove(this))
        //{
        //    m_moveMode = true;
        //    m_movelControl.MoveEndEvent += MoveModeEnd;
        //}
        while (m_moveMode)
        {
            yield return null;
        }
        MoveEnd();
    }

    protected IEnumerator Attack()
    {
        //m_attackWeapon = m_unitAI.StartAttack(this);
        if (m_attackWeapon)
        {
            m_attackMode = true;
            m_attackWeapon.OnAttackEnd += AttackModeEnd;
        }
        while (m_attackMode)
        {
            yield return null;
        }
        UnitRest();
    }
    protected void MoveModeEnd()
    {
        m_moveMode = false;
        //m_movelControl.MoveEndEvent -= MoveModeEnd;
    }
    protected void AttackModeEnd()
    {
        m_attackMode = false;
        m_attackWeapon = null;
    }

    IEnumerator End()
    {
        int count = 0;
        while (count < 2)
        {
            count++;
            yield return new WaitForSeconds(0.3f);
        }
        m_end = true;
    }
}
