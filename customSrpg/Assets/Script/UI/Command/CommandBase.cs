using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIControl
{
    [RequireComponent(typeof(ViewCommandControl))]
    public partial class CommandBase : MonoBehaviour
    {
        [SerializeField] string m_name;
        [SerializeField] int m_moveType = 0;
        [SerializeField] int m_setType = 0;
        protected CommandSet[] m_sets = { new DecOn(), new DecInCanOut(), new DecMessCanOut() };
        protected CommandCursorMove[] m_moveTypes = { new NoneMove(), new MoveUDOnly(), new MoveLRInD(), 
            new MoveUDInR(), new MoveLROutUD(), new MoveLRInDOutU(), new AllOut() ,new MoveUDInROutL(), new MoveLROnly()};
        public ViewCommandControl SelectController { get; private set; }
        CommandBase m_parent;
        [SerializeField] CommandBase[] m_defaultCommandes;
        [SerializeField] CommandBase m_protoCommande;
        [SerializeField] CommandAction m_action;
        CommandBase[] m_children;
        public void StartSet()
        {
            SelectController = GetComponent<ViewCommandControl>();
            if (m_action && m_protoCommande)
            {
                StartSetAction();
            }
            else if(m_defaultCommandes.Length > 0)
            {
                StartSetDefault();
            }
            SelectOut();
        }
        public void StartSetDefault()
        {
            m_children = new CommandBase[m_defaultCommandes.Length];
            for (int i = 0; i < m_defaultCommandes.Length; i++)
            {
                m_children[i] = Instantiate(m_defaultCommandes[i]);
                m_children[i].transform.SetParent(SelectController.BasePos);
            }
            foreach (var item in m_children)
            {
                item.SetParent(this);
                item.StartSet();
            }
            SelectController.StartSet(m_children);
            SelectController.SetText(m_name);
        }
        public void StartSetAction()
        {
            m_children = new CommandBase[m_action.CommandNum];
            for (int i = 0; i < m_action.CommandNum; i++)
            {
                m_children[i] = Instantiate(m_protoCommande);
                m_children[i].transform.SetParent(SelectController.BasePos);
            }
            foreach (var item in m_children)
            {
                item.SetParent(this);
                item.StartSet();
            }
            SelectController.StartSet(m_children);
            m_action.SetData(m_children);
            SelectController.SetText(m_name);
        }
        public void SetParent(CommandBase parent)
        {
            m_parent = parent;
        }
        public void CursorMove(float x,float y)
        {
            Vector2 dir = new Vector2(x, y);
            m_moveTypes[m_moveType]?.CursorMove(this, dir);
        }
        public virtual void CommandDecide()
        {
            SelectController.OnClickCommand();
        }
        public virtual void NextCommand()
        {
            SelectController?.Next();
        }
        public virtual void BackCommand()
        {
            SelectController?.Back();
        }
        public virtual void Decide()
        {
            m_sets[m_setType].Decide(this);
        }
        public virtual void Cancel()
        {
            m_sets[m_setType].Cancel(this);
        }
        public virtual void MoveChild()
        {
            m_children[SelectController.SelectNum].SelectCommand();
        }
        public virtual void MoveParent()
        {
            SelectOut();
            m_parent.SelectCommand();
        }
        public virtual void OpenMessage()
        {

        }
        public virtual void SelectCommand()
        {
            CommandBaseControl.Instance.SetAction(CursorMove, CommandDecide);
            SelectController.SelectCommand();
        }
        public virtual void SelectOut()
        {
            SelectController.OutCommand();
        }
    }
}
