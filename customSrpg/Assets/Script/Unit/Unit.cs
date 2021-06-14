using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユニットの基底クラス
/// </summary>
public class Unit : MonoBehaviour
{
    [SerializeField] protected UnitMaster m_master;
    [SerializeField] protected UnitMovelControl m_movelControl;
    /// <summary> 初期座標 </summary>
    [SerializeField] protected Vector2Int m_startPos;
    public UnitMaster GetUnitData() { return m_master; }
}
