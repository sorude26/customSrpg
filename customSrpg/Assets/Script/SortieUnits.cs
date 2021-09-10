using System.Linq;
using UnityEngine;
/// <summary>
/// 出撃する機体の編成設定
/// </summary>
[CreateAssetMenu]
public class SortieUnits : ScriptableObject
{
    [Tooltip("出撃機体の編成数")]
    [SerializeField] int[] m_sortieUnitsPattern;
    [Tooltip("出撃機体のカラー")]
    [SerializeField] Color[] m_unitsColorPattern;
    [Tooltip("出撃機体の構築固定パターン")]
    [SerializeField] UnitBuildData[] m_sortieUnitsBuildPattern;
    [Tooltip("出現パーツのパターン")]
    [SerializeField] PopParts[] m_popPartsPattern;
    /// <summary> 出撃機体の総数 </summary>
    public int AllUnitNumber { get => m_sortieUnitsPattern.Sum(); }
    /// <summary> 出撃機体の編成数 </summary>
    public int[] SortiePattern { get => m_sortieUnitsPattern; }
    /// <summary> 出撃機体のカラー </summary>
    public Color[] ColorPattern { get => m_unitsColorPattern; }
    /// <summary> 出撃機体の構築パターン </summary>
    public UnitBuildData[] BuildPattern { get => m_sortieUnitsBuildPattern; }
    /// <summary>
    /// パターンの完全ランダム機体データを返す
    /// </summary>
    /// <param name="patternNum"></param>
    /// <returns></returns>
    public UnitBuildData RandamUnit(int patternNum)
    {
        if (patternNum >= m_popPartsPattern.Length)
        {
            return m_popPartsPattern[0].PopUnitR();
        }
        return m_popPartsPattern[patternNum].PopUnitR();
    }
    /// <summary>
    /// パターンの規則性のあるランダム機体データを返す
    /// </summary>
    /// <param name="patternNum"></param>
    /// <returns></returns>
    public UnitBuildData PatternUnit(int patternNum)
    {
        if (patternNum >= m_popPartsPattern.Length)
        {
            return m_popPartsPattern[0].PopUnitP();
        }
        return m_popPartsPattern[patternNum].PopUnitP();
    }
}
