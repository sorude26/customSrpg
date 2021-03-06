using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 射撃武器
/// </summary>
public class ShotWeapon : WeaponMaster
{
    [Tooltip("銃口")]
    [SerializeField] Transform m_muzzle;
    [Tooltip("マズルフラッシュのエフェクト")]
    [SerializeField] GameObject m_muzzleFlash;
    public override void AttackStart()
    {
        m_attackStart?.Invoke();
        m_attackStart = null;
        m_attackMode?.Invoke(Type,0);
        StartCoroutine(Shot());
    }
    IEnumerator Shot()
    {
        yield return new WaitForSeconds(0.5f);
        m_muzzleFlash.SetActive(true);
        int count = m_partsData.MaxAttackNumber[m_partsID];
        while (count > 0)
        {
            count--;
            m_attack?.Invoke();
            EffectManager.PlayEffect(EffectType.Shot, m_muzzle.position);
            yield return new WaitForSeconds(m_partsData.AttackInterval[m_partsID]);
        }
        m_muzzleFlash.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        m_attackEnd?.Invoke();
        m_attackEnd = null;
        m_attack = null;
    }
}
