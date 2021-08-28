using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIControl;

public class UnitPartsChangeLArm : UnitPartsChange
{
    public override int CommandNum { get => GameManager.Instanse.PartsList.GetAllLArms().Length; }

    public override void SetData(CommandBox[] commands)
    {
        var allParts = GameManager.Instanse.PartsList.GetAllLArms();
        m_commandNames = new string[allParts.Length];
        m_partsIDs = new int[allParts.Length];
        for (int i = 0; i < allParts.Length; i++)
        {
            m_commandNames[i] = allParts[i].PartsName;
            m_partsIDs[i] = allParts[i].PartsID;
            commands[i].SetText(m_commandNames[i]);
            commands[i].StartSet(i, PartsChange);
        }
        OnPartsChange += Customize.CustomizeSelect.Instance.ChangePartsLArm;
    }
}
