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
            m_soriteUnit[i] = i;
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
        //m_levelData.MaxSortieUnit[id];
        // m_levelData.StageSizeID[id];
        //Debug.Log($"ID:{id}番入力");
        SortieManager.SetStageData(m_sortieNum, m_levelData.StageSizeID[id], m_alliesNum);
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
