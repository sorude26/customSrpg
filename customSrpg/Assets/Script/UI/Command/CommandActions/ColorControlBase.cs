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
        CommandBaseControl.Instance.SetAction(Customize.CustomizeSelect.Instance.OpenColorPanel(), MoveParent);
    }
    public override void SelectCommand()
    {
        CommandBaseControl.Instance.SetAction(Customize.CustomizeSelect.Instance.OpenColorPanel(), MoveParent);
    }
    public override void SelectOut()
    {
        Customize.CustomizeSelect.Instance.CloseColorPanel();
    }
}
