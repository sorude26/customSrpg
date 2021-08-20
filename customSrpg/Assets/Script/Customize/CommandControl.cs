using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandControl : MonoBehaviour
{
    [Tooltip("コマンドボタンのPrefab")]
    [SerializeField] CommandBox m_commandBox;
    [Tooltip("コマンドの配置")]
    [SerializeField] Transform[] m_commandsPos;
    [Tooltip("コマンド名")]
    [SerializeField] string[] m_commands;
    public CommandBox[] StartSet()
    {
        List<CommandBox> commands = new List<CommandBox>();
        for (int i = 0; i < m_commandsPos.Length; i++)
        {
            if (m_commands.Length <= i)
            {
                break;
            }
            var command = Instantiate(m_commandBox,this.transform);
            command.SetText(m_commands[i]);
            command.transform.position = m_commandsPos[i].position;
            commands.Add(command);
        }
        return commands.ToArray();
    }
}
