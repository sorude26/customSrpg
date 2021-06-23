﻿using System.Collections;
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
}