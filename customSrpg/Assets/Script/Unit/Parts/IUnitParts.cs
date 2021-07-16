using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユニットパーツが持つ情報を返す為のインターフェース
/// </summary>
public interface IUnitParts : IParts ,IBattleEffect
{
    int GetMaxHP();
    int GetCurrentHP();
    int GetDefense();
    void Damage(int damage);
    void ColorChange(Color color);
}
