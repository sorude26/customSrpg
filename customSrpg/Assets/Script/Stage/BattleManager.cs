﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 戦闘処理担当
/// </summary>
public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }
    [SerializeField] BattleCalculator m_calculator;
    /// <summary> 攻撃者 </summary>
    Unit m_attacker;
    /// <summary> 攻撃対象リスト </summary>
    List<Unit> m_attackTarget = new List<Unit>();
    /// <summary> 入手用攻撃対象リスト </summary>
    public List<Unit> AttackTarget { get => m_attackTarget; }
    /// <summary> 攻撃対象 </summary>
    Unit m_target;
    /// <summary> 攻撃中フラグ </summary>
    bool m_attackNow;
    private void Awake()
    {
        Instance = this;
    }
    /// <summary>
    /// 攻撃者の設定
    /// </summary>
    /// <param name="attacker"></param>
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
    /// <summary>
    /// 範囲内のユニットを配列で返す
    /// </summary>
    /// <param name="targetPos"></param>
    /// <returns></returns>
    public Unit[] GetAttackTargets(MapData[] targetPos)
    {
        List<Unit> units = new List<Unit>();
        var targetUnit = StageManager.Instance.GetStageUnits();
        foreach (var unit in targetUnit)
        {
            var t = targetPos.Where(px => px.PosX == unit.CurrentPosX).Where(pz => pz.PosZ == unit.CurrentPosZ).FirstOrDefault();
            if (t != null)
            {
                units.Add(unit);
            }
        }
        return units.ToArray();
    }
    /// <summary>
    /// 攻撃対象設定
    /// </summary>
    /// <param name="targetNum"></param>
    /// <returns></returns>
    public Unit SetTarget(int targetNum)
    {
        if (targetNum >= m_attackTarget.Count)
        {
            Debug.Log("指定対象不在");
            m_target = null;
            return null;
        }
        m_target = m_attackTarget[targetNum];
        return m_target;
    }
    public void SetTarget(Unit target)
    {
        m_target = target;
    }
    /// <summary>
    /// 指定した武器で攻撃を開始する
    /// </summary>
    /// <param name="attackWeapon"></param>
    public void AttackStart(WeaponPosition attackWeapon)
    {
        if (m_attackNow)
        {
            return;
        }
        EventManager.StageGuideViewEnd();
        if (!m_target)
        {
            Debug.Log("攻撃対象不在");
            return;
        }
        m_attackNow = true;
        m_attacker.TargetLook(m_target);
        m_target.TargetLook(m_attacker);
        WeaponMaster weapon = m_attacker.GetUnitData().GetWeapon(attackWeapon);
        m_target.GetUnitData().SetBattleEvent(weapon);
        m_target.GetUnitData().BattleEnd += AttackEnd;
        int hit = GetHit(attackWeapon);
        for (int i = 0; i < weapon.MaxAttackNumber; i++)
        {
            Attack(m_target, hit, weapon.Power);
        }
        weapon.AttackStart();
    }
    /// <summary>
    /// 単発攻撃処理
    /// </summary>
    /// <param name="target"></param>
    /// <param name="hit"></param>
    /// <param name="power"></param>
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
    /// <summary>
    /// 武装の命中率を返す
    /// </summary>
    /// <param name="attackWeapon"></param>
    /// <returns></returns>
    public int GetHit(WeaponPosition attackWeapon) => 
        m_calculator.GetHit(m_attacker.GetUnitData().GetHitAccuracy(attackWeapon), m_target.GetUnitData().GetAvoidance());
    public int GetHit(WeaponPosition attackWeapon, Unit attacker) =>
        attacker.GetUnitData().GetHitAccuracy(attackWeapon);
    public int GetHit(int hit, int accuracy) =>
        m_calculator.GetHit(hit, accuracy);
    /// <summary>
    /// 攻撃終了時の処理
    /// </summary>
    void AttackEnd() 
    { 
        m_attackNow = false;
        m_target.GetUnitData().BattleEnd -= AttackEnd;
    }
    public int GetPointDurable(int maxHP, int currentHP) => m_calculator.GetPointDurable(maxHP, currentHP);
    public int GetPointDamage(int damage,int currentHP) => m_calculator.GetPointDamage(damage,currentHP);
}
