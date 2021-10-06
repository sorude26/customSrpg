using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIControl;
public class ColorControlBase : CommandBase
{
    public override void StartSet()
    {
        SelectController = GetComponent<ViewCommandControl>();
        SelectController.SetText(m_name);
    }

    public override void CommandDecide()
    {
        CommandBaseControl.Instance.SetAction(Customize.CustomizeManager.Instance.OpenColorPanel(), MoveParent);
    }
    public override void SelectCommand()
    {
        Customize.CustomizeManager.Instance.ViewMessage(m_commandType);
        CommandBaseControl.Instance.SetAction(Customize.CustomizeManager.Instance.OpenColorPanel(), MoveParent);
    }
    public override void SelectOut()
    {
        Customize.CustomizeManager.Instance.CloseColorPanel();
    }
}
