using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit
{
    [SerializeField] protected UnitAI m_unitAI;
    protected bool m_moveMode;
    protected bool m_attackMode;
    WeaponMaster m_attackWeapon;
    public override void StartUp()
    {
        //Debug.Log("呼ばれた");
        if (State == UnitState.StandBy)
        {
            State = UnitState.Action;
            StartCoroutine(StartAI());
        }
    }
    protected IEnumerator StartAI()
    {
        //Debug.Log("開始");
        while (State == UnitState.Action)
        {
            yield return Move();
            yield return Attack();
            yield return null;
        }
        //Debug.Log("終了");
    }
    protected IEnumerator Move()
    {
        //Debug.Log("移動");
        if(m_unitAI.StartMove(this))
        {
            m_moveMode = true;
            m_movelControl.MoveEndEvent += MoveModeEnd;
        }
        while (m_moveMode)
        {
            yield return null;
        }
    }
    
    protected IEnumerator Attack()
    {
        //Debug.Log("攻撃");
        m_attackWeapon = m_unitAI.StartAttack(this);
        if (m_attackWeapon)
        {
            m_attackMode = true;
            m_attackWeapon.AttackEnd += AttackModeEnd;
        }
        while (m_attackMode)
        {
            yield return null;
        }
        ActionEnd();
    }
    protected void MoveModeEnd()
    {
        m_moveMode = false;
        m_movelControl.MoveEndEvent -= MoveEnd;
    }
    protected void AttackModeEnd()
    {
        m_attackMode = false;
        m_attackWeapon.AttackEnd -= AttackModeEnd;
    }
    public override void ActionEnd()
    {
        if (State == UnitState.Action)
        {
            StartCoroutine(End());
        }
    }
    IEnumerator End()
    {
        int count = 0;
        while (count < 2)
        {
            count++;
            yield return new WaitForSeconds(0.5f);
        }
        State = UnitState.Rest;
        m_attackWeapon = null;
        StageManager.Instance.NextUnit();
    }
}
