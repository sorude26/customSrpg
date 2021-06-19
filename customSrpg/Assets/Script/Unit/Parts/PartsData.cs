using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsData : ScriptableObject
{
    [Tooltip("パーツID")]
    [SerializeField] int m_partsID;
    /// <summary> パーツ名 </summary>
    [SerializeField] string m_partsName;
    [Tooltip("重量")]
    [SerializeField] protected int m_weight = 10;
    [Tooltip("パーツサイズ")]
    [SerializeField] int m_partsSize = 1;
    /// <summary> パーツID </summary>
    public int PartsID { get => m_partsID; }
    /// <summary> パーツ名 </summary>
    public string PartsName { get => m_partsName; }
    /// <summary> 重量 </summary>
    public int Weight { get => m_weight; }
    /// <summary> パーツサイズ </summary>
    public int PartsSize { get => m_partsSize; }
}
