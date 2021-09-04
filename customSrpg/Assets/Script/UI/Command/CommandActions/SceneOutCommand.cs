using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIControl;

public class SceneOutCommand : CommandBase
{
    bool m_sceneOut;
    public override void StartSet()
    {
        SelectController = GetComponent<ViewCommandControl>();
        SelectController.SetText(m_name);
    }

    public override void CommandDecide()
    {
        if (m_sceneOut)
        {
            return;
        }

        m_sceneOut = true;
        FadeController.Instance.StartFadeOut(Customize.CustomizeManager.Instance.DataSet);
    }
}
