using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    [SerializeField] GameObject m_decisionMassage;
    public event Action OnDecision;
    public event Action OnCancel;
    public void OpenMassage()
    {
        m_decisionMassage.SetActive(true);
    }
    public void OnClickDecision()
    {
        m_decisionMassage.SetActive(false);
        OnDecision?.Invoke();
    }
    public void OnClickCancel()
    {
        m_decisionMassage.SetActive(false);
        OnCancel?.Invoke();
    }
}
