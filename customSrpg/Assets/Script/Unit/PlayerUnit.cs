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
                m_waitTime = 0.1f;
                StartCoroutine(StartAI());
            }
            else
            {
                StageManager.Instance.OpenCommand();
            }
        }
    }
    /// <summary>
    /// マニュアル時の行動終了処理
    /// </summary>
    public void ActionEnd()
    {
        if (m_autoMode || State != UnitState.Action)
        {
            return;
        }
        m_waitTime = m_actionEndWaitTime;
        StartCoroutine(End());
    }
    public void ChangeMode()
    {
        if (State != UnitState.Stop)
        {
            return;
        }
        if (m_autoMode)
        {
            m_autoMode = false;
        }
        else
        {
            m_autoMode = true;
        }
    }
}
