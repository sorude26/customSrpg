using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユニットの状態
/// </summary>
public enum UnitState
{
    /// <summary> 行動待機 </summary>
    StandBy,
    /// <summary> 行動中 </summary>
    Action,
    /// <summary> 休息中 </summary>
    Rest,
    /// <summary> 戦闘不能 </summary>
    Destory,
}
/// <summary>
/// ユニットの基底クラス
/// </summary>
public class Unit : MonoBehaviour
{
    /// <summary> ユニットのデータを持つ </summary>
    [SerializeField] protected UnitMaster m_master;
    /// <summary> ユニットの移動を制御する </summary>
    [SerializeField] protected UnitMovelControl m_movelControl;
    /// <summary> ユニットの行動を制御する </summary>
    [SerializeField] protected MotionController m_motion;
    /// <summary> 初期座標 </summary>
    [SerializeField] protected Vector2Int m_startPos;
    /// <summary> 現在のX座標 </summary>
    public int CurrentPosX { get; protected set; }
    /// <summary> 現在のZ座標 </summary>
    public int CurrentPosZ { get; protected set; }
    /// <summary> ユニットの状態 </summary>
    public UnitState State { get; protected set; }
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
    [SerializeField] protected WeaponMaster m_testWeapom = null;
    private void Start()
    {
        StartSet();
    }
    protected virtual void StartSet()
    {
        State = UnitState.StandBy;
        CurrentPosX = m_startPos.x;
        CurrentPosZ = m_startPos.y;
        m_movelControl.StartSet(SetCurrentPos, m_startPos.x, m_startPos.y);
        m_master.BodyBreak += UnitDestroy;
        m_master.SetParts(m_body);
        m_master.SetParts(m_head);
        m_master.SetParts(m_lArm);
        m_master.SetParts(m_rArm);
        m_master.SetParts(m_leg);
        m_master.SetParts(m_testWeapom);
    }
    public UnitMaster GetUnitData() => m_master;
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
    /// <summary>
    /// 指定した地点へ移動させる
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public void TargetMoveStart(int x, int z)
    {
        m_movelControl.UnitMoveSet(MapManager.Instance.MapDatas, x, z, m_master.GetLiftingForce());
        m_motion.MotionTypeChange(MotionType.Walk);
    }
    public void MoveSkep()
    {
        m_movelControl.SkipMove();
    }
    public void MoveEnd()
    {
        m_movelControl.MoveEnd();
    }
    public void TargetLook(Unit target)
    {
        m_movelControl.TargetLook(target.transform.position);
    }
    /// <summary>
    /// 休息中のユニットを待機状態にする
    /// </summary>
    public virtual void WakeUp()
    {
        if (State == UnitState.Rest)
        {
            State = UnitState.StandBy;
        }
    }
    /// <summary>
    /// 待機中のユニットを行動状態にする
    /// </summary>
    public virtual void StartUp()
    {
        if (State == UnitState.StandBy)
        {
            State = UnitState.Action;
        }
    }
    /// <summary>
    /// 行動中のユニットを休息状態にする
    /// </summary>
    public virtual void ActionEnd()
    {
        if (State == UnitState.Action)
        {
            State = UnitState.Rest;
            StageManager.Instance.NextUnit();
        }
    }
    /// <summary>
    /// ユニットの撃破時に呼ばれ、戦闘不能にする
    /// </summary>
    protected virtual void UnitDestroy()
    {
        m_master.BodyBreak -= UnitDestroy;
        EffectManager.PlayEffect(EffectType.ExplosionUnit, transform.position);
        State = UnitState.Destory;
        gameObject.SetActive(false);
    }
}
