using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Effectの種類
/// </summary>
public enum EffectType
{
    /// <summary> 使用しない </summary>
    None,
    /// <summary> 射撃 </summary>
    Shot,
    /// <summary> 被弾 </summary>
    ShotHit,
    /// <summary> パーツ破壊 </summary>
    ExplosionParts,
    /// <summary> ユニット破壊 </summary>
    ExplosionUnit,
    /// <summary> 爆発大 </summary>
    Explosion,
}
public class EffectManager : MonoBehaviour
{
    /// <summary>
    /// Effectと最大表示数を設定できる
    /// </summary>
    [System.Serializable]
    class EffectData
    {
        /// <summary> EffectのPrefab </summary>
        public GameObject EffectPrefab = default;
        /// <summary> 最大表示数 </summary>
        public int MaxCount = 1;
    }
    static EffectManager m_instance;
    /// <summary> 全Effectの入れ物 </summary>
    [SerializeField] EffectData[] m_effectDatas;
    /// <summary> 全EffectのID付き入れ物 </summary>
    Dictionary<EffectType, List<EffectControl>> m_effectDic = new Dictionary<EffectType, List<EffectControl>>();
    [SerializeField] int m_maxTextCount = 20;
    [SerializeField] StageText m_damgeTextPrefab = default;
    List<StageText> m_damgeTexts = new List<StageText>();
    private void Awake()
    {
        m_instance = this;
        for (int i = 0; i < m_effectDatas.Length; i++)
        {
            EffectType effectType = (EffectType)(i + 1);
            m_effectDic.Add(effectType, new List<EffectControl>());
            for (int k = 0; k < m_effectDatas[i].MaxCount; k++)
            {
                var instance = Instantiate(m_effectDatas[i].EffectPrefab, this.transform);
                var eControl = instance.AddComponent<EffectControl>();
                m_effectDic[effectType].Add(eControl);
            }
        }
        for (int j = 0; j < m_maxTextCount; j++)
        {
            m_damgeTexts.Add(Instantiate(m_damgeTextPrefab, this.transform));
        }
    }
    /// <summary>
    /// 指定した場所で指定したEffectを再生する
    /// </summary>
    /// <param name="effectType"></param>
    /// <param name="pos"></param>
    public static void PlayEffect(EffectType effectType, Vector3 pos)
    {
        foreach (var effect in m_instance.m_effectDic[effectType])
        {
            if (effect.IsActive())
            {
                continue;
            }
            effect.Play(pos);
            return;
        }
    }
    /// <summary>
    /// 通常のダメージを表示する
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="pos"></param>
    public static void PlayDamage(int damage,Vector3 pos)
    {
        foreach (var text in m_instance.m_damgeTexts)
        {
            if (text.IsActive())
            {
                continue;
            }
            text.Play(damage, pos);
            return;
        }
    }

    /// <summary>
    /// 任意の時間、ダメージを表示する
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="pos"></param>
    /// <param name="size"></param>
    /// <param name="time"></param>
    public static void PlayDamage(int damage,Vector3 pos, int size,float time)
    {
        foreach (var text in m_instance.m_damgeTexts)
        {
            if (text.IsActive())
            {
                continue;
            }
            text.Play(damage, pos, size, time);
            return;
        }
    }
    /// <summary>
    /// 任意の時間、メッセージを表示する
    /// </summary>
    /// <param name="message"></param>
    /// <param name="pos"></param>
    /// <param name="size"></param>
    /// <param name="time"></param>
    public static void PlayMessage(string message, Vector3 pos, int size, float time)
    {
        foreach (var text in m_instance.m_damgeTexts)
        {
            if (text.IsActive())
            {
                continue;
            }
            text.Play(message, pos, size, time);
            return;
        }
    }
}
