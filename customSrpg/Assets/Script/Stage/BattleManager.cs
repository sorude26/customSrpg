﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 戦闘処理担当
/// </summary>
public class BattleManager : MonoBehaviour
{
    /// <summary> 攻撃者 </summary>
    Unit m_attacker;
    /// <summary> 攻撃対象リスト </summary>
    List<Unit> m_attackTarget;
    /// <summary> 攻撃対象 </summary>
    Unit m_target;
    public void SetAttacker(Unit attacker)
    {
        m_attacker = attacker;
    }
    /// <summary>
    /// 攻撃対象リストの登録をする
    /// </summary>
    public void SetAttackTargets()
    {
        m_attackTarget = new List<Unit>();
        var targetPos = StageManager.Instance.GetAttackTarget();
        var targetUnit = StageManager.Instance.GetStageUnits();
        foreach (var unit in targetUnit)
        {
            var t = targetPos.Where(px => px.PosX == unit.CurrentPosX).Where(pz => pz.PosZ == unit.CurrentPosZ).FirstOrDefault();
            if(t != null)
            {
                m_attackTarget.Add(unit);
            }
        }
        foreach (var item in m_attackTarget)
        {
            Debug.Log(item);
        }
    }
    public void SetTarget(Unit target)
    {
        m_target = target;
    }
    public void AttackStart(WeaponPosition attackWeapon)
    {
        WeaponMaster weapon = m_attacker.GetUnitData().GetWeapon(attackWeapon);
        int hit = GetHit(attackWeapon);
        for (int i = 0; i < weapon.MaxAttackNumber; i++)
        {
            Attack(m_target, hit, weapon.Power);
        }
    }
    void Attack(Unit target,int hit,int power)
    {
        int r = Random.Range(0, 100);
        if (r <= hit)
        {
            target.GetUnitData().HitCheckShot(power);
        }
        else
        {
            Debug.Log("Miss!");
        }
    }
    public int GetHit(WeaponPosition attackWeapon)
    {
        int hit = 50;
        hit += m_attacker.GetUnitData().GetHitAccuracy(attackWeapon);
        hit -= m_target.GetUnitData().GetAvoidance();
        if (hit > 99)
        {
            hit = 99;
        }
        else if (hit < 0)
        {
            hit = 0;
        }
        return hit;
    }
}
