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
    [Tooltip("出現パーツのパターン")]
    [SerializeField] PopParts[] m_popPartsPattern;
    [Tooltip("規則性の無い機体構成を返すフラグ")]
    [SerializeField] bool m_randamBuild;
    /// <summary> 出撃機体の総数 </summary>
    public int AllUnitNumber { get => m_sortieUnitsPattern.Sum(); }
    /// <summary> 出撃機体の編成数 </summary>
    public int[] SortiePattern { get => m_sortieUnitsPattern; }
    /// <summary> 出撃機体のカラー </summary>
    public Color[] ColorPattern { get => m_unitsColorPattern; }
    /// <summary>
    /// フラグに対応した機体のデータを返す
    /// </summary>
    /// <param name="patternNum"></param>
    /// <returns></returns>
    public UnitBuildData SoriteData(int patternNum)
    {
        if (m_randamBuild)
        {
            return RandamUnit(patternNum);
        }
        return PatternUnit(patternNum);
    }
    /// <summary>
    /// パターンの完全ランダム機体データを返す
    /// </summary>
    /// <param name="patternNum"></param>
    /// <returns></returns>
    UnitBuildData RandamUnit(int patternNum)
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
    UnitBuildData PatternUnit(int patternNum)
    {
        if (patternNum >= m_popPartsPattern.Length)
        {
            return m_popPartsPattern[0].PopUnitP();
        }
        return m_popPartsPattern[patternNum].PopUnitP();
    }
}
