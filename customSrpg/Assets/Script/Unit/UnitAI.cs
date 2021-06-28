﻿using System.Linq;
using System;
using UnityEngine;

[CreateAssetMenu]
public class UnitAI : ScriptableObject
{
    public virtual void TargetPointSet(Unit unit)
    {
        var map = MapManager.Instance.StartSearch(unit).ToList();
        map.ForEach(p => p.MapScore = 0);
        map.ForEach(p => SetMapScore(unit, p));
    }
    public virtual void StartMove(Unit unit)
    {
        TargetPointSet(unit);
        var target = MapManager.Instance.StartSearch(unit).ToList().OrderByDescending(t => t.MapScore).FirstOrDefault();
        Debug.Log(target.PosX + "," + target.PosZ + unit);
        unit.TargetMoveStart(target.PosX, target.PosZ);
    }
    public virtual WeaponMaster StartAttack(Unit unit)
    {
        WeaponMaster attackWeapon = null;
        var weapons = unit.GetUnitData().GetWeapons().ToList().OrderByDescending(weapon => weapon.Power);
        Unit target = null;
        foreach (var weapon in weapons)
        {
            int hit = BattleManager.Instance.GetHit(unit.GetUnitData().GetWeaponPosition(weapon),unit);
            target = BattleManager.Instance.GetAttackTargets(MapManager.Instance.StartSearch(unit.CurrentPosX, unit.CurrentPosZ, weapon))
            .ToList().Where(u => u.State == UnitState.Stop).OrderByDescending(s => s.GetScore(weapon.Power, hit)).FirstOrDefault();
            if (target)
            {
                BattleManager.Instance.SetTarget(target);
                BattleManager.Instance.SetAttacker(unit);
                BattleManager.Instance.AttackStart(unit.GetUnitData().GetWeaponPosition(weapon));
                attackWeapon = weapon;
                break;
            }
        }
        return attackWeapon;
    }
    /// <summary>
    /// 地点からの武器の攻撃範囲を返す
    /// </summary>
    /// <param name="map"></param>
    /// <param name="weapon"></param>
    /// <returns></returns>
    protected MapData[] GetAttackPositions(MapData map, WeaponMaster weapon) =>
        MapManager.Instance.StartSearch(map.PosX, map.PosZ, weapon);
    /// <summary>
    /// 攻撃力の最も高い武器、攻撃範囲の最も広い武器の順に攻撃可能範囲を検索し最も高い得点をマップに保存する
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="point"></param>
    protected virtual void SetMapScore(Unit unit, MapData point)
    {
        int hit = BattleManager.Instance.GetHit(unit.GetUnitData().GetWeaponPosition(unit.GetUnitData().GetMaxPowerWeapon()),unit);
        int power = unit.GetUnitData().GetMaxPowerWeapon().Power;
        Unit target = GetTarget( point, unit.GetUnitData().GetMaxPowerWeapon(), hit);
        if (target)
        {
            SetScore(point, target.GetScore(power, hit));
            return;
        }
        hit = BattleManager.Instance.GetHit(unit.GetUnitData().GetWeaponPosition(unit.GetUnitData().GetMaxRangeWeapon()), unit);
        power = unit.GetUnitData().GetMaxRangeWeapon().Power;
        target = GetTarget( point, unit.GetUnitData().GetMaxRangeWeapon(), hit);
        if (target)
        {
            SetScore(point, target.GetScore(power, hit));
            return;
        }
        point.MapScore = 0;
    }
    /// <summary>
    /// スコア最大の停止状態ユニットを返す
    /// </summary>
    /// <param name="point"></param>
    /// <param name="weapon"></param>
    /// <param name="hit"></param>
    /// <returns></returns>
    protected Unit GetTarget(MapData point,WeaponMaster weapon,int hit)
    {
        Unit target = BattleManager.Instance.GetAttackTargets(GetAttackPositions(point, weapon))
            .ToList().Where(u => u.State == UnitState.Stop).OrderByDescending(s => s.GetScore(weapon.Power, hit)).FirstOrDefault();
        return target;
    }
    /// <summary>
    /// 地点のスコアと比較し、大きいスコアを登録する
    /// </summary>
    /// <param name="point"></param>
    /// <param name="score"></param>
    protected void SetScore(MapData point, int score)
    {
        if (point.MapScore < score) 
        {
            point.MapScore = score;           
        } 
    }
}
