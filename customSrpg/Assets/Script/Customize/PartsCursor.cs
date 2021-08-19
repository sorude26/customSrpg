using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartsCursor : MonoBehaviour
{
    [SerializeField] Text m_text;
    /// <summary> パーツのID </summary>
    int m_partsID;
    /// <summary> 選択された際のイベント </summary>
    public event Action<int> OnCursor;
    public void SetID(int partsID)
    {
        m_partsID = partsID;
        m_text.text = GameManager.Instanse.PartsList.GetHead(partsID).PartsName;
    }
    public void OnClickThis()
    {
        OnCursor?.Invoke(m_partsID);
    }
}
