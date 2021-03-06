using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 戦闘処理担当
/// </summary>
public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }
    [Tooltip("ダメージ計算機")]
    [SerializeField] BattleCalculator m_calculator;
    /// <summary> 攻撃者 </summary>
    Unit m_attacker;
    /// <summary> 攻撃対象リスト </summary>
    List<Unit> m_attackTarget = new List<Unit>();
    /// <summary> 入手用攻撃対象リスト </summary>
    public List<Unit> AttackTarget { get => m_attackTarget; }
    /// <summary> 攻撃対象 </summary>
    Unit m_target;
    /// <summary> 攻撃中フラグ </summary>
    public bool AttackNow { get; private set; }
    /// <summary> 攻撃者の武装位置 </summary>
    WeaponPosition m_weaponPos;
    /// <summary> 攻撃時の合計ダメージ </summary>
    int m_totalDamage;
    public event Action BattleEnd;
    string m_getName;
    private void Awake()
    {
        Instance = this;
    }
    /// <summary>
    /// 攻撃者の設定
    /// </summary>
    /// <param name="attacker"></param>
    public void SetAttacker(Unit attacker)
    {
        m_attacker = attacker;
    }
    /// <summary>
    /// 攻撃対象リストの登録をする
    /// </summary>
    public void SetAttackTargets()
    {
        m_attackTarget = new List<Unit>();
        var targetPos = StageManager.Instance.GetAttackTarget();
        var targetUnit = StageManager.Instance.GetStageUnits();
        foreach (var unit in targetUnit)
        {
            var t = targetPos.Where(px => unit.State == UnitState.Stop && px.PosX == unit.CurrentPosX && px.PosZ == unit.CurrentPosZ).FirstOrDefault();
            if (t != null)
            {
                m_attackTarget.Add(unit);
            }
        }
    }
    /// <summary>
    /// 範囲内のユニットを配列で返す
    /// </summary>
    /// <param name="targetPos"></param>
    /// <returns></returns>
    public Unit[] GetAttackTargets(MapData[] targetPos)
    {
        List<Unit> units = new List<Unit>();
        var targetUnit = StageManager.Instance.GetStageUnits();
        foreach (var unit in targetUnit)
        {
            var t = targetPos.Where(px => unit.State == UnitState.Stop && px.PosX == unit.CurrentPosX && px.PosZ == unit.CurrentPosZ).FirstOrDefault();
            if (t != null)
            {
                units.Add(unit);
            }
        }
        return units.ToArray();
    }
    /// <summary>
    /// 攻撃対象設定
    /// </summary>
    /// <param name="targetNum"></param>
    /// <returns></returns>
    public Unit SetTarget(int targetNum)
    {
        if (targetNum >= m_attackTarget.Count)
        {
            Debug.Log("指定対象不在");
            m_target = null;
            return null;
        }
        m_target = m_attackTarget[targetNum];
        return m_target;
    }
    public void SetTarget(Unit target)
    {
        m_target = target;
    }
    /// <summary>
    /// 指定した武器で攻撃を開始する
    /// </summary>
    /// <param name="attackWeapon"></param>
    public bool AttackStart(WeaponPosition attackWeapon)
    {
        SetWeaponPos(attackWeapon);
        return AttackStart();
    }
    /// <summary>
    /// プレイヤーの攻撃開始
    /// </summary>
    public bool AttackStart()
    {
        if (AttackNow)
        {
            Debug.Log("攻撃中");
            return false;
        }
        EventManager.StageGuideViewEnd();
        if (!m_target)
        {
            Debug.Log("攻撃対象不在");
            return false;
        }
        AttackNow = true;
        m_totalDamage = 0;
        m_getName = "";
        m_attacker.TargetLook(m_target);
        m_target.TargetLook(m_attacker);
        StartCoroutine(BattleStart());
        StageManager.Instance.Cursor.Warp(m_target.transform.position, m_attacker.transform.position);
        return true;
    }
    IEnumerator BattleStart()
    {
        yield return StageManager.Instance.Cursor.Camera.PointFocus();
        WeaponMaster weapon = m_attacker.GetUnitData().GetWeapon(m_weaponPos);
        if (weapon.Type == WeaponType.Blade || weapon.Type == WeaponType.Knuckle)
        {
            weapon.OnAttackStart += m_attacker.AttackMoveStart;
            weapon.OnAttackEnd += m_attacker.AttackMoveReturn;
        }
        m_target.GetUnitData().SetBattleEvent(weapon);
        m_target.GetUnitData().BattleEnd += AttackEnd;
        BattleTargetDataView();
        int hit = GetHit(m_weaponPos);
        for (int i = 0; i < weapon.MaxAttackNumber; i++)
        {
            Attack(m_target, hit, weapon.Power);
        }
        weapon.AttackStart();
    }
    /// <summary>
    /// 攻撃箇所登録
    /// </summary>
    /// <param name="weaponPos"></param>
    public void SetWeaponPos(WeaponPosition weaponPos)
    {
        m_weaponPos = weaponPos;
    }
    public void BattleTargetDataView()
    {
        StageManager.Instance.Cursor.BattleUnitView(m_attacker, m_target);
    }
    /// <summary>
    /// 単発攻撃処理
    /// </summary>
    /// <param name="target"></param>
    /// <param name="hit"></param>
    /// <param name="power"></param>
    void Attack(Unit target, int hit, int power)
    {
        int r = UnityEngine.Random.Range(0, 100);
        if (r <= hit)
        {
            m_totalDamage += target.GetUnitData().HitCheckShot(power);
        }
        else
        {
            target.GetUnitData().HitCheckShot(0);
        }
    }
    /// <summary>
    /// 武装の命中率を返す
    /// </summary>
    /// <param name="attackWeapon"></param>
    /// <returns></returns>
    public int GetHit(WeaponPosition attackWeapon) =>
        m_calculator.GetHit(m_attacker.GetUnitData().GetHitAccuracy(attackWeapon), m_target.GetUnitData().GetAvoidance());
    /// <summary>
    /// 武装の命中率を返す
    /// </summary>
    /// <param name="attackWeapon"></param>
    /// <param name="attacker"></param>
    /// <returns></returns>
    public int GetHit(WeaponPosition attackWeapon, Unit attacker) =>
        attacker.GetUnitData().GetHitAccuracy(attackWeapon);
    /// <summary>
    /// 回避を考慮した命中率
    /// </summary>
    /// <param name="hit"></param>
    /// <param name="accuracy"></param>
    /// <returns></returns>
    public int GetHit(int hit, int accuracy) =>
        m_calculator.GetHit(hit, accuracy);
    /// <summary>
    /// 攻撃終了時の処理
    /// </summary>
    void AttackEnd()
    {
        StartCoroutine(AllBattleEnd());
    }
    IEnumerator AllBattleEnd()
    {
        yield return StageManager.Instance.Cursor.Camera.FocusEnd();
        if (m_totalDamage > 0)
        {
            StartCoroutine(StageManager.Instance.Massage.ShortMessageView($"Total:{m_totalDamage}", 5f, 1f, 100));

        }
        else
        {
            StartCoroutine(StageManager.Instance.Massage.ShortMessageView("Miss", 5f, 0.8f, 90));
        }
        yield return new WaitForSeconds(0.05f);
        while (StageManager.Instance.Massage.ViewNow)
        {
            yield return null;
        }
        if (m_getName != "")
        {
            StartCoroutine(StageManager.Instance.Massage.ShortMessageView(m_getName, 3f, 2f, 95));
        }
        yield return new WaitForSeconds(0.05f);
        while (StageManager.Instance.Massage.ViewNow)
        {
            yield return null;
        }
        BattleEnd?.Invoke();
        AttackNow = false;
    }
    /// <summary>
    /// 耐久得点
    /// </summary>
    /// <param name="maxHP"></param>
    /// <param name="currentHP"></param>
    /// <returns></returns>
    public int GetPointDurable(int maxHP, int currentHP) => m_calculator.GetPointDurable(maxHP, currentHP);
    /// <summary>
    /// ダメージ得点
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="currentHP"></param>
    /// <returns></returns>
    public int GetPointDamage(int damage, int currentHP) => m_calculator.GetPointDamage(damage, currentHP);

    public void PartsGet()
    {
        int number = UnityEngine.Random.Range(0, 7);
        if (number == 0)
        {
            var parts = m_target.GetUnitData().Body;
            if (parts != null)
            {
                if (UnitDataMaster.HavePartsDic[PartsType.Body][parts.PartsID] > 0) { return; }
                UnitDataMaster.HavePartsDic[PartsType.Body][parts.PartsID]++;
                Debug.Log($"{parts.PartsName}：Bodyを入手した");
                m_getName = $"Body:{parts.PartsName}、入手しました";
            }
        }
        else if (number == 1)
        {
            var parts = m_target.GetUnitData().Head;
            if (parts != null)
            {
                if (UnitDataMaster.HavePartsDic[PartsType.Head][parts.PartsID] > 0)
                { 
                    return; 
                }
                UnitDataMaster.HavePartsDic[PartsType.Head][parts.PartsID]++;
                Debug.Log($"{parts.PartsName}：Headを入手した");
                m_getName = $"Head:{parts.PartsName}、入手しました";
            }
        }
        else if (number == 2)
        {
            var parts = m_target.GetUnitData().RArm;
            if (parts != null)
            {
                if (UnitDataMaster.HavePartsDic[PartsType.RArm][parts.PartsID] > 0)
                { 
                    return;
                }
                UnitDataMaster.HavePartsDic[PartsType.RArm][parts.PartsID]++;
                Debug.Log($"{parts.PartsName}：RArmを入手した");
                m_getName = $"RArm:{parts.PartsName}、入手しました";
            }
        }
        else if (number == 3)
        {
            var parts = m_target.GetUnitData().LArm;
            if (parts != null)
            {
                if (UnitDataMaster.HavePartsDic[PartsType.LArm][parts.PartsID] > 0)
                {
                    return;
                }
                UnitDataMaster.HavePartsDic[PartsType.LArm][parts.PartsID]++;
                Debug.Log($"{parts.PartsName}：LArmを入手した");
                m_getName = $"LArm:{parts.PartsName}、入手しました";
            }
        }
        else if (number == 4)
        {
            var parts = m_target.GetUnitData().Leg;
            if (parts != null)
            {
                if (UnitDataMaster.HavePartsDic[PartsType.Leg][parts.PartsID] > 0)
                {
                    return;
                }
                UnitDataMaster.HavePartsDic[PartsType.Leg][parts.PartsID]++;
                Debug.Log($"{parts.PartsName}：Legを入手した");
                m_getName = $"Leg:{parts.PartsName}、入手しました";
            }
        }
        else if (number == 5)
        {
            var parts = m_target.GetUnitData().RAWeapon;
            if (parts != null && parts.PartsID > 0)
            {
                if (UnitDataMaster.HavePartsDic[PartsType.Weapon][parts.PartsID] > 0)
                {
                    return;
                }
                UnitDataMaster.HavePartsDic[PartsType.Weapon][parts.PartsID]++;
                Debug.Log($"{parts.PartsName}、入手");
                m_getName = $"Weapon:{parts.PartsName}、入手しました";
            }
        }
        else if (number == 6)
        {
            var parts = m_target.GetUnitData().LAWeapon;
            if (parts != null && parts.PartsID > 0)
            {
                if (UnitDataMaster.HavePartsDic[PartsType.Weapon][parts.PartsID] > 0)
                {
                    return;
                }
                UnitDataMaster.HavePartsDic[PartsType.Weapon][parts.PartsID]++;
                Debug.Log($"{parts.PartsName}、入手");
                m_getName = $"Weapon:{parts.PartsName}、入手しました";
            }
        }
    }
}
