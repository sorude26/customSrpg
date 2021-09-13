using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIControl;

public class TargetStage : CommandAction
{
    [SerializeField] int m_commandNum;
    public override int CommandNum => m_commandNum;

    public override void SetData(CommandBase[] commands)
    {
        for (int i = 0; i < m_commandNum; i++)
        {
            m_commandNames[i] = $"ステージ{i + 1}";
            var comand = commands[i].GetComponent<ViewCommandControl>();
            comand.SetText(m_commandNames[i]);
            comand.StartSet(i,SelectStageManager.Instance.SetStageData);
        }
    }

}
