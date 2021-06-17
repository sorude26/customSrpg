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
    [SerializeField] protected MotionController m_motion;
    /// <summary> 初期座標 </summary>
    [SerializeField] protected Vector2Int m_startPos;
    /// <summary> 現在のX座標 </summary>
    public int CurrentPosX { get; protected set; }
    /// <summary> 現在のZ座標 </summary>
    public int CurrentPosZ { get; protected set; }

    //仮データ
    /// <summary> 機体胴体 </summary>
    [SerializeField] protected PartsBody m_body = null;
    /// <summary> 機体頭部 </summary>
    [SerializeField] protected PartsHead m_head = null;
    /// <summary> 機体左手 </summary>
    [SerializeField] protected PartsArm m_lArm = null;
    /// <summary> 機体右手 </summary>
    [SerializeField] protected PartsArm m_rArm = null;
    /// <summary> 機体脚部 </summary>
    [SerializeField] protected PartsLeg m_leg = null;
    private void Start()
    {
        CurrentPosX = m_startPos.x;
        CurrentPosZ = m_startPos.y;
        m_movelControl.SetOwner(this);
        m_master.SetParts(m_body);
        m_master.SetParts(m_head);
        m_master.SetParts(m_lArm);
        m_master.SetParts(m_rArm);
        m_master.SetParts(m_leg);
    }
    public UnitMaster GetUnitData() { return m_master; }
    /// <summary>
    /// 現在のユニット位置を設定する
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public void SetCurrentPos(int x, int z)
    {
        CurrentPosX = x;
        CurrentPosZ = z;
    }
    public void TargetPositionMoveStart(int x, int z)
    {
        m_movelControl.UnitMoveSet(MapManager.Instance.MapDatas, x, z);
        m_motion.MotionTypeChange(MotionType.Walk);
    }
}
