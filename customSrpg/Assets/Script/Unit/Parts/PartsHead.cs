using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsHead : UnitPartsMaster
{
    /// <summary> 回避力 </summary>
    [SerializeField] int m_avoidance;
    /// <summary> 回避力 </summary>
    public int Avoidance { get => m_avoidance; }    
}
