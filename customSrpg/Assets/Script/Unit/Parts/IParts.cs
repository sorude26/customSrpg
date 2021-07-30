using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// パーツが持つ情報を返す為のインターフェース
/// </summary>
public interface IParts 
{
    /// <summary>
    /// IDを返す
    /// </summary>
    /// <returns></returns>
    int GetID();
    /// <summary>
    /// 重量を返す
    /// </summary>
    /// <returns></returns>
    int GetWeight();
    /// <summary>
    /// パーツのサイズを返す
    /// </summary>
    /// <returns></returns>
    int GetSize();
    /// <summary>
    /// 破壊されているかを返す
    /// </summary>
    /// <returns></returns>
    bool GetBreak();
    /// <summary>
    /// パーツを消す
    /// </summary>
    void DestoryParts();
}
