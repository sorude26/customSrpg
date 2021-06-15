using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePanel : MonoBehaviour
{
    /// <summary> 移動可能表示用パネル </summary>
    [SerializeField] GameObject m_movePanel;
    /// <summary> 攻撃可能表示用パネル </summary>
    [SerializeField] GameObject m_attackPanel;
    private void OnEnable()
    {
        ViewEnd();
        EventManager.OnStageGuideViewEnd += ViewEnd;
    }
    private void OnDisable()
    {
        EventManager.OnStageGuideViewEnd -= ViewEnd;
    }
    /// <summary>
    /// 移動可能表示を出す
    /// </summary>
    public void ViewMovePanel()
    {
        m_movePanel.SetActive(true);
    }
    /// <summary>
    /// 攻撃可能表示を出す
    /// </summary>
    public void ViewAttackPanel()
    {
        m_attackPanel.SetActive(true);
    }
    /// <summary>
    /// 表示パネルを閉じる
    /// </summary>
    public void ViewEnd()
    {
        m_movePanel.SetActive(false);
        m_attackPanel.SetActive(false);
    }
}
