using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIControl
{
    public class ViewCommandControl : CommandBox
    {
        [SerializeField] Vector2 m_commandSpan = Vector2.down;
        [SerializeField] Vector2 m_commandStartPos = Vector2.zero;
        [SerializeField] RectTransform m_rect;
        int m_maxCommandNumber;
        public int SelectNum { get; private set; }
        Vector2[] m_commandPos;
        public RectTransform BasePos { get => m_rect; }
        public override event Action<int> OnCommand;
        CommandBase[] m_commandBases;
        public override void StartSet(int id, Action<int> action)
        {
            CommandID = id;
            OnCommand += action;
        }
        public void StartSet(CommandBase[] commands)
        {
            m_maxCommandNumber = commands.Length;
            m_commandPos = new Vector2[m_maxCommandNumber];
            m_rect.localPosition = m_commandStartPos;
            Vector2 commandPos = Vector2.zero;
            for (int i = 0; i < m_maxCommandNumber; i++)
            {
                m_commandPos[i] = commandPos;
                commands[i].GetComponent<RectTransform>().localPosition = commandPos;
                commandPos += m_commandSpan;
            }
            m_commandBases = commands;
        }
        public void Next()
        {
            SelectOut();
            SelectNum++;
            if (SelectNum > m_maxCommandNumber - 1)
            {
                SelectNum = 0;
                BasePos.localPosition = m_commandStartPos;
            }
            else
            {
                BasePos.localPosition = (Vector2)BasePos.localPosition - m_commandSpan;
            }
            SelectOn();
        }
        public void Back()
        {
            SelectOut();
            SelectNum--;
            if (SelectNum < 0)
            {
                SelectNum = m_maxCommandNumber - 1;
                BasePos.localPosition = (Vector2)BasePos.localPosition - m_commandSpan * SelectNum;
            }
            else
            {
                BasePos.localPosition = (Vector2)BasePos.localPosition + m_commandSpan;
            }
            SelectOn();
        }
        void SelectOut()
        {
        }
        void SelectOn()
        {
        }
        public override void OnClickCommand()
        {
            OnCommand?.Invoke(CommandID);
        }

        public override void SelectCommand()
        {
            m_button.Select();
            m_rect.gameObject.SetActive(true);
        }

        public override void OutCommand()
        {
            m_rect.gameObject.SetActive(false);
        }
    }
}