using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit
{
    [SerializeField] protected UnitAI m_unitAI;
    protected bool m_moveMode;
    protected bool m_attackMode;
    WeaponMaster m_attackWeapon;
    bool end = false;
    public override void StartUp()
    {
        Debug.Log("呼ばれた" + this.name + State);
        if (State == UnitState.StandBy)
        {
            State = UnitState.Action;
            StartCoroutine(StartAI());
        }
    }
    protected IEnumerator StartAI()
    {
        end = false;
        //Debug.Log("開始");
        while (!end)
        {
            yield return Move();
            yield return Attack();
            yield return End();
        }
        //Debug.Log("終了");
        ActionEnd();
        StageManager.Instance.NextUnit();
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
        Debug.Log("移動" + this.name);
        MoveEnd();
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
        Debug.Log("攻撃" + this.name);
    }
    protected void MoveModeEnd()
    {
        m_moveMode = false;
        m_movelControl.MoveEndEvent -= MoveModeEnd;
    }
    protected void AttackModeEnd()
    {
        m_attackMode = false;
        m_attackWeapon.AttackEnd -= AttackModeEnd;
        m_attackWeapon = null;
    }
    public override void ActionEnd()
    {
        if (State == UnitState.Action)
        {
            State = UnitState.Rest;
        }
    }
    IEnumerator End()
    {
        int count = 0;
        while (count < 2)
        {
            count++;
            yield return new WaitForSeconds(0.3f);
        }
        Debug.Log("終了" + this.name);
        end = true;
    }
}
