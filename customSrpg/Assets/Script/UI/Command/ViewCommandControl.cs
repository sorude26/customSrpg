using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCommandControl : CommandButton<ViewCommandControl>
{
    [SerializeField] CommandBox m_commandPrefab;
    [SerializeField] Vector2 m_commandSpan = Vector2.down;
    [SerializeField] Vector2 m_commandStartPos = Vector2.zero;
    [SerializeField] int m_commandNum = 5;
    int m_selectNum = 0;
    Vector2[] m_commandPos;
    CommandBox[] m_commands;
    RectTransform m_basePos;
    private void Start()
    {
        m_basePos = GetComponent<RectTransform>();
        StartSet();
    }
    public void StartSet()
    {
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
    }
    public void Next()
    {
        m_selectNum++;
        if (m_selectNum > m_commandNum - 1)
        {
            m_selectNum = 0;
            m_basePos.localPosition = Vector2.zero;
            return;
        }
        m_basePos.localPosition = (Vector2)m_basePos.localPosition - m_commandSpan;
    }
    public void Back()
    {
        m_selectNum--;
        if (m_selectNum < 0)
        {
            m_selectNum = m_commandNum - 1;
            m_basePos.localPosition = (Vector2)m_basePos.localPosition - m_commandSpan * m_selectNum;
            return;
        }
        m_basePos.localPosition = (Vector2)m_basePos.localPosition + m_commandSpan;
    }

    public override void StartSet(int id, Action<int> action)
    {
    }

    public override ViewCommandControl SelectCommand()
    {
        return this;
    }

    public override void OutCommand()
    {
    }
}
