using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユニットの武器を含めた全パーツの基底クラス
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class PartsMaster<T> : MonoBehaviour, IParts where T:PartsData
{
    [Tooltip("パーツの基礎ID")]
    [SerializeField] protected int m_partsID;
    [Tooltip("パーツのデータ")]
    [SerializeField] protected T m_partsData;
    [Tooltip("表示が切り替わるパーツ")]
    [SerializeField] protected GameObject[] m_partsObject;
    [Tooltip("パーツのアニメーション")]
    [SerializeField] protected Animator m_anime;
    /// <summary> パーツID </summary>
    public int PartsID { get => m_partsID; }
    /// <summary> パーツ名 </summary>
    public string PartsName { get => m_partsData.PartsName[m_partsID]; }
    /// <summary> 重量 </summary>
    public int Weight { get => m_partsData.Weight[m_partsID]; }
    /// <summary> パーツサイズ </summary>
    public int PartsSize { get => m_partsData.PartsSize[m_partsID]; }
    /// <summary> 破壊フラグ </summary>
    public bool Break { get; protected set; }
    /// <summary> パーツの固有アニメーション </summary>
    public Animator PartsAnime { get => m_anime; }
    public int GetID() => PartsID;
    public int GetWeight() => Weight;
    public virtual int GetSize() => PartsSize;
    public bool GetBreak() => Break;
    public virtual void DestoryParts() => Destroy(this.gameObject);
}
