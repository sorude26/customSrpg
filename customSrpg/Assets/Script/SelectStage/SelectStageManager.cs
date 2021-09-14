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
    int m_tagetStageID = -1;
    int m_select = 0;
    int m_alliesNum = 0;
    int m_sortieNum = 0;
    int[] m_soriteUnit;
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
    }
    public void SetStageData(int id)
    {
        if (m_tagetStageID != id)
        {
            m_tagetStageID = id;
            m_stageGuideText.text = m_levelData.StageGuide[id];
        }
        else 
        {

        }
        //m_levelData.MaxSortieUnit[id];
        // m_levelData.StageSizeID[id];
        //Debug.Log($"ID:{id}番入力");
        SortieManager.SetStageData(m_sortieNum, m_levelData.StageSizeID[id], m_alliesNum);
    }
    public void SelectUnit(int id)
    {
        m_soriteUnit[m_sortieNum] = id;
        m_allModels[id].SelectOnTarget(m_sortieNum.ToString());
        m_sortieNum++;
    }
    public void SelectOutUnit(int id)
    {
        m_allModels[id].SelectOutTarget();
        for (int i = 0; i < m_soriteUnit.Length; i++)
        {
            if (m_soriteUnit[i] == id)
            {
                for (int b = i; b < m_soriteUnit.Length - 1; b++)
                {
                    m_soriteUnit[b] = m_soriteUnit[b + 1];
                    if (m_soriteUnit[b] >= 0)
                    {
                        m_allModels[b].SelectOnTarget(b.ToString());
                    }
                }
                break;
            }
        }
    }
    /// <summary>
    /// ステージ開始
    /// </summary>
    public void SortiePlayer()
    {
        FadeController.Instance.StartFadeOut(ChangeScene);
    }
    void ChangeScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
