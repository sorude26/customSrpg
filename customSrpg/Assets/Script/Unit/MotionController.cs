using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 機体ごとにモーションを変更可能にする
/// </summary>
public class MotionController : MonoBehaviour
{
    UnitType m_unitType = UnitType.Human;
    private Animator m_anime;
    private List<Animator> m_childAnime;
    public void SetAnime(Animator anime,UnitType unitType)
    {
        m_anime = anime;
        m_unitType = unitType;
        Wait();
    }
    public void StartSet()
    {
        m_anime = GetComponent<Animator>();
        Wait();
    }
    public void SetChildAnime(Animator[] animes)
    {
        m_childAnime = new List<Animator>();
        foreach (var anime in animes)
        {
            m_childAnime.Add(anime);
        }
        Wait();
    }
    /// <summary>
    /// 待機状態
    /// </summary>
    public virtual void Wait()
    {
        switch (m_unitType)
        {
            case UnitType.Human:
                m_anime.Play("HumanWait");
                break;
            case UnitType.Walker:
                m_anime.Play("HumanWait");
                break;
            case UnitType.Helicopter:
                break;
            case UnitType.Tank:
                break;
            case UnitType.Giant:
                m_anime.Play("Wait");
                break;
            default:
                break;
        }
        if (m_childAnime != null)
        {
            foreach (var anime in m_childAnime)
            {
                anime.Play("Wait");
            }
        }
    }

    public virtual void BodyAttack(WeaponType weapon, int number)
    {
        switch (m_unitType)
        {
            case UnitType.Human:
                m_anime.Play("WalkerAttack");
                break;
            case UnitType.Walker:
                m_anime.Play("WalkerAttack");
                break;
            case UnitType.Helicopter:
                break;
            case UnitType.Tank:
                break;
            case UnitType.Giant:
                m_anime.Play("ArmAttack");
                break;
            default:
                break;
        }
    }
    public virtual void ShoulderAttack(WeaponType weapon, int number)
    {
        switch (m_unitType)
        {
            case UnitType.Human:
                m_anime.Play("WalkerAttack");
                break;
            case UnitType.Walker:
                m_anime.Play("WalkerAttack");
                break;
            case UnitType.Helicopter:
                break;
            case UnitType.Tank:
                break;
            case UnitType.Giant:
                m_anime.Play("CannonAttack");
                break;
            default:
                break;
        }
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
        switch (m_unitType)
        {
            case UnitType.Human:
                m_anime.Play("HumanWalk");
                break;
            case UnitType.Walker:
                m_anime.Play("HumanWalk");
                break;
            case UnitType.Helicopter:
                break;
            case UnitType.Tank:
                break;
            case UnitType.Giant:
                m_anime.Play("Walk");
                break;
            default:
                break;
        }if (m_childAnime != null)
        {
            foreach (var anime in m_childAnime)
            {
                anime.Play("Walk");
            }
        }
    }
    public void GuardRArm()
    {
        m_anime.Play("HumanGuardRArmWait");
    }

    public void Damage()
    {
        if (m_unitType == UnitType.Human)
        {
            m_anime.Play("HumanWaitDamage");
        }
    }
    public void HeavyDamage()
    {
        m_anime.Play("HumanWaitHeavyDamage");
    }
    public void Destroy()
    {
        switch (m_unitType)
        {
            case UnitType.Human:
                m_anime.Play("HumanDestroy");
                break;
            case UnitType.Walker:
                m_anime.Play("HumanDestroy");
                break;
            case UnitType.Helicopter:
                break;
            case UnitType.Tank:
                break;
            case UnitType.Giant:
                m_anime.Play("Destroy");
                break;
            default:
                break;
        }
    }
    public void Idle()
    {
        m_anime.Play("Idle");
    }
    void DestroyEnd()
    {
        EffectManager.PlayEffect(EffectType.ExplosionUnit, transform.position);
        gameObject.SetActive(false);
    }
}

