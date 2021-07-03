using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleMode
{
    None,
    Guard,
    Counter,
}
[CreateAssetMenu]
public class BattleCalculator : ScriptableObject
{
    [Tooltip("命中率基礎値")]
    [SerializeField] int m_baseHit = 50;
    [Tooltip("耐久値点数、x：割合、y：点数、最大割合から順に設定する")]
    [SerializeField] Vector2Int[] m_durablePercent;
    [Tooltip("ダメージ値点数、x：割合、y：点数、最大割合から順に設定する")]
    [SerializeField] Vector2Int[] m_damagePercent;
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
    public static int GetDamage(int attack, int defense)
    {
        float r = Random.Range(0.8f, 1.2f);
        r = attack * attack / (attack / 2 + defense) * r;
        return (int)r;
    }
    public static int EstimatedDamage(int attack, int defense, int hit) =>
        (attack * attack / (attack / 2 + defense)) * hit / 100;
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
