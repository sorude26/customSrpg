using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcUnit : Unit
{
    [Tooltip("NPCユニットの行動設定")]
    [SerializeField] protected UnitAI m_unitAI;
    /// <summary> 移動中フラグ </summary>
    protected bool m_moveMode;
    /// <summary> 攻撃中フラグ </summary>
    protected bool m_attackMode;
    /// <summary> 攻撃武器 </summary>
    WeaponMaster m_attackWeapon;
    /// <summary>
    /// 待機状態ならば行動を開始
    /// </summary>
    public override void StartUp()
    {
        if (State == UnitState.StandBy)
        {
            State = UnitState.Action;
            StartCoroutine(StartAI());
        }
    }
    /// <summary>
    /// 自身に設定されたAIに基づいて行動する
    /// </summary>
    /// <returns></returns>
    protected IEnumerator StartAI()
    {
        yield return Move();
        yield return Attack();
        yield return End();
        ActionEnd();
        StageManager.Instance.NextUnit();
    }
    /// <summary>
    /// 移動対象があれば移動する
    /// </summary>
    /// <returns></returns>
    protected IEnumerator Move()
    {
        if(m_unitAI.StartMove(this))
        {
            m_moveMode = true;
            m_movelControl.MoveEndEvent += MoveModeEnd;
        }
        while (m_moveMode)
        {
            yield return null;
        }
        MoveEnd();
    }
    /// <summary>
    /// 攻撃対象があれば攻撃する
    /// </summary>
    /// <returns></returns>
    protected IEnumerator Attack()
    {
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
    /// <summary>
    /// 移動終了時に呼ぶ
    /// </summary>
    protected void MoveModeEnd()
    {
        m_moveMode = false;
        m_movelControl.MoveEndEvent -= MoveModeEnd;
    }
    /// <summary>
    /// 攻撃終了時に呼ぶ
    /// </summary>
    protected void AttackModeEnd()
    {
        m_attackMode = false;
        m_attackWeapon.AttackEnd -= AttackModeEnd;
        m_attackWeapon = null;
    }
    /// <summary>
    /// 終了処理
    /// </summary>
    /// <returns></returns>
    IEnumerator End()
    {
        int count = 0;
        while (count < 2)
        {
            count++;
            yield return new WaitForSeconds(0.3f);
        }
    }
}
