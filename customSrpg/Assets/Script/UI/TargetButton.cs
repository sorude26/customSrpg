using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetButton : ButtonMaster
{
    List<Unit> m_targets;
    public override void Open()
    {
        gameObject.SetActive(true);
        m_buttonNum = 0;
        m_targets = BattleManager.Instance.AttackTarget; 
    }
    public override void CursorUp() { }
    public override void CursorDown() { }
    public override void CursorLeft()
    {
        if (m_buttonNum > 0)
        {
            m_buttonNum--;
        }
        else
        {
            m_buttonNum = m_targets.Count - 1;
        }
        CursorMove();
    }
    public override void CursorRight()
    {
        if (m_buttonNum < m_targets.Count - 1)
        {
            m_buttonNum++;
        }
        else
        {
            m_buttonNum = 0;
        }
        CursorMove();
    }
    protected override void CursorMove()
    {
        if (m_targets != null && m_targets.Count > 0)
        {
            StageManager.Instance.CursorWap(m_targets[m_buttonNum].CurrentPosX, m_targets[m_buttonNum].CurrentPosZ);
        }
    }
    public override void Decision()
    {
        if (m_targets != null && m_targets.Count > 0)
        {
            BattleManager.Instance.SetTarget(m_targets[m_buttonNum]);
            BattleManager.Instance.AttackStart();
        }
    }
}
