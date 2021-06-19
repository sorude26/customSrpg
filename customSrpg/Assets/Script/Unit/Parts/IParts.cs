using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// パーツが持つ情報を返す為のインターフェース
/// </summary>
public interface IParts 
{
    int GetID();
    int GetWeight();
    int GetSize();
    bool GetBreak();
}
