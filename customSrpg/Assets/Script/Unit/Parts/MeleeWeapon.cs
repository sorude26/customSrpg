using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 近接攻撃武器
/// </summary>
public class MeleeWeapon : WeaponMaster
{
    [SerializeField] Transform m_blade;
    public override void AttackStart()
    {
        m_attackStart?.Invoke(Type);
        StartCoroutine(Attack());
    }
    IEnumerator Attack()
    {
        int count = m_partsData.MaxAttackNumber;
        while (count > 0)
        {
            count--;
            m_attack?.Invoke();
            EffectManager.PlayEffect(EffectType.ShotHit, m_blade.position);
            yield return new WaitForSeconds(m_partsData.AttackInterval);
        }
        m_attackEnd?.Invoke();
        m_attackEnd = null;
        m_attack = null;
    }
}
