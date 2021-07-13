using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 機体ごとにモーションを変更可能にする
/// </summary>
public class MotionController : MonoBehaviour
{
    [SerializeField] UnitType m_unitType;
    private Animator m_anime;
    private void Start()
    {
        m_anime = GetComponent<Animator>();
    }

    public virtual void Wait()
    {
        m_anime.Play("HumanWait");
    }
    /// <summary>
    /// 右手武器で攻撃する
    /// </summary>
    public virtual void TargetShotRArm()
    {
        switch (m_unitType)
        {
            case UnitType.Human:
                m_anime.Play("HumanAttackRArm");
                break;
            case UnitType.Walker:
                m_anime.Play("WalkerAttack");
                break;
            case UnitType.Helicopter:
                break;
            case UnitType.Tank:
                break;
            default:
                break;
        }
    }
    public virtual void TargetShotLArm()
    {
        m_anime.Play("HumanAttackLArm");
    }
    /// <summary>
    /// 歩行
    /// </summary>
    public virtual void Walk()
    {
        m_anime.Play("HumanWalk");
    }
    public void GuardRArm()
    {
        m_anime.Play("HumanGuardRArmWait");
    }

    public void Damage()
    {
        m_anime.Play("HumanWaitDamage");
    }
    public void HeavyDamage()
    {
        m_anime.Play("HumanWaitHeavyDamage");
    }
}

