using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIControl;

public class UnitPartsChange : CommandAction<CommandBox>
{
    int[] m_partsIDs;
    void Set()
    {
        var allBody = GameManager.Instanse.PartsList.GetAllBodys();
        m_commandNames = new string[allBody.Length];
        m_partsIDs = new int[allBody.Length];
        for (int i = 0; i < allBody.Length; i++)
        {
            m_commandNames[i] = allBody[i].PartsName;
            m_partsIDs[i] = allBody[i].PartsID;
        }
    }
}
