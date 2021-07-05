using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotWepon : WeaponMaster
{
    [SerializeField] Transform m_muzzle;
    [SerializeField] GameObject m_muzzleFlash;
    public override void AttackStart()
    {
        StartCoroutine(Shot());
    }
    IEnumerator Shot()
    {
        m_muzzleFlash.SetActive(true);
        int count = m_partsData.MaxAttackNumber;
        while (count > 0)
        {
            count--;
            m_attack?.Invoke();
            m_attack = null;
            EffectManager.PlayEffect(EffectType.Shot, m_muzzle.position);
            yield return new WaitForSeconds(m_partsData.AttackInterval);
        }
        m_muzzleFlash.SetActive(false);
        m_attackEnd?.Invoke();
        m_attackEnd = null;
    }
}
