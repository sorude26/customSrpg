using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPartsMaster<T> : PartsMaster<T>, IUnitParts where T :UnitPartsData
{
    /// <summary> パーツ耐久値 </summary>
    public int MaxPartsHp { get => m_partsData.MaxPartsHp; }
    /// <summary> パーツ装甲値 </summary>
    public int Defense { get => m_partsData.Defense; }
    /// <summary> 現在のパーツ耐久値 </summary>
    public int CurrentPartsHp { get; protected set; }
    void Start()
    {
        StartSet();
    }
    /// <summary>
    /// パーツの初期化処理
    /// </summary>
    protected virtual void StartSet()
    {
        CurrentPartsHp = MaxPartsHp;
    }
   
    /// <summary>
    /// パーツにダメージを与える
    /// </summary>
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
    /// <summary>
    /// パーツ破壊時の処理
    /// </summary>
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
    public int GetCurrentHP()
    {
        return CurrentPartsHp;
    }
    public int GetDefense()
    {
        return Defense;
    }
}
