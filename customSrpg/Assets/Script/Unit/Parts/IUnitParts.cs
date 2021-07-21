using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユニットパーツが持つ情報を返す為のインターフェース
/// </summary>
public interface IUnitParts : IParts ,IBattleEffect
{
    /// <summary>
    /// パーツの最大耐久値を返す
    /// </summary>
    /// <returns></returns>
    int GetMaxHP();
    /// <summary>
    /// パーツの現在耐久値を返す
    /// </summary>
    /// <returns></returns>
    int GetCurrentHP();
    /// <summary>
    /// パーツの防御力を返す
    /// </summary>
    /// <returns></returns>
    int GetDefense();
    /// <summary>
    /// パーツにダメージを与える
    /// </summary>
    /// <param name="damage"></param>
    int Damage(int damage);
    /// <summary>
    /// パーツの色変更
    /// </summary>
    /// <param name="color"></param>
    void ColorChange(Color color);
}
