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
public class PartsBody : UnitPartsMaster
{
    /// <summary> 機体出力 </summary>
    [SerializeField] int m_unitOutput;
    /// <summary> 昇降力 </summary>
    [SerializeField] float m_liftingForce;
    /// <summary> 移動力 </summary>
    [SerializeField] int m_movePower = 0;
    /// <summary> 回避力 </summary>
    [SerializeField] int m_avoidance;
    /// <summary> 機体タイプ </summary>
    [SerializeField] BodyType m_bodyType = BodyType.Human;    
    /// <summary> 機体出力 </summary>
    public int UnitOutput { get => m_unitOutput; }
    /// <summary> 昇降力 </summary>
    public float LiftingForce { get => m_liftingForce; }
    /// <summary> 移動力 </summary>
    public int MovePower { get => m_movePower; }
    /// <summary> 機体タイプ </summary>
    public BodyType BodyPartsType { get => m_bodyType; }
    /// <summary> ヘッドパーツ接続部 </summary>
    [SerializeField] Transform m_headParts;
    /// <summary> 左手パーツ接続部 </summary>
    [SerializeField] Transform m_lArmParts;
    /// <summary> 右手パーツ接続部 </summary>
    [SerializeField] Transform m_rArmParts;

    /// <summary> 内蔵武器 </summary>
    [SerializeField] WeaponMaster m_weapon;
    /// <summary> 内蔵武器 </summary>
    public WeaponMaster BodyWeapon { get => m_weapon; }
    void Start()
    {
        StartSet();
    }
    protected override void PartsBreak()
    {
        Break = true;
    }
    /// <summary>
    /// 機体の回避力と出力の合計値を返す
    /// </summary>
    /// <returns></returns>
    public int GetAvoidance() 
    {
        return m_avoidance + UnitOutput;
    }
    public override int GetPartsSize()
    {
        int size = PartsSize;
        if (m_weapon)
        {
            size += m_weapon.PartsSize;
        }
        return size;
    }
}
