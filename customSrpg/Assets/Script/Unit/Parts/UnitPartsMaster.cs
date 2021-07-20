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
    protected int m_damageCount = 0;
    protected List<int> m_partsDamage;
    [Tooltip("攻撃命中の表示箇所")]
    [SerializeField] protected Transform[] m_hitPos;
    [Tooltip(" 耐久値半分以下で表示する煙 ")]
    [SerializeField] protected GameObject m_damageSmoke;
    [Tooltip("色が変更可能な装甲")]
    [SerializeField] protected Renderer[] m_amors;

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
        m_partsDamage = new List<int>();
    }
    /// <summary>
    /// パーツの色変更
    /// </summary>
    /// <param name="color"></param>
    public virtual void ColorChange(Color color)
    {
        foreach (var renderer in m_amors)
        {
            renderer.material.color = color;
        }
    }
    /// <summary>
    /// パーツにダメージを与える
    /// </summary>
    /// <param name="power"></param>
    public virtual int Damage(int power)
    {
        if (CurrentPartsHp <= 0)
        {
            return 0;
        }
        int d = BattleCalculator.GetDamage(power, Defense);
        CurrentPartsHp -= d;
        //Debug.Log($"{PartsName}に{d}ダメージ、残:{ CurrentPartsHp}");
        m_partsDamage.Add(d);
        if (CurrentPartsHp < MaxPartsHp / 2)
        {
            m_damageSmoke.SetActive(true);
        }
        if (CurrentPartsHp <= 0)
        {
            //Debug.Log($"{PartsName}が破壊");
            CurrentPartsHp = 0;
            Break = true;
        }
        return d;
    }
    /// <summary>
    /// ダメージの演出を行う
    /// </summary>
    public virtual void DamageEffect()
    {
        int r = Random.Range(0, m_hitPos.Length);
        EffectManager.PlayEffect(EffectType.ShotHit, m_hitPos[r].position);
        EffectManager.PlayDamage(m_partsDamage[m_damageCount], m_hitPos[r].position);
        m_damageCount++;
        if (m_damageCount >= m_partsDamage.Count)
        {
            if (CurrentPartsHp <= 0)
            {
                EffectManager.PlayEffect(EffectType.ExplosionParts, transform.position);
                PartsBreak();
            }
            m_damageCount = 0;
            m_partsDamage.Clear();
        }
    }
    /// <summary>
    /// パーツ破壊時の処理
    /// </summary>
    protected virtual void PartsBreak()
    {
        m_partsObject.SetActive(false);
    }
    /// <summary>
    /// パーツのサイズを返す
    /// </summary>
    /// <returns></returns>
    public virtual int GetPartsSize() => PartsSize;
    /// <summary>
    /// パーツの最大耐久値を返す
    /// </summary>
    /// <returns></returns>
    public int GetMaxHP() => MaxPartsHp;
    /// <summary>
    /// パーツの現在耐久値を返す
    /// </summary>
    /// <returns></returns>
    public int GetCurrentHP() => CurrentPartsHp;
    /// <summary>
    /// パーツの防御力を返す
    /// </summary>
    /// <returns></returns>
    public int GetDefense() => Defense;
}
