using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit
{
    public override void StartUp()
    {
        if (State == UnitState.StandBy)
        {
            State = UnitState.Action;
            StartCoroutine(StartAI());
        }
    }
    protected IEnumerator StartAI()
    {
        while (State == UnitState.Action)
        {

            yield return null;
        }
    }
}
