using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIControl;

public class UnitPartsChangeHead : UnitPartsChange
{
    public override int CommandNum { get => GameManager.Instanse.PartsList.GetHaveAllHead().Length; }

    public override void SetData(CommandBase[] commands)
    {
        var allParts = GameManager.Instanse.PartsList.GetHaveAllHead();
        m_commandNames = new string[allParts.Length];
        m_partsIDs = new int[allParts.Length];
        for (int i = 0; i < allParts.Length; i++)
        {
            m_commandNames[i] = allParts[i].PartsName;
            m_partsIDs[i] = allParts[i].PartsID;
            var comand = commands[i].GetComponent<ViewCommandControl>();
            comand.SetText(m_commandNames[i]);
            comand.StartSet(i, PartsChange);
        }
        OnPartsChange += Customize.CustomizeManager.Instance.ChangePartsHead;
    }
}
