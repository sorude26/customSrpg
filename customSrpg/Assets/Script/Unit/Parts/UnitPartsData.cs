using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 機体のタイプ
/// </summary>
public enum UnitType
{
    Human,
    Walker,
    Helicopter,
    Tank,
}
/// <summary>
/// 手の種類
/// </summary>
public enum ArmType
{
    Left,
    Right,
}
/// <summary>
/// 機体パーツデータの基底クラス
/// </summary>
public class UnitPartsData : PartsData
{
    [Tooltip("パーツ耐久値")]
    [SerializeField] protected int m_partsHp;
    [Tooltip("パーツ装甲値")]
    [SerializeField] protected int m_defense;
    /// <summary> パーツ耐久値 </summary>
    public int MaxPartsHp { get => m_partsHp; }
    /// <summary> パーツ装甲値 </summary>
    public int Defense { get => m_defense; }
}
