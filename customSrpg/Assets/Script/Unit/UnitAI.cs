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
    protected MapData[] GetAttackPositions(MapData map, WeaponMaster weapon) => 
        MapManager.Instance.StartSearch(map.PosX, map.PosZ, weapon);
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
