using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectStageManager : MonoBehaviour
{
    public static SelectStageManager Instance { get; private set; }
    [SerializeField] Text m_stageGuideText;
    [SerializeField] StageLevelData m_levelData;
    [SerializeField] Customize.CustomizeModel[] m_allModels;
    [SerializeField] int m_maxHorizonalModelCount = 4;
    [SerializeField] GameObject m_modelBase;
    int m_tagetStageID = -1;
    int m_selectNum = 0;
    int m_alliesNum = 0;
    int m_sortieNum = 0;
    int[] m_soriteUnit;
    bool m_unitSelectMode = false;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        m_soriteUnit = new int[UnitDataMaster.MaxUintCount];
        for (int i = 0; i < m_soriteUnit.Length; i++)
        {
            m_soriteUnit[i] = -1;
            m_allModels[i].StartSet(GameManager.Instanse.GetColor, i);
        }
        FadeController.Instance.StartFadeIn();
        GameScene.InputManager.Instance.OnInputArrowLate += SelectChange;
        SortieManager.SetPlayer(0, m_soriteUnit);
    }
    public void SetStageData(int id)
    {
        if (m_tagetStageID != id)
        {
            m_tagetStageID = id;
            m_stageGuideText.text = m_levelData.StageGuide[id];
            if (m_unitSelectMode)
            {
                m_modelBase.transform.position = new Vector3(0, 0, 50);
                m_unitSelectMode = false;
                SelectOutAll();
            }
        }
        else if (m_unitSelectMode)
        {
            SelectUnit(m_selectNum);
        }
        else
        {
            m_unitSelectMode = true;
            SortieManager.SetStageData(m_tagetStageID, m_alliesNum);
            m_modelBase.transform.position = Vector3.zero;
            UIControl.CommandBaseControl.Instance.CommandMoveOff();
            m_allModels[m_selectNum].SelectOn();
        }
    }
    void SelectChange(float x, float y)
    {
        if (!m_unitSelectMode)
        {
            return;
        }
        m_allModels[m_selectNum].SelectOut();
        if (x > 0)
        {
            m_selectNum++;
            if (m_selectNum >= m_allModels.Length)
            {
                m_selectNum = 0;
            }
        }
        else if (x < 0)
        {
            m_selectNum--;
            if (m_selectNum < 0)
            {
                m_selectNum = m_allModels.Length - 1;
            }
        }
        else if (y > 0)
        {
            m_selectNum += m_maxHorizonalModelCount;
            if (m_selectNum >= m_allModels.Length)
            {
                m_selectNum -= m_allModels.Length;
            }
        }
        else if (y < 0)
        {
            m_selectNum -= m_maxHorizonalModelCount;
            if (m_selectNum < 0)
            {
                m_selectNum += m_allModels.Length;
            }
        }
        m_allModels[m_selectNum].SelectOn();
    }
    public void SelectUnit(int id)
    {
        if (m_allModels[id].Select)
        {
            SelectOutUnit(id);
            return;
        }
        if (m_sortieNum >= m_levelData.MaxSortieUnit[m_tagetStageID])
        {
            return;
        }
        m_soriteUnit[m_sortieNum] = id;
        m_allModels[id].SelectOnTarget((m_sortieNum + 1).ToString());
        m_sortieNum++;
    }
    public void SelectOutUnit(int id)
    {
        m_allModels[id].SelectOutTarget();
        m_sortieNum--;
        for (int i = 0; i < m_soriteUnit.Length; i++)
        {
            if (m_soriteUnit[i] == id)
            {
                for (int k = i; k < m_soriteUnit.Length - 1; k++)
                {
                    m_soriteUnit[k] = m_soriteUnit[k + 1];
                }
                break;
            }
        }
        for (int i = 0; i < m_sortieNum; i++)
        {
            m_allModels[m_soriteUnit[i]].SelectOnTarget((i + 1).ToString());
        }
    }
    void SelectOutAll()
    {
        foreach (var model in m_allModels)
        {
            model.SelectOutTarget();
        }
        for (int i = 0; i < m_soriteUnit.Length; i++)
        {
            m_soriteUnit[i] = -1;
        }
        m_selectNum = 0;
        m_sortieNum = 0;
    }
    /// <summary>
    /// ステージ開始
    /// </summary>
    public void SortiePlayer()
    {
        if (!m_unitSelectMode || m_sortieNum <= 0)
        {
            return;
        }
        SortieManager.SetPlayer(m_sortieNum, m_soriteUnit);
        FadeController.Instance.StartFadeOut(ChangeScene);
    }
    void ChangeScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
