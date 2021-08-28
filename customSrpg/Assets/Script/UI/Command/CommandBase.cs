using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIControl
{
    [RequireComponent(typeof(ViewCommandControl))]
    public partial class CommandBase : MonoBehaviour
    {
        [SerializeField] int m_type = 0;
        public int CommandID { get; private set; }
        protected bool m_select;
        protected CommandCursorMove m_commandMove;
        protected CommandCursorMove[] m_moveType = { new NoneMove(), new MoveUDOnly(), new MoveLRInD(), new MoveUDInR(), new MoveLROutUD(), new MoveLRInDOutU() };
        ViewCommandControl m_selectControl;
        public void StartSet()
        {
            m_commandMove = m_moveType[m_type];
            m_selectControl = GetComponent<ViewCommandControl>();
            m_selectControl.StartSet(CommandID, SelectControl);
        }
        public void CursorMove(float x,float y)
        {
            Vector2 dir = new Vector2(x, y);
            m_commandMove?.CursorMove(this, dir);
        }
        public virtual void NextCommand()
        {
            m_selectControl?.Next();
        }
        public virtual void BackCommand()
        {
            m_selectControl?.Back();
        }
        public virtual void Decide()
        {

        }
        public virtual void Cancel()
        {

        }
        public virtual void SelectControl(int num)
        {

        }
    }
}
