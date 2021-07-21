using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 胴体パーツ
/// </summary>
public class PartsBody : UnitPartsMaster<BodyData> ,IUnitParts
{
    /// <summary> 機体出力 </summary>
    public int UnitOutput { get => m_partsData.UnitOutput; }
    /// <summary> 昇降力 </summary>
    public float LiftingForce { get => m_partsData.LiftingForce; }
    /// <summary> 移動力 </summary>
    public int MovePower { get => m_partsData.MovePower; }
    /// <summary> 命中精度 </summary>
    public int HitAccuracy { get => m_partsData.HitAccuracy; }
    /// <summary> 機体タイプ </summary>
    public UnitType BodyPartsType { get => m_partsData.BodyPartsType; }
    [Tooltip("頭部パーツ接続部")]
    [SerializeField] Transform m_headParts;
    [Tooltip("左手パーツ接続部")]
    [SerializeField] Transform m_lArmParts;
    [Tooltip("右手パーツ接続部")]
    [SerializeField] Transform m_rArmParts;
    [Tooltip("内蔵武器")]
    [SerializeField] WeaponMaster m_weapon;
    /// <summary> 頭部パーツ接続部 </summary>
    public Transform HeadPos { get => m_headParts; }
    /// <summary> 左手パーツ接続部 </summary>
    public Transform LArmPos { get => m_lArmParts; }
    /// <summary> 右手パーツ接続部 </summary>
    public Transform RArmPos { get => m_rArmParts; }
    /// <summary> 内蔵武器 </summary>
    public WeaponMaster BodyWeapon { get => m_weapon; }
    
    protected override void PartsBreak()
    {
        Break = true;
    }   
    /// <summary>
    /// 機体の回避力と出力の合計値を返す
    /// </summary>
    /// <returns></returns>
    public int GetAvoidance() => m_partsData.Avoidance + UnitOutput;
    /// <summary>
    /// 武装込みのサイズを返す
    /// </summary>
    /// <returns></returns>
    public override int GetSize()
    {
        int size = PartsSize;
        if (m_weapon)
        {
            size += m_weapon.PartsSize;
        }
        return size;
    }
}
