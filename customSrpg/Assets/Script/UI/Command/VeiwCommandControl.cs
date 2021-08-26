using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeiwCommandControl : MonoBehaviour
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
        m_commands[m_selectNum -1].GetComponent<RectTransform>().localPosition = m_commandSpan * (m_commandNum -m_selectNum - 1) + m_commandStartPos;
    }
    public void Back()
    {
        m_selectNum--;
        if (m_selectNum < 0)
        {
            m_selectNum = m_commandNum - 1;
            m_basePos.localPosition = Vector2.zero;
            return;
        }
        m_basePos.localPosition = (Vector2)m_basePos.localPosition + m_commandSpan;
        m_commands[m_selectNum + 1].GetComponent<RectTransform>().localPosition = m_commandStartPos;
    }
}
