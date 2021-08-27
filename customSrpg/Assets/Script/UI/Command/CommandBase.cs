﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CommandBase : MonoBehaviour
{
    public int CommandID { get; private set; }
    public int SelectNumber { get; private set; }
    protected bool m_select;
    CommandBase m_parent;
    protected CommandCursorMove m_commandMove;
    ViewCommandControl m_commandControl;
    public void CursorMove(Vector2 dir)
    {
        m_commandMove.CursorMove(this, dir);
    }
    public virtual void NextCommand()
    {
        m_commandControl.Next();
    }
    public virtual void BackCommand()
    {
        m_commandControl.Back();
    }
    public virtual void Decide()
    {

    }
    public virtual void Cancel()
    {

    }
}
