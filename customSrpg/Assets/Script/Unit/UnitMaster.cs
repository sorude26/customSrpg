using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AttackParts
{
    LArm,
    RArm,
    LShoulder,
    RShoulder,
    Body,
}
public class UnitMaster : MonoBehaviour
{
    /// <summary> 機体胴体 </summary>
    protected PartsBody m_body = null;
    /// <summary> 機体頭部 </summary>
    protected PartsHead m_head = null;
    /// <summary> 機体左手 </summary>
    protected PartsArm m_lArm = null;
    /// <summary> 機体右手 </summary>
    protected PartsArm m_rArm = null;
    /// <summary> 機体脚部 </summary>
    protected PartsLeg m_leg = null;
    /// <summary> 左手武器 </summary>
    protected WeaponMaster m_lAWeapon = null;
    /// <summary> 右手武器 </summary>
    protected WeaponMaster m_rAWeapon = null;
    /// <summary> 左肩武器 </summary>
    protected WeaponMaster m_lSWeapon = null;
    /// <summary> 右肩武器 </summary>
    protected WeaponMaster m_rSWeapon = null;
    /// <summary> 胴体武器 </summary>
    protected WeaponMaster m_bodyWeapon = null;

    /// <summary>
    /// 現在の総パーツ耐久値を返す
    /// </summary>
    /// <returns></returns>
    public int GetCurrentHP()
    {
        int hp = 0;
        UnitPartsMaster[] allParts = { m_body, m_head, m_lArm, m_rArm, m_leg };
        foreach (var parts in allParts)
        {
            if (parts)
            {
                hp += parts.CurrentPartsHp;
            }
        }
        return hp;
    }
    /// <summary>
    /// 現在の回避率を返す
    /// </summary>
    /// <returns></returns>
    public int GetAvoidance()
    {
        int avoidance = m_body.GetAvoidance() - GetWeight();
        if (m_leg)
        {
            if (!m_leg.Break)
            {
                avoidance += m_leg.Avoidance;
            }
        }
        if (m_head)
        {
            if (!m_head.Break)
            {
                avoidance += m_head.Avoidance;
            }
        }
        return avoidance;
    }
    /// <summary>
    /// 機体の総重量を返す
    /// </summary>
    /// <returns></returns>
    public int GetWeight()
    {
        int weight = 0;
        PartsMaster[] allParts = { m_body, m_head, m_lArm, m_rArm, m_leg, m_lAWeapon, m_rAWeapon, m_lSWeapon, m_rSWeapon, m_bodyWeapon };
        foreach (var parts in allParts)
        {
            if (parts)
            {
                if (!parts.Break)
                {
                    weight += parts.Weight;
                }
            }
        }
        return weight;
    }
    /// <summary>
    /// 平均装甲値を返す
    /// </summary>
    /// <returns></returns>
    public int GetAmorPoint()
    {
        int count = 0;
        int armor = 0;
        UnitPartsMaster[] allParts = { m_body, m_head, m_lArm, m_rArm, m_leg };
        foreach (var parts in allParts)
        {
            if (parts)
            {
                if (parts.Break)
                {
                    continue;
                }
                armor += parts.Defense;
                count++;
            }
        }
        return armor / count;
    }
    /// <summary>
    /// 命中弾をランダムなパーツに割り振り、ダメージ計算を行わせる
    /// </summary>
    /// <param name="hitDamage"></param>
    public void HitCheckShot(int hitDamage)
    {
        int hitPos = 0;
        UnitPartsMaster[] allParts = { m_body, m_head, m_lArm, m_rArm, m_leg };
        foreach (var parts in allParts)
        {
            if (parts)
            {
                if (parts.CurrentPartsHp > 0)
                {
                    continue;
                }
                hitPos += parts.GetPartsSize();
            }
        }
        int r = Random.Range(0, hitPos);
        int prb = 0;
        foreach (var parts in allParts)
        {
            if (parts)
            {
                if (parts.CurrentPartsHp > 0)
                {
                    continue;
                }
                prb += parts.GetPartsSize();
                if (prb > r)
                {
                    parts.Damage(hitDamage);
                    break;
                }
            }
        }
    }
}
