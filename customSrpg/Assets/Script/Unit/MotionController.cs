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
    public void StartSet()
    {
        m_anime = GetComponent<Animator>();
        Wait();
    }
    /// <summary>
    /// 待機状態
    /// </summary>
    public virtual void Wait()
    {
        m_anime.Play("HumanWait");
    }
    /// <summary>
    /// 右手の攻撃
    /// </summary>
    /// <param name="weapon"></param>
    /// <param name="number"></param>
    public virtual void RArmAttack(WeaponType weapon,int number)
    {
        switch (weapon)
        {
            case WeaponType.Rifle:
            case WeaponType.MachineGun:
            case WeaponType.Shotgun:
            case WeaponType.MShotGun:
                TargetShotRArm();
                break;
            case WeaponType.Knuckle:
                AttackPunchRArm();
                break;
            case WeaponType.Blade:
                AttackSlashRArm(number);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 左手の攻撃
    /// </summary>
    /// <param name="weapon"></param>
    /// <param name="number"></param>
    public virtual void LArmAttack(WeaponType weapon,int number)
    {
        switch (weapon)
        {
            case WeaponType.Rifle:
            case WeaponType.MachineGun:
            case WeaponType.Shotgun:
            case WeaponType.MShotGun:
                TargetShotLArm();
                break;
            case WeaponType.Knuckle:
                AttackPunchLArm();
                break;
            case WeaponType.Blade:
                AttackSlashLArm(number);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 右手射撃武器で攻撃する
    /// </summary>
    public virtual void TargetShotRArm()
    {
        m_anime.Play("HumanAttackRArm");
    }
    /// <summary>
    /// 右手で殴る
    /// </summary>
    public virtual void AttackPunchRArm()
    {
        m_anime.Play("HumanAttackPunchRArm");
    }
    /// <summary>
    /// 攻撃の回数に応じた切り裂き系の格闘攻撃をする
    /// </summary>
    /// <param name="number"></param>
    public virtual void AttackSlashRArm(int number)
    {
        if (number == 0)
        {
            m_anime.Play("HumanWalk");
        }
        else if (number % 2 == 0)
        {
            m_anime.Play("HumanAttackSlashRArm2");
        }
        else
        {
            m_anime.Play("HumanAttackSlashRArm");
        }
    }
    /// <summary>
    /// 左手射撃武器で攻撃する
    /// </summary>
    public virtual void TargetShotLArm()
    {
        m_anime.Play("HumanAttackLArm");
    }
    /// <summary>
    /// 左手で殴る
    /// </summary>
    public virtual void AttackPunchLArm()
    {
        m_anime.Play("HumanAttackPunchLArm");
    }
    /// <summary>
    /// 攻撃の回数に応じた切り裂き系の格闘攻撃をする
    /// </summary>
    /// <param name="number"></param>
    public virtual void AttackSlashLArm(int number)
    {
        if (number == 0)
        {
            m_anime.Play("HumanWalk");
        }
        else if (number % 2 == 0)
        {
            m_anime.Play("HumanAttackSlashLArm2");
        }
        else
        {
            m_anime.Play("HumanAttackSlashLArm");
        }
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
    public void Destroy()
    {
        m_anime.Play("HumanDestroy");
    }
    void DestroyEnd()
    {
        EffectManager.PlayEffect(EffectType.ExplosionUnit, transform.position);
        gameObject.SetActive(false);
    }
}

