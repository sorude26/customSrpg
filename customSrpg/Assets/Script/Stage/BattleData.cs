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
/// ダメージ計算を扱う
/// </summary>
public class BattleData
{
    public static int GetDamage(int attack,int defense)
    {
        float r = Random.Range(0.8f, 1.2f);
        r = attack * attack / (attack / 2 + defense) * r;
        return (int)r;
    }
    public static int GetDamage(int attack, int defense,BattleMode mode)
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
}
