using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユニットの武器を含めた全パーツの基底クラス
/// </summary>
/// <typeparam name="T"></typeparam>
public class PartsMaster<T> : MonoBehaviour, IParts where T:PartsData
{
    [SerializeField] protected T m_partsData;
    /// <summary> 表示されるパーツ </summary>
    [SerializeField] protected GameObject m_partsObject;
    /// <summary> パーツID </summary>
    public int PartsID { get => m_partsData.PartsID; }
    /// <summary> パーツ名 </summary>
    public string PartsName { get => m_partsData.PartsName; }
    /// <summary> 重量 </summary>
    public int Weight { get => m_partsData.Weight; }
    /// <summary> パーツサイズ </summary>
    public int PartsSize { get => m_partsData.PartsSize; }
    /// <summary> 破壊フラグ </summary>
    public bool Break { get; protected set; }
    public int GetID() => PartsID;
    public int GetWeight() => Weight;
    public virtual int GetSize() => PartsSize;
    public bool GetBreak() => Break;
}
