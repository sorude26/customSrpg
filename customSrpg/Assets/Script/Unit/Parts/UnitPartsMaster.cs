using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 機体パーツの基底クラス
/// </summary>
/// <typeparam name="T">対応するパーツのデータ</typeparam>
public class UnitPartsMaster<T> : PartsMaster<T>, IUnitParts where T :UnitPartsData
{
    /// <summary> パーツ耐久値 </summary>
    public int MaxPartsHp { get => m_partsData.MaxPartsHp; }
    /// <summary> パーツ装甲値 </summary>
    public int Defense { get => m_partsData.Defense; }
    /// <summary> 現在のパーツ耐久値 </summary>
    public int CurrentPartsHp { get; protected set; }
    /// <summary> 表示用パーツ耐久値 </summary>
    public int ViewCurrentHp { get; protected set; }
    /// <summary> ダメージを受けた回数 </summary>
    protected int m_damageCount = 0;
    /// <summary> 受けたダメージ </summary>
    protected List<int> m_partsDamage;
    [Tooltip("攻撃命中の表示箇所")]
    [SerializeField] protected Transform[] m_hitPos;
    [Tooltip(" 耐久値半分以下で表示する煙 ")]
    [SerializeField] protected GameObject m_damageSmoke;
    [Tooltip("色が変更可能な装甲")]
    [SerializeField] protected Renderer[] m_amors;
    protected Color m_startColor = Color.green;
    protected bool m_damageColor;
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
        ViewCurrentHp = MaxPartsHp;
        m_partsDamage = new List<int>();
    }

    public virtual void PartsColorChange(Color color)
    {
        foreach (var renderer in m_amors)
        {
            renderer.material.color = color;
        }
        m_startColor = color;
    }
    public virtual void ColorChange(Color color)
    {
        foreach (var renderer in m_amors)
        {
            renderer.material.color = color;
        }
    }

    public virtual int Damage(int power)
    {
        if (CurrentPartsHp <= 0)
        {
            return 0;
        }
        int damage = BattleCalculator.GetDamage(power, Defense);
        CurrentPartsHp -= damage;
        m_partsDamage.Add(damage);
        if (CurrentPartsHp < MaxPartsHp / 2)
        {
            m_damageSmoke.SetActive(true);
        }
        if (CurrentPartsHp <= 0)
        {
            CurrentPartsHp = 0;
            Break = true;
        }
        return damage;
    }
    /// <summary>
    /// ダメージの演出を行う
    /// </summary>
    public virtual void DamageEffect()
    {
        int r = Random.Range(0, m_hitPos.Length);
        EffectManager.PlayEffect(EffectType.ShotHit, m_hitPos[r].position);
        EffectManager.PlayDamage(m_partsDamage[m_damageCount], m_hitPos[r].position);
        ViewCurrentHp -= m_partsDamage[m_damageCount];
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
        if (!m_damageColor)
        {
            m_damageColor = true;
            StartCoroutine(DamageColor());
        }
    }
    /// <summary>
    /// パーツ破壊時の処理
    /// </summary>
    protected virtual void PartsBreak()
    {
        foreach (var item in m_partsObject)
        {
            item.SetActive(false);
        }
    }

    public int GetMaxHP() => MaxPartsHp;
    public int GetCurrentHP() => CurrentPartsHp;
    public int GetDefense() => Defense;
    protected virtual IEnumerator DamageColor()
    {
        ColorChange(Color.white);
        yield return new WaitForSeconds(0.05f);
        ColorChange(Color.black);
        yield return new WaitForSeconds(0.05f);
        ColorChange(Color.white);
        yield return new WaitForSeconds(0.05f);
        ColorChange(m_startColor);
        m_damageColor = false;
    }
}
