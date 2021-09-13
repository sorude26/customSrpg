using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIControl;

public class StageSelectCommand : CommandBase
{
    public override void CommandDecide()
    {
        m_children[SelectController.SelectNum].SelectController.OnClickCommand();
    }
}
