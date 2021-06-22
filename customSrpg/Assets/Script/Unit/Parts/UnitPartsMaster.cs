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
    /// <summary> 攻撃命中の表示箇所 </summary>
    [SerializeField] protected Transform[] m_hitPos;
    /// <summary> 耐久値半分以下で表示する煙 </summary>
    [SerializeField] protected GameObject m_damageSmoke;
    void Start()
    {
        StartSet();
    }
    /// <summary>
    /// パーツの初期化処理
    /// </summary>
    protected virtual void StartSet()
    {
        m_damageSmoke.SetActive(false);
        CurrentPartsHp = MaxPartsHp;
    }
   
    /// <summary>
    /// パーツにダメージを与える
    /// </summary>
    /// <param name="power"></param>
    public virtual void Damage(int power)
    {
        if (CurrentPartsHp <= 0)
        {
            return;
        }
        int d = BattleData.GetDamage(power, Defense);
        int r = Random.Range(0, m_hitPos.Length);
        EffectManager.PlayEffect(EffectType.ShotHit, m_hitPos[r].position);
        CurrentPartsHp -= d;
        Debug.Log($"{PartsName}に{d}ダメージ、残:{ CurrentPartsHp}");
        if (CurrentPartsHp < MaxPartsHp / 2)
        {
            m_damageSmoke.SetActive(true);
        }
        if (CurrentPartsHp <= 0)
        {
            EffectManager.PlayEffect(EffectType.ExplosionParts, transform.position);
            Debug.Log($"{PartsName}が破壊");
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
