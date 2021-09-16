using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PopParts : ScriptableObject
{
    [Tooltip("胴体パターン")]
    [SerializeField] PartsBody[] m_popBody;
    [Tooltip("頭部パターン")]
    [SerializeField] PartsHead[] m_popHead;
    [Tooltip("右腕パターン")]
    [SerializeField] PartsArm[] m_popRArm;
    [Tooltip("左腕パターン")]
    [SerializeField] PartsArm[] m_popLArm;
    [Tooltip("脚部パターン")]
    [SerializeField] PartsLeg[] m_popLeg;
    [Tooltip("武装パターン")]
    [SerializeField] WeaponMaster[] m_popWeapon;
    /// <summary>
    /// ランダムな構築データを返す
    /// </summary>
    /// <returns></returns>
    public UnitBuildData PopUnitR()
    {
        int b = Random.Range(0, m_popBody.Length);
        int h = Random.Range(0, m_popHead.Length);
        int ar = Random.Range(0, m_popRArm.Length);
        int al = Random.Range(0, m_popLArm.Length);
        int l = Random.Range(0, m_popLeg.Length);
        int wr = Random.Range(0, m_popWeapon.Length);
        int wl = Random.Range(0, m_popWeapon.Length);
        return new UnitBuildData(m_popHead[h].PartsID, m_popBody[b].PartsID, m_popRArm[ar].PartsID, m_popLArm[al].PartsID, m_popLeg[l].PartsID,
            m_popWeapon[wr].PartsID, m_popWeapon[wl].PartsID);
    }
    /// <summary>
    /// 一定のパターンの構築データを返す（要：武器以外の個数統一）
    /// </summary>
    /// <returns></returns>
    public UnitBuildData PopUnitP()
    {
        int p = Random.Range(0, m_popBody.Length);
        int wr = Random.Range(0, m_popWeapon.Length);
        int wl = Random.Range(0, m_popWeapon.Length);
        return new UnitBuildData(m_popHead[p].PartsID, m_popBody[p].PartsID, m_popRArm[p].PartsID, m_popLArm[p].PartsID, m_popLeg[p].PartsID,
            m_popWeapon[wr].PartsID, m_popWeapon[wl].PartsID);
    }
}
