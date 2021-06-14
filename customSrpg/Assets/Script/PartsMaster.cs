using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsMaster : MonoBehaviour
{
    /// <summary> パーツID </summary>
    [SerializeField] int m_partsID;
    /// <summary> パーツID </summary>
    public int PartsID { get => m_partsID; }
    /// <summary> パーツ名 </summary>
    [SerializeField] string m_partsName;
    /// <summary> パーツ名 </summary>
    public string PartsName { get => m_partsName; }
    /// <summary> 重量 </summary>
    [SerializeField] protected int m_weight = 10;
    /// <summary> 重量 </summary>
    public int Weight { get => m_weight; }
    /// <summary> パーツサイズ </summary>
    [SerializeField] int m_partsSize = 1;
    /// <summary> パーツサイズ </summary>
    public int PartsSize { get => m_partsSize; }

    /// <summary> 命中精度 </summary>
    [SerializeField] protected int m_hitAccuracy;
    /// <summary> 命中精度 </summary>
    public int HitAccuracy { get => m_hitAccuracy; }
}
