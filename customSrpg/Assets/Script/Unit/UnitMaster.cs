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
    /// <summary> 被ダメージ時のイベント </summary>
    public event Action OnDamage;
    /// <summary> 機体胴体 </summary>
    public PartsBody Body { get; protected set; } = null;
    /// <summary> 機体頭部 </summary>
    public PartsHead Head { get; protected set; } = null;
    /// <summary> 機体左手 </summary>
    public PartsArm LArm { get; protected set; } = null;
    /// <summary> 機体右手 </summary>
    public PartsArm RArm { get; protected set; } = null;
    /// <summary> 機体脚部 </summary>
    public PartsLeg Leg { get; protected set; } = null;
    /// <summary> 左手武器 </summary>
    public WeaponMaster LAWeapon { get; protected set; } = null;
    /// <summary> 右手武器 </summary>
    public WeaponMaster RAWeapon { get; protected set; } = null;
    /// <summary> 肩武器 </summary>
    public WeaponMaster SWeapon { get; protected set; } = null;
    /// <summary> 右肩武器 </summary>
    public WeaponMaster RSWeapon { get; protected set; } = null;
    /// <summary> 胴体武器 </summary>
    public WeaponMaster BodyWeapon { get; protected set; } = null;
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
        IUnitParts[] allParts = { Body, Head, LArm, RArm, Leg };
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
        IUnitParts[] allParts = { Body, Head, LArm, RArm, Leg };
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
        int move = Body.MovePower;
        if (Leg)
        {
            move += Leg.CurrentMovePower;
        }
        if (Body.UnitOutput - GetWeight() * 2 > 0)
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
        float liftingForce = Body.LiftingForce;
        if (Leg)
        {
            if (!Leg.Break)
            {
                liftingForce += Leg.CurrentLiftingForce;
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
        int avoidance = Body.GetAvoidance() - GetWeight();
        if (avoidance < 0)
        {
            avoidance = 0;
        }
        if (Leg)
        {
            avoidance += Leg.CurrentAvoidance;
        }
        if (Head)
        {
            if (!Head.Break)
            {
                avoidance += Head.Avoidance;
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
        IParts[] allParts = { Body, Head, LArm, RArm, Leg, LAWeapon, RAWeapon, SWeapon, RSWeapon, BodyWeapon };
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
        IUnitParts[] allparts = { Body, Head, LArm, RArm, Leg };
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
        int hitAccuray = Body.HitAccuracy;
        if (Head)
        {
            hitAccuray += Head.HitAccuracy;
        }
        switch (position)
        {
            case WeaponPosition.Body:
                if (BodyWeapon)
                hitAccuray += BodyWeapon.HitAccuracy;
                break;
            case WeaponPosition.LArm:
                hitAccuray += LArm.HitAccuracy;
                if(LAWeapon)
                hitAccuray += LAWeapon.HitAccuracy;
                break;
            case WeaponPosition.RArm:
                hitAccuray += RArm.HitAccuracy;
                if (RAWeapon)
                hitAccuray += RAWeapon.HitAccuracy;
                break;
            case WeaponPosition.Shoulder:
                if(SWeapon)
                hitAccuray += SWeapon.HitAccuracy;
                break;
            case WeaponPosition.RShoulder:
                if(RSWeapon)
                hitAccuray += RSWeapon.HitAccuracy;
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
                return BodyWeapon;
            case WeaponPosition.LArm:
                return LAWeapon;
            case WeaponPosition.RArm:
                return RAWeapon;
            case WeaponPosition.Shoulder:
                return SWeapon;
            case WeaponPosition.RShoulder:
                return RSWeapon;
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
        if (weapon == BodyWeapon)
        {
            return WeaponPosition.Body;
        }
        else if (weapon == LAWeapon)
        {
            return WeaponPosition.LArm;
        }
        else if (weapon == RAWeapon)
        {
            return WeaponPosition.RArm;
        }
        else if (weapon == SWeapon)
        {
            return WeaponPosition.Shoulder;
        }
        else if (weapon == RSWeapon)
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
        WeaponMaster[] weapons = { BodyWeapon, LAWeapon, RAWeapon, SWeapon, RSWeapon };
        foreach (var weapon in weapons)
        {
            if (weapon != null && !weapon.Break)
                weaponList.Add(weapon);
        }
        return weaponList.ToArray();
    }
    /// <summary>
    /// 最も攻撃力の高い武器
    /// </summary>
    /// <returns></returns>
    public WeaponMaster GetMaxPowerWeapon()
    {
        WeaponMaster[] weapons = { BodyWeapon, LAWeapon, RAWeapon, SWeapon, RSWeapon };
        List<WeaponMaster> weaponList = new List<WeaponMaster>();
        foreach (var weapon in weapons)
        {
            if (weapon != null && !weapon.Break) 
                weaponList.Add(weapon);
        }
        return weaponList.OrderByDescending(weapon => weapon.MaxPower).FirstOrDefault();
    }
    /// <summary>
    /// 最も射程範囲の広い武器
    /// </summary>
    /// <returns></returns>
    public WeaponMaster GetMaxRangeWeapon()
    {
        WeaponMaster[] weapons = { BodyWeapon, LAWeapon, RAWeapon, SWeapon, RSWeapon };
        List<WeaponMaster> weaponList = new List<WeaponMaster>();
        foreach (var weapon in weapons)
        {
            if (weapon != null && !weapon.Break)
                weaponList.Add(weapon);
        }
        return weaponList.OrderByDescending(weapon => (weapon.Range + 1) * 2 * weapon.Range - (weapon.MinRange + 1) * 2 * weapon.MinRange).FirstOrDefault();
    }
    /// <summary>
    /// 命中弾をランダムなパーツに割り振り、ダメージ計算を行う
    /// </summary>
    /// <param name="power"></param>
    public int HitCheckShot(int power)
    {
        int damage = 0;
        if (GetCurrentHP() <= 0)
        {
            return damage;
        }
        int hitPos = 0;
        IUnitParts[] allParts = { Body, Head, LArm, RArm, Leg };
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
                    damage = parts.Damage(power);
                    m_damegePartsList.Add(parts);
                    break;
                }
            }
        }
        return damage;
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
        m_damegePartsList[m_attackCount].DamageEffect();
        OnDamage?.Invoke();
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
        weapon.OnAttack += PlayPartsDamegeEffect;
        weapon.OnAttackEnd += BattleEndEvent;
        m_attackerWeapon = weapon;
    }
    /// <summary>
    /// 戦闘終了時のイベント
    /// </summary>
    public void BattleEndEvent()
    {
        if (Body.CurrentPartsHp <= 0)
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
        if (!body) { return;}
        Body = body;
    }
    /// <summary>
    /// 頭部を登録する
    /// </summary>
    /// <param name="head"></param>
    public void SetParts(PartsHead head)
    {
        if (!head) { return; }
        Head = head;
    }
    /// <summary>
    /// 腕部を登録する
    /// </summary>
    /// <param name="arm"></param>
    public void SetParts(PartsArm arm)
    {
        if (!arm) { return; }
        switch (arm.Arm)
        {
            case ArmType.Left:
                LArm = arm;
                break;
            case ArmType.Right:
                RArm = arm;
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
        if (!leg) { return; }
        Leg = leg;
    }
    /// <summary>
    /// 武装を振り分け登録する
    /// </summary>
    /// <param name="weapon"></param>
    public void SetParts(WeaponMaster weapon)
    {
        if (!weapon) { return; }
        switch (weapon.WeaponPos)
        {
            case WeaponPosition.LArm:
                LAWeapon = weapon;
                LArm.SetGripWeapon(LAWeapon);
                break;
            case WeaponPosition.RArm:
                RAWeapon = weapon;
                RArm.SetGripWeapon(RAWeapon);
                break;
            case WeaponPosition.Shoulder:
                SWeapon = weapon;
                break;
            case WeaponPosition.RShoulder:
                RAWeapon = weapon;
                break;
            case WeaponPosition.Body:
                BodyWeapon = weapon;
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
        IUnitParts[] allParts = { Body, Head, LArm, RArm, Leg };
        foreach (var parts in allParts)
        {
            if (parts != null)
            {
                parts.PartsColorChange(color);
            }
        }
    }
    public Animator[] GetAnimators()
    {
        List<Animator> animators = new List<Animator>();
        IParts[] allParts = { Body, Head, LArm, RArm, Leg, LAWeapon, RAWeapon, SWeapon, RSWeapon, BodyWeapon };
        int count = 0;
        foreach (var parts in allParts)
        {
            if (parts != null && parts.PartsAnime != null)
            {
                animators.Add(parts.PartsAnime);
                count++;
            }
        }
        if (count == 0)
        {
            return null;
        }
        return animators.ToArray();
    }
}
