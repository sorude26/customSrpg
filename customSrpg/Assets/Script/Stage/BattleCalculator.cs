using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleMode
{
    None,
    Guard,
    Counter,
}
/// <summary>
/// ダメージ計算、点数計算等の戦闘中計算式を扱う
/// </summary>
[CreateAssetMenu]
public class BattleCalculator : ScriptableObject
{
    [Tooltip("命中率基礎値")]
    [SerializeField] int m_baseHit = 50;
    [Tooltip("耐久値点数、x：割合、y：点数、最大割合から順に設定する")]
    [SerializeField] Vector2Int[] m_durablePercent;
    [Tooltip("ダメージ値点数、x：割合、y：点数、最大割合から順に設定する")]
    [SerializeField] Vector2Int[] m_damagePercent;
    /// <summary>
    /// 耐久力の最大値と現在値の差に応じた点数を返す
    /// </summary>
    /// <param name="maxHP"></param>
    /// <param name="currentHP"></param>
    /// <returns></returns>
    public int GetPointDurable(int maxHP,int currentHP)
    {
        float p = currentHP / (float)maxHP * 100;
        foreach (var point in m_durablePercent)
        {
            if (p >= point.x)
            {
                return point.y;
            }
        }
        return 0;
    }
    /// <summary>
    /// 現在の耐久値とダメージ量に応じた点数を返す
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="currentHP"></param>
    /// <returns></returns>
    public int GetPointDamage(int damage,int currentHP)
    {
        float p = damage / (float)currentHP * 100;
        foreach (var point in m_damagePercent)
        {
            if (p >= point.x)
            {
                return point.y;
            }
        }
        return 0;
    }
    /// <summary>
    /// 防御力を考慮したダメージ量を返す
    /// </summary>
    /// <param name="attack"></param>
    /// <param name="defense"></param>
    /// <returns></returns>
    public static int GetDamage(int attack, int defense)
    {
        float r = Random.Range(0.9f, 1.2f);
        r = attack * attack / (attack / 2 + defense) * r;
        return (int)r;
    }
    /// <summary>
    /// 防御力と命中率を考慮した推定ダメージを返す
    /// </summary>
    /// <param name="attack"></param>
    /// <param name="number"></param>
    /// <param name="defense"></param>
    /// <param name="hit"></param>
    /// <returns></returns>
    public static int EstimatedDamage(int attack, int number, int defense, int hit) =>
        attack * attack / (attack / 2 + defense) * (hit / 100) * number;
    /// <summary>
    /// 行動を考慮したダメージを返す
    /// </summary>
    /// <param name="attack"></param>
    /// <param name="defense"></param>
    /// <param name="mode"></param>
    /// <returns></returns>
    public static int GetDamage(int attack, int defense, BattleMode mode)
    {
        float h = 0f;
        switch (mode)
        {
            case BattleMode.Guard:
                h = 0.5f;
                break;
            case BattleMode.Counter:
                h = -0.2f;
                break;
            default:
                break;
        }
        float r = Random.Range(0.8f - h, 1.2f - h);
        r = attack * attack / (attack / 2 + defense) * r;
        return (int)r;
    }
    /// <summary>
    /// 命中率と回避力から命中率を0から99の範囲で返す
    /// </summary>
    /// <param name="attackerHit"></param>
    /// <param name="targetAvoidance"></param>
    /// <returns></returns>
    public int GetHit(int attackerHit,int targetAvoidance)
    {
        int hit = m_baseHit;
        hit += attackerHit;
        hit -= targetAvoidance;
        if (hit > 99)
        {
            hit = 99;
        }
        else if (hit < 0)
        {
            hit = 0;
        }
        return hit;
    }
}
