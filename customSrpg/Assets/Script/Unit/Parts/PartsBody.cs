using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public BodyType BodyPartsType { get => m_partsData.BodyPartsType; }
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
