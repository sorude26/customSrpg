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
    }
    protected override void CursorMove()
    {
        
    }
}
