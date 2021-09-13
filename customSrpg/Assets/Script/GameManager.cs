using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instanse { get; private set; }
    [SerializeField] UnitPartsList m_partsList;
    public UnitPartsList PartsList { get => m_partsList; }
    [SerializeField] ColorData m_colorData;
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
        UnitBuildDataMaster.StartSet(m_partsList);
        //SetSParts();
        SetAllParts();
    }

    void SetAllParts()
    {
        for (int i = 0; i < 6; i++)
        {
            var parts = (PartsType)i;
            for (int x = 0; x < UnitBuildDataMaster.HavePartsDic[parts].Length; x++)
            {
                UnitBuildDataMaster.HavePartsDic[parts][x]++;
            }
        }
    }
    void SetSParts()
    {
        for (int i = 0; i < 6; i++)
        {
            var parts = (PartsType)i;
            for (int x = 0; x < 3; x++)
            {
                UnitBuildDataMaster.HavePartsDic[parts][x]++;
            }
        }
    }
}
