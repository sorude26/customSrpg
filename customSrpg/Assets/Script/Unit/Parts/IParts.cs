using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// パーツが持つ情報を返す為のインターフェース
/// </summary>
public interface IParts
{
    /// <summary>
    /// パーツID
    /// </summary>
    int PartsID { get; }
    /// <summary>
    /// 破壊フラグ
    /// </summary>
    bool Break { get; }
    /// <summary>
    /// 重量
    /// </summary>
    int Weight { get; }
    /// <summary>
    /// パーツのサイズを返す
    /// </summary>
    /// <returns></returns>
    int GetSize();
    /// <summary>
    /// パーツを消す
    /// </summary>
    void DestoryParts();
    /// <summary>
    /// パーツ固有アニメーション
    /// </summary>
    Animator PartsAnime { get; }
}
