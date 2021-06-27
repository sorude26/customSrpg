using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAI : ScriptableObject
{
    public virtual void TargetPointSet(Unit unit)
    {
        MapManager.Instance.StartSearch(unit).ToList().ForEach(p => SetMapScore(unit, p));
    }
    public virtual void StartMove(Unit unit)
    {
        var target = MapManager.Instance.StartSearch(unit).ToList().OrderByDescending(t => t.MapScore).FirstOrDefault();
        unit.TargetMoveStart(target.PosX, target.PosZ);
    }
    public virtual void StartAttack(Unit unit)
    {

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
        int hit = BattleManager.Instance.GetHit(unit.GetUnitData().GetWeaponPosition(unit.GetUnitData().GetMaxPowerWeapon()));
        int power = unit.GetUnitData().GetMaxPowerWeapon().Power;
        Unit target = BattleManager.Instance.GetAttackTargets(GetAttackPositions(point, unit.GetUnitData().GetMaxPowerWeapon()))
            .ToList().Where(u => u.State == UnitState.Stop).OrderByDescending(s => s.GetScore(power, hit)).FirstOrDefault();
        if (target)
        {
            point.MapScore = target.GetScore(power, hit);
            return;
        }
        hit = BattleManager.Instance.GetHit(unit.GetUnitData().GetWeaponPosition(unit.GetUnitData().GetMaxRangeWeapon()));
        power = unit.GetUnitData().GetMaxRangeWeapon().Power;
        target = BattleManager.Instance.GetAttackTargets(GetAttackPositions(point, unit.GetUnitData().GetMaxRangeWeapon()))
            .ToList().Where(u => u.State == UnitState.Stop).OrderByDescending(s => s.GetScore(power, hit)).FirstOrDefault();
        if (target)
        {
            point.MapScore = target.GetScore(power, hit);
            return;
        }
        point.MapScore = 0;
    }
}
