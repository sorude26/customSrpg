using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instanse { get; private set; }
    [SerializeField] UnitPartsList m_partsList;
    public UnitPartsList PartsList { get => m_partsList; }
    [SerializeField] ColorData m_colorData;
    [SerializeField] int[] m_sParts;
    [SerializeField] GameDataManager m_dataManager;
    public bool AutoMode { get; set; }
    public Color GetColor(int colorNum) => m_colorData.GetColor(colorNum);
    private void Awake()
    {
        if (Instanse)
        {
            Destroy(gameObject);
            return;
        }
        Instanse = this;
        DontDestroyOnLoad(gameObject);
        UnitDataMaster.StartSet(m_partsList);
        SetSParts();
        //SetAllParts();
    }

    void SetAllParts()
    {
        for (int i = 0; i < 6; i++)
        {
            var parts = (PartsType)i;
            for (int x = 0; x < UnitDataMaster.HavePartsDic[parts].Length; x++)
            {
                UnitDataMaster.HavePartsDic[parts][x]++;
            }
        }
    }
    void SetSParts()
    {
        UnitDataMaster.HavePartsDic[PartsType.Weapon][0]++;
        for (int i = 0; i < 6; i++)
        {
            var parts = (PartsType)i;
            UnitDataMaster.HavePartsDic[parts][m_sParts[i]]++;
        }
        for (int i = 0; i < UnitDataMaster.MaxUintCount; i++)
        {
            UnitDataMaster.SetData(i, new UnitBuildData(m_sParts[1], m_sParts[0], m_sParts[2], m_sParts[3], m_sParts[4], m_sParts[5], 0), 22);
        }
    }
    public void Save()
    {
        m_dataManager.SaveData();
    }
    public void Load()
    {
        m_dataManager.LoadData();
    }
}
