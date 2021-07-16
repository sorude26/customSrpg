using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユニットのパーツデータを管理するクラス
/// </summary>
public class UnitMaster : MonoBehaviour
{
    /// <summary> 戦闘終了時のイベント </summary>
    public event Action BattleEnd;
    /// <summary> 機体破壊時のイベント </summary>
    public event Action BodyBreak;
    public event Action OnDamage;
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
    protected int m_attackCount = 0;
    protected WeaponMaster m_attackerWeapon = null;
    protected List<IUnitParts> m_damegePartsList;
    /// <summary>
    /// 機体の最大耐久値
    /// </summary>
    /// <returns></returns>
    public int GetMaxHP()
    {
        int hp = 0;
        IUnitParts[] allParts = { m_body, m_head, m_lArm, m_rArm, m_leg };
        foreach (var parts in allParts)
        {
            if (parts != null)
                hp += parts.GetMaxHP();
        }
        return hp;
    }
    /// <summary>
    /// 現在の総パーツ耐久値
    /// </summary>
    /// <returns></returns>
    public int GetCurrentHP()
    {
        int hp = 0;
        IUnitParts[] allParts = { m_body, m_head, m_lArm, m_rArm, m_leg };
        foreach (var parts in allParts)
        {
            if (parts != null)
                hp += parts.GetCurrentHP();
        }
        return hp;
    }
    /// <summary>
    /// 現在の移動力
    /// </summary>
    /// <returns></returns>
    public int GetMovePower()
    {
        int move = m_body.MovePower;
        if (m_leg)
        {
            move += m_leg.CurrentMovePower;
        }
        if (m_body.UnitOutput - GetWeight() * 2 > 0)
        {
            move += 5;
        }
        return move;
    }
    /// <summary>
    /// 現在の昇降力
    /// </summary>
    /// <returns></returns>
    public float GetLiftingForce()
    {
        float liftingForce = m_body.LiftingForce;
        if (m_leg)
        {
            if (!m_leg.Break)
            {
                liftingForce += m_leg.CurrentLiftingForce;
            }
        }
        return liftingForce;
    }
    /// <summary>
    /// 現在の回避率
    /// </summary>
    /// <returns></returns>
    public int GetAvoidance()
    {
        int avoidance = m_body.GetAvoidance() - GetWeight();
        if (m_leg)
        {
            avoidance += m_leg.CurrentAvoidance;
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
    /// 機体の総重量
    /// </summary>
    /// <returns></returns>
    public int GetWeight()
    {
        int weight = 0;
        IParts[] allParts = { m_body, m_head, m_lArm, m_rArm, m_leg, m_lAWeapon, m_rAWeapon, m_lSWeapon, m_rSWeapon, m_bodyWeapon };
        foreach (var parts in allParts)
        {
            if (parts != null)
            {
                if (parts.GetBreak())
                {
                    weight += parts.GetWeight();
                }
            }
        }
        return weight;
    }
    /// <summary>
    /// 平均装甲値
    /// </summary>
    /// <returns></returns>
    public int GetAmorPoint()
    {
        int count = 0;
        int armor = 0;
        IUnitParts[] allparts = { m_body, m_head, m_lArm, m_rArm, m_leg };
        foreach (var parts in allparts)
        {
            if (parts != null)
            {
                if (parts.GetCurrentHP() > 0)
                {
                    armor += parts.GetDefense();
                    count++;
                }
            }
        }
        if (count == 0)
        {
            return 0;
        }
        return armor / count;
    }
    /// <summary>
    /// 指定箇所の武装使用時の命中精度
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public int GetHitAccuracy(WeaponPosition position)
    {
        int hitAccuray = m_body.HitAccuracy;
        if (m_head)
        {
            hitAccuray += m_head.HitAccuracy;
        }
        switch (position)
        {
            case WeaponPosition.Body:
                hitAccuray += m_bodyWeapon.HitAccuracy;
                break;
            case WeaponPosition.LArm:
                hitAccuray += m_lArm.HitAccuracy;
                hitAccuray += m_lAWeapon.HitAccuracy;
                break;
            case WeaponPosition.RArm:
                hitAccuray += m_rArm.HitAccuracy;
                hitAccuray += m_rAWeapon.HitAccuracy;
                break;
            case WeaponPosition.LShoulder:
                hitAccuray += m_lSWeapon.HitAccuracy;
                break;
            case WeaponPosition.RShoulder:
                hitAccuray += m_rSWeapon.HitAccuracy;
                break;
            default:
                break;
        }
        return hitAccuray;
    }
    /// <summary>
    /// 指定箇所の武装
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public WeaponMaster GetWeapon(WeaponPosition position)
    {
        switch (position)
        {
            case WeaponPosition.Body:
                return m_bodyWeapon;
            case WeaponPosition.LArm:
                return m_lAWeapon;
            case WeaponPosition.RArm:
                return m_rAWeapon;
            case WeaponPosition.LShoulder:
                return m_lSWeapon;
            case WeaponPosition.RShoulder:
                return m_rSWeapon;
            default:
                break;
        }
        return null;
    }
    /// <summary>
    /// 武器の装備箇所
    /// </summary>
    /// <param name="weapon"></param>
    /// <returns></returns>
    public WeaponPosition GetWeaponPosition(WeaponMaster weapon)
    {
        if (weapon == m_bodyWeapon)
        {
            return WeaponPosition.Body;
        }
        else if (weapon == m_lAWeapon)
        {
            return WeaponPosition.LArm;
        }
        else if (weapon == m_rAWeapon)
        {
            return WeaponPosition.RArm;
        }
        else if (weapon == m_lSWeapon)
        {
            return WeaponPosition.LShoulder;
        }
        else if (weapon == m_rSWeapon)
        {
            return WeaponPosition.RShoulder;
        }
        Debug.Log("非装備");
        return WeaponPosition.None;
    }
    /// <summary>
    /// 装備武器の配列を返す
    /// </summary>
    /// <returns></returns>
    public WeaponMaster[] GetWeapons()
    {
        List<WeaponMaster> weaponList = new List<WeaponMaster>();
        WeaponMaster[] weapons = { m_bodyWeapon, m_lAWeapon, m_rAWeapon, m_lSWeapon, m_rSWeapon };
        foreach (var weapon in weapons)
        {
            if (weapon)
            {
                weaponList.Add(weapon);
            }
        }
        return weaponList.ToArray();
    }
    /// <summary>
    /// 最も攻撃力の高い武器
    /// </summary>
    /// <returns></returns>
    public WeaponMaster GetMaxPowerWeapon()
    {
        WeaponMaster[] weapons = { m_bodyWeapon, m_lAWeapon, m_rAWeapon, m_lSWeapon, m_rSWeapon };
        List<WeaponMaster> weaponList = new List<WeaponMaster>();
        foreach (var weapon in weapons)
        {
            if (weapon != null) 
                weaponList.Add(weapon);
        }
        return weaponList.OrderByDescending(weapon => weapon.Power).FirstOrDefault();
    }
    /// <summary>
    /// 最も射程範囲の広い武器
    /// </summary>
    /// <returns></returns>
    public WeaponMaster GetMaxRangeWeapon()
    {
        WeaponMaster[] weapons = { m_bodyWeapon, m_lAWeapon, m_rAWeapon, m_lSWeapon, m_rSWeapon };
        List<WeaponMaster> weaponList = new List<WeaponMaster>();
        foreach (var weapon in weapons)
        {
            if (weapon != null)
                weaponList.Add(weapon);
        }
        return weaponList.OrderByDescending(weapon => (weapon.Range + 1) * 2 * weapon.Range - (weapon.MinRange + 1) * 2 * weapon.MinRange).FirstOrDefault();
    }
    /// <summary>
    /// 命中弾をランダムなパーツに割り振り、ダメージ計算を行う
    /// </summary>
    /// <param name="power"></param>
    public void HitCheckShot(int power)
    {
        if (GetCurrentHP() <= 0)
        {
            return;
        }
        int hitPos = 0;
        IUnitParts[] allParts = { m_body, m_head, m_lArm, m_rArm, m_leg };
        foreach (var parts in allParts)
        {
            if (parts != null)
            {
                if (parts.GetCurrentHP() > 0)
                {
                    hitPos += parts.GetSize();
                }
            }
        }
        int r = UnityEngine.Random.Range(0, hitPos);
        int prb = 0;
        foreach (var parts in allParts)
        {
            if (parts != null)
            {
                if (parts.GetBreak())
                {
                    continue;
                }
                prb += parts.GetSize();
                if (prb > r)
                {
                    parts.Damage(power);
                    m_damegePartsList.Add(parts);
                    break;
                }
            }
        }
    }
    /// <summary>
    /// パーツのダメージエフェクトを再生する
    /// </summary>
    void PlayPartsDamegeEffect()
    {
        if (m_attackCount >= m_damegePartsList.Count)
        {
            return;
        }
        OnDamage?.Invoke();
        m_damegePartsList[m_attackCount].DamageEffect();
        m_attackCount++;
    }
    /// <summary>
    /// 武器にイベントを登録する
    /// </summary>
    /// <param name="weapon"></param>
    public void SetBattleEvent(WeaponMaster weapon)
    {
        m_attackCount = 0;
        m_damegePartsList = new List<IUnitParts>();
        weapon.Attack += PlayPartsDamegeEffect;
        weapon.AttackEnd += BattleEndEvent;
        m_attackerWeapon = weapon;
    }
    /// <summary>
    /// 戦闘終了時のイベント
    /// </summary>
    public void BattleEndEvent()
    {
        if (GetCurrentHP() <= 0)
        {
            BodyBreak?.Invoke();
            BodyBreak = null;
        }
        BattleEnd?.Invoke();
        BattleEnd = null;
    }
    /// <summary>
    /// 胴体を登録する
    /// </summary>
    /// <param name="body"></param>
    public void SetParts(PartsBody body)
    {
        m_body = body;
    }
    /// <summary>
    /// 頭部を登録する
    /// </summary>
    /// <param name="head"></param>
    public void SetParts(PartsHead head)
    {
        m_head = head;
    }
    /// <summary>
    /// 腕部を登録する
    /// </summary>
    /// <param name="arm"></param>
    public void SetParts(PartsArm arm)
    {
        switch (arm.Arm)
        {
            case ArmType.Left:
                m_lArm = arm;
                break;
            case ArmType.Right:
                m_rArm = arm;
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 脚部を登録する
    /// </summary>
    /// <param name="leg"></param>
    public void SetParts(PartsLeg leg)
    {
        m_leg = leg;
    }
    /// <summary>
    /// 武装を振り分け登録する
    /// </summary>
    /// <param name="weapon"></param>
    public void SetParts(WeaponMaster weapon)
    {
        switch (weapon.WPosition)
        {
            case WeaponPosition.LArm:
                m_lAWeapon = weapon;
                break;
            case WeaponPosition.RArm:
                m_rAWeapon = weapon;
                break;
            case WeaponPosition.LShoulder:
                m_lSWeapon = weapon;
                break;
            case WeaponPosition.RShoulder:
                m_rAWeapon = weapon;
                break;
            case WeaponPosition.Body:
                m_bodyWeapon = weapon;
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 機体の色を変更する
    /// </summary>
    /// <param name="color"></param>
    public void UnitColorChange(Color color)
    {
        IUnitParts[] allParts = { m_body, m_head, m_lArm, m_rArm, m_leg };
        foreach (var parts in allParts)
        {
            if (parts != null)
            {
                parts.ColorChange(color);
            }
        }
    }
}
