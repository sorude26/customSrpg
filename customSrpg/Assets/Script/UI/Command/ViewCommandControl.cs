using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIControl
{
    public class ViewCommandControl : CommandBox
    {
        [SerializeField] CommandBox m_commandPrefab;
        [SerializeField] Vector2 m_commandSpan = Vector2.down;
        [SerializeField] Vector2 m_commandStartPos = Vector2.zero;
        [SerializeField] int m_commandNum = 5;
        int m_selectNum = 0;
        Vector2[] m_commandPos;
        CommandBox[] m_commands;
        RectTransform m_basePos;
        [SerializeField] ViewCommandControl[] m_commandControls;
        [SerializeField] CommandAction m_action;
        [SerializeField] string m_name;
        public override void StartSet(int id, Action<int> action)
        {           
            CommandID = id;
            if (m_action != null)
            {
                StartSet();
                return;
            }
            m_commandPos = new Vector2[m_commandControls.Length];
            List<CommandBox> commands = new List<CommandBox>();
            Vector2 commandPos = m_commandStartPos;
            for (int i = 0; i < m_commandControls.Length; i++)
            {
                m_commandPos[i] = commandPos;
                var command = Instantiate(m_commandControls[i], transform);
                command.GetComponent<RectTransform>().localPosition = commandPos;
                command.StartSet(i, action);
                commandPos += m_commandSpan;
                commands.Add(command);
            }
            m_commands = commands.ToArray();
            SetText(m_name);
            m_basePos = GetComponent<RectTransform>();
        }
        public void StartSet()
        {
            m_commandNum = m_action.CommandNum;
            m_commandPos = new Vector2[m_commandNum];
            List<CommandBox> commands = new List<CommandBox>();
            Vector2 commandPos = m_commandStartPos;
            for (int i = 0; i < m_commandNum; i++)
            {
                m_commandPos[i] = commandPos;
                var command = Instantiate(m_commandPrefab, transform);
                command.GetComponent<RectTransform>().localPosition = commandPos;
                commandPos += m_commandSpan;
                commands.Add(command);
            }
            m_commands = commands.ToArray();
            m_action.SetData(m_commands);
            SetText(m_name);
            m_basePos = GetComponent<RectTransform>();
        }
        public void Next()
        {
            SelectOut();
            m_selectNum++;
            if (m_selectNum > m_commandNum - 1)
            {
                m_selectNum = 0;
                m_basePos.localPosition = Vector2.zero;
            }
            else
            {
                m_basePos.localPosition = (Vector2)m_basePos.localPosition - m_commandSpan;
            }
            SelectOn();
        }
        public void Back()
        {
            SelectOut();
            m_selectNum--;
            if (m_selectNum < 0)
            {
                m_selectNum = m_commandNum - 1;
                m_basePos.localPosition = (Vector2)m_basePos.localPosition - m_commandSpan * m_selectNum;
            }
            else
            {
                m_basePos.localPosition = (Vector2)m_basePos.localPosition + m_commandSpan;
            }
            SelectOn();
        }
        void SelectOut()
        {
            m_commands[m_selectNum].OutCommand();
        }
        void SelectOn()
        {
            m_commands[m_selectNum].SelectCommand();
        }
        public override void OnClickCommand()
        {
            m_commands[m_selectNum].OnClickCommand();
        }

        public override CommandBox SelectCommand()
        {
            gameObject.SetActive(true);
            return this;
        }

        public override void OutCommand()
        {
            gameObject.SetActive(false);
        }
    }
}