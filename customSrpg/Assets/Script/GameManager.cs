using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instanse { get; private set; }
    [SerializeField] UnitPartsList m_partsList;
    public UnitPartsList PartsList { get => m_partsList; }
    [SerializeField] UnitBuildData[] m_playerUnits;
    public UnitBuildData[] PlayerUnits { get => m_playerUnits; }
    [SerializeField] int[] m_playerColor;
    public int[] PlayerColor { get => m_playerColor; }
    [SerializeField] int m_haveUnitNumber = 3;
    public int HaveUnitNumber { get => m_haveUnitNumber; }
    private void Awake()
    {
        if (Instanse)
        {
            Destroy(this);
            return;
        }
        Instanse = this;
    }
}
