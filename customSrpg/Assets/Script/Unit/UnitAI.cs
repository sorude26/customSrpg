using System.Linq;
using System;
using UnityEngine;

/// <summary>
/// NPCの基本的行動の基底クラス
/// </summary>
[CreateAssetMenu]
public class UnitAI : ScriptableObject
{
    /// <summary>
    /// 移動目標を設定する
    /// </summary>
    /// <param name="unit"></param>
    public virtual void TargetPointSet(Unit unit)
    {
        var map = MapManager.Instance.StartSearch(unit);
        foreach (var p in map)
        {
            p.MapScore = 0;
        }
        foreach (var p in map)
        {
            SetMapScore(unit, p);
        }
    }
    /// <summary>
    /// 現在位置以外で高得点の座標があればTrue
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    public virtual bool StartMove(Unit unit)
    {
        TargetPointSet(unit);
        var target = MapManager.Instance.StartSearch(unit).OrderByDescending(t => t.MapScore).FirstOrDefault();
        if (target.PosX == unit.CurrentPosX && target.PosZ == unit.CurrentPosZ)
        {
            return false;
        }
        MapManager.Instance.StartSearch(unit);
        unit.TargetMoveStart(target.PosX, target.PosZ);
        return true; 
    }
    /// <summary>
    /// 攻撃可能な最も攻撃力の高い武器を返す
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    public virtual WeaponMaster StartAttack(Unit unit)
    {
        WeaponMaster attackWeapon = null;
        var weapons = unit.GetUnitData().GetWeapons().OrderByDescending(weapon => weapon.MaxPower);
        Unit target = null;
        foreach (var weapon in weapons)
        {
            int hit = BattleManager.Instance.GetHit(unit.GetUnitData().GetWeaponPosition(weapon),unit);
            target = BattleManager.Instance.GetAttackTargets(MapManager.Instance.StartSearch(unit.CurrentPosX, unit.CurrentPosZ, weapon))
            .OrderByDescending(s => s.GetScore(weapon.MaxPower, hit)).FirstOrDefault();
            if (target)
            {
                BattleManager.Instance.SetTarget(target);
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
    /// 攻撃力の最も高い武器、攻撃範囲の最も広い武器の順に攻撃可能範囲を検索し、得点を登録する
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="point"></param>
    protected virtual void SetMapScore(Unit unit, MapData point)
    {
        if (SetMapScore(unit, point, unit.GetUnitData().GetWeaponPosition(unit.GetUnitData().GetMaxPowerWeapon()))) return;
        if (SetMapScore(unit, point, unit.GetUnitData().GetWeaponPosition(unit.GetUnitData().GetMaxRangeWeapon()))) return;
    }
    /// <summary>
    /// 指定箇所の武器での得点を登録可能ならば登録しTrueを返す
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="point"></param>
    /// <param name="weapon"></param>
    /// <returns></returns>
    protected virtual bool SetMapScore(Unit unit, MapData point,WeaponPosition weapon)
    {
        int hit = BattleManager.Instance.GetHit(weapon, unit);
        int power = unit.GetUnitData().GetWeapon(weapon).MaxPower;
        Unit target = GetTarget(point, unit.GetUnitData().GetWeapon(weapon), hit);
        if (target)
        {
            SetScore(point, target.GetScore(power, hit));
            return true;
        }
        return false;
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
            .OrderByDescending(s => s.GetScore(weapon.MaxPower, hit)).FirstOrDefault();
        return target;
    }
    /// <summary>
    /// 地点のスコアと比較し、大きいスコアを登録する
    /// </summary>
    /// <param name="point"></param>
    /// <param name="score"></param>
    protected void SetScore(MapData point, int score) 
    {
        if (point.MapScore < score) { point.MapScore = score; }
    }
}
