using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPartsMaster : PartsMaster
{
    /// <summary> パーツ耐久値 </summary>
    [SerializeField] protected int m_partsHp;
    /// <summary> パーツ耐久値 </summary>
    public int MaxPartsHp { get => m_partsHp; }
    /// <summary> パーツ装甲値 </summary>
    [SerializeField] protected int m_defense;
    /// <summary> パーツ装甲値 </summary>
    public int Defense { get => m_defense; }
    /// <summary> 現在のパーツ耐久値 </summary>
    public int CurrentPartsHp { get; protected set; }

    /// <summary>
    /// パーツの初期化
    /// </summary>
    protected virtual void StartSet()
    {
        CurrentPartsHp = m_partsHp;
    }
}
