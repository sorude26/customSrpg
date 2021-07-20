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
    public virtual void Wait()
    {
        m_anime.Play("HumanWait");
    }
    public virtual void RArmAttack(WeaponType weapon)
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
                AttackSlashRArm();
                break;
            default:
                break;
        }
    }
    public virtual void LArmAttack(WeaponType weapon)
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
                AttackSlashLArm();
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 右手武器で攻撃する
    /// </summary>
    public virtual void TargetShotRArm()
    {
        m_anime.Play("HumanAttackRArm");
    }
    public virtual void AttackPunchRArm()
    {
        m_anime.Play("HumanAttackPunchRArm");
    }
    public virtual void AttackSlashRArm()
    {
        m_anime.Play("HumanAttackSlashRArm");
    }
    public virtual void TargetShotLArm()
    {
        m_anime.Play("HumanAttackLArm");
    }
    public virtual void AttackPunchLArm()
    {
        m_anime.Play("HumanAttackPunchLArm");
    }
    public virtual void AttackSlashLArm()
    {
        m_anime.Play("HumanAttackSlashLArm");
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

