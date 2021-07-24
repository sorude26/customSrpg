using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 近接攻撃武器
/// </summary>
public class MeleeWeapon : WeaponMaster
{
    [SerializeField] Transform m_blade;
    [SerializeField] float m_attackHitTime = 0.3f;
    public override void AttackStart()
    {
        m_attackStart?.Invoke();
        m_attackStart = null;
        m_attackMode?.Invoke(Type,0);
        StartCoroutine(Attack());
    }
    IEnumerator Attack()
    {
        int count = 1;
        while (count <= m_partsData.MaxAttackNumber)
        {           
            m_attackMode?.Invoke(Type, count);
            yield return new WaitForSeconds(m_attackHitTime);
            m_attack?.Invoke();
            EffectManager.PlayEffect(EffectType.ShotHit, m_blade.position);
            count++;
            yield return new WaitForSeconds(m_partsData.AttackInterval);
        }
        m_attackEnd?.Invoke();
        m_attackEnd = null;
        m_attack = null;
    }
}
