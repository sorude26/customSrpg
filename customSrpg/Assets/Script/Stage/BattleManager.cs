using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 戦闘処理担当
/// </summary>
public class BattleManager : MonoBehaviour
{
    List<Unit> m_attackTarget;
    public void AttackTarget()
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
}
