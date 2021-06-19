using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 機体のタイプ
/// </summary>
public enum BodyType
{
    Human,
    Helicopter,
    Tank,
}
public enum ArmType
{
    Left,
    Right,
}
public class UnitPartsData : PartsData
{
    /// <summary> パーツ耐久値 </summary>
    [SerializeField] protected int m_partsHp;
    /// <summary> パーツ装甲値 </summary>
    [SerializeField] protected int m_defense;
    /// <summary> パーツ耐久値 </summary>
    public int MaxPartsHp { get => m_partsHp; }
    /// <summary> パーツ装甲値 </summary>
    public int Defense { get => m_defense; }
}
