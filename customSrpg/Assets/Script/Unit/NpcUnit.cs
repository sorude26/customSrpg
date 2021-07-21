using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 自立行動を行うユニット
/// </summary>
public class NpcUnit : Unit
{
    [Tooltip("NPCユニットの行動設定")]
    [SerializeField] protected UnitAI m_unitAI;
    [Tooltip("行動終了時の待ち時間")]
    [SerializeField] protected float m_actionEndWaitTime = 1f;
    /// <summary> 待機時間 </summary>
    protected float m_waitTime;
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
    /// 自身に設定されたAIに基づいて各フェイズに行動する
    /// </summary>
    /// <returns></returns>
    protected IEnumerator StartAI()
    {
        yield return Move();
        yield return Attack();
        yield return End();
    }
    /// <summary>
    /// 移動処理
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
        StageManager.Instance.Cursor.Warp(this);
    }
    /// <summary>
    /// 攻撃処理
    /// </summary>
    /// <returns></returns>
    protected IEnumerator Attack()
    {
        m_attackWeapon = m_unitAI.StartAttack(this);
        if (m_attackWeapon)
        {
            m_attackMode = true;
            m_attackWeapon.OnAttackEnd += AttackModeEnd;
            m_waitTime = m_actionEndWaitTime;
        }
        while (m_attackMode)
        {
            yield return null;
        }
    }
    /// <summary>
    /// 終了処理
    /// </summary>
    /// <returns></returns>
    IEnumerator End()
    {
        yield return new WaitForSeconds(m_waitTime);
        m_waitTime = 0;
        UnitRest();
        StageManager.Instance.NextUnit();
    }
    /// <summary>
    /// 移動終了時に呼ぶ
    /// </summary>
    protected void MoveModeEnd()
    {
        m_moveMode = false;
    }
    /// <summary>
    /// 攻撃終了時に呼ぶ
    /// </summary>
    protected void AttackModeEnd()
    {
        m_attackMode = false;
        m_attackWeapon = null;
    }
}
