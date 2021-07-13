using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instanse { get; private set; }
    [SerializeField] UnitPartsList m_partsList;
    public UnitPartsList PartsList { get => m_partsList; }
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
