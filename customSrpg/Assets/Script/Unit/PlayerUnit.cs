using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : NpcUnit
{
    [SerializeField] protected bool m_autoMode;
    public override void StartUp()
    {
        if (State == UnitState.StandBy)
        {
            State = UnitState.Action;
            if (m_autoMode)
            {
                StartCoroutine(StartAI());
            }
            else
            {
                StageManager.Instance.OpenCommand();
            }
        }
    }
    public void ActionEnd()
    {
        if (m_attackMode || State != UnitState.Action)
        {
            return;
        }
        m_waitTime = m_actionEndWaitTime;
        StartCoroutine(End());
    }
}
