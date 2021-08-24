using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class CommandControl : MonoBehaviour,ICommand
{
    [Tooltip("コマンドボタンのPrefab")]
    [SerializeField] CommandBox m_commandBox;
    [Tooltip("コマンドの配置")]
    [SerializeField] Transform[] m_commandsPos;
    [Tooltip("コマンド名")]
    [SerializeField] string[] m_commandsName;
    CommandBox[] m_commands;
    int m_commandNum = 0;
    public CommandBox[] Commands { get => m_commands; }
    public void StartSet()
    {
        List<CommandBox> commands = new List<CommandBox>();
        for (int i = 0; i < m_commandsPos.Length; i++)
        {
            if (m_commandsName.Length <= i)
            {
                break;
            }
            var command = Instantiate(m_commandBox,this.transform);
            command.SetText(m_commandsName[i]);
            command.transform.position = m_commandsPos[i].position;
            commands.Add(command);
        }
        m_commands = commands.ToArray();
    }   

    public void SelectCommand()
    {
        m_commands[m_commandNum].SelectCommand();
    }
}
