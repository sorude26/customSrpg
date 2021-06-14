using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPartsMaster : PartsMaster
{
    /// <summary> パーツ耐久値 </summary>
    [SerializeField] protected int m_partsHp;
    /// <summary> パーツ装甲値 </summary>
    [SerializeField] protected int m_defense;
    /// <summary> パーツ耐久値 </summary>
    public int MaxPartsHp { get => m_partsHp; }
    /// <summary> パーツ装甲値 </summary>
    public int Defense { get => m_defense; }
    /// <summary> 現在のパーツ耐久値 </summary>
    public int CurrentPartsHp { get; protected set; }

    /// <summary>
    /// パーツの初期化処理
    /// </summary>
    protected virtual void StartSet()
    {
        CurrentPartsHp = m_partsHp;
    }
    /// <summary> パーツにダメージを与える </summary>
    /// <param name="dmage"></param>
    public virtual void Damage(int dmage)
    {
        if (CurrentPartsHp <= 0)
        {
            return;
        }
        CurrentPartsHp -= dmage;
        Debug.Log(PartsName + "に" + dmage + "ダメージ");
        if (CurrentPartsHp <= 0)
        {
            Debug.Log(PartsName + "が破壊");
            CurrentPartsHp = 0;
            PartsBreak();
        }
    }
    /// <summary> パーツ破壊時の処理 </summary>
    protected virtual void PartsBreak()
    {
        Break = true;
        m_partsObject.SetActive(false);
    }
    /// <summary>
    /// パーツのサイズを返す
    /// </summary>
    /// <returns></returns>
    public virtual int GetPartsSize()
    {
        return PartsSize;
    }
}
