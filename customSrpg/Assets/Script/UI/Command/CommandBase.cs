using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CommandBase : MonoBehaviour
{
    public event Action<int> OnClickEvent;
    public int CommandID { get; private set; }
    public int SelectNumber { get; private set; }
    protected bool m_select;
    CommandBase m_parent;
    CommandBase[] m_children;
    
    public virtual void OnClickThis()
    {
        OnSelect();
        OnClickEvent?.Invoke(CommandID);
    }
    public virtual void OnSelect()
    {
        if (m_select) { return; }
        m_select = true;
    }
    public virtual void SelectOut()
    {
        if (!m_select) { return; }
        m_select = false;
    }
    public virtual void NextCommand()
    {

    }
    public virtual void BackCommand()
    {

    }
    public virtual void Decide()
    {

    }
    public virtual void Cancel()
    {

    }
    public virtual void InSelectChild(int number)
    {
        m_children[number].OnSelect();
    }
    public virtual void OutSelectForParent()
    {
        SelectOut();
        m_parent.OnSelect();
    }
}
