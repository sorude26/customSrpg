using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIControl;

public class UnitSelectBase : CommandBase
{
    public override void StartSet()
    {
        SelectController = GetComponent<ViewCommandControl>();
        SelectController.SetText(m_name);
    }

    public override void CommandDecide()
    {
        CommandBaseControl.Instance.SetAction(Customize.CustomizeManager.Instance.CursorMove, MoveParent);
    }
    public override void SelectCommand()
    {
        CommandBaseControl.Instance.SetAction(Customize.CustomizeManager.Instance.CursorMove, MoveParent);
    }
    public override void SelectOut()
    {
    }
}
