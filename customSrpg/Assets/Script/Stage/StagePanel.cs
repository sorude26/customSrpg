using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePanel : MonoBehaviour
{
    /// <summary> 移動可能表示用パネル </summary>
    [SerializeField] GameObject m_movePanel;
    /// <summary> 攻撃可能表示用パネル </summary>
    [SerializeField] GameObject m_attackPanel;
    bool m_moveMode;
    bool m_attackMode;
    int m_posX;
    int m_posZ;
    public bool StartMode { get; private set; }
    public void SetPos(int x,int z)
    {
        m_posX = x;
        m_posZ = z;
    }
    public void ViewStartPanel()
    {
        m_attackPanel.SetActive(true);
        StartMode = true;
    }
    public void CloseStartPanel()
    {
        m_attackPanel.SetActive(false); ;
        StartMode = false;
    }
    /// <summary>
    /// 移動可能表示を出す
    /// </summary>
    public void ViewMovePanel()
    {
        m_movePanel.SetActive(true);
        m_moveMode = true;
    }
    /// <summary>
    /// 攻撃可能表示を出す
    /// </summary>
    public void ViewAttackPanel()
    {
        m_attackPanel.SetActive(true);
        m_attackMode = true;
    }
    /// <summary>
    /// 攻撃可能表示を消す
    /// </summary>
    public void CloseAttackPanel()
    {
        m_attackPanel.SetActive(false);
        m_attackMode = false;
    }
    /// <summary>
    /// 表示パネルを閉じる
    /// </summary>
    public void ViewEnd()
    {
        m_movePanel.SetActive(false);
        m_moveMode = false;
        if (m_attackMode)
        {
            CloseAttackPanel();
        }
    }
    private void OnMouseDown()
    {
        if (m_moveMode)
        {
            StageManager.Instance.PointMove(m_posX, m_posZ);
        }
        if (StartMode)
        {
            StageManager.Instance.PointUnitSet(m_posX, m_posZ);
            CloseStartPanel();
        }
    }
    private void OnEnable()
    {
        ViewEnd();
        EventManager.OnStageGuideViewEnd += ViewEnd;
        EventManager.OnGameStart += CloseStartPanel;
        EventManager.OnAttackSearchEnd += CloseAttackPanel;
    }
    private void OnDisable()
    {
        EventManager.OnStageGuideViewEnd -= ViewEnd;
        EventManager.OnGameStart -= CloseStartPanel;
        EventManager.OnAttackSearchEnd -= CloseAttackPanel;
    }
}
