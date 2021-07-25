using System;
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
    /// <summary> 停止中 </summary>
    Stop,
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
    /// <summary> ユニットの組み立てを行う </summary>
    [SerializeField] protected UnitBuilder m_builder;
    [SerializeField] protected int m_unitNumber;
    [Tooltip("初期座標 X:X座標、Y:Z座標")]
    [SerializeField] protected Vector2Int m_startPos;
    /// <summary> 現在のX座標 </summary>
    public int CurrentPosX { get; protected set; }
    /// <summary> 現在のZ座標 </summary>
    public int CurrentPosZ { get; protected set; }
    /// <summary> ユニットの状態 </summary>
    public UnitState State { get; protected set; }
    //仮データ
    [SerializeField] UnitBuildData m_data;
    /// <summary>
    /// 初期化処理
    /// </summary>
    public virtual void StartSet()
    {
        State = UnitState.Stop;
        CurrentPosX = m_startPos.x;
        CurrentPosZ = m_startPos.y;
        m_movelControl.StartSet(SetCurrentPos, m_startPos.x, m_startPos.y);
        m_master.BodyBreak += UnitDestroy;
        m_builder.SetData(m_data, m_master);
        m_motion.StartSet();
        m_movelControl.MoveStartEvent += m_motion.Walk;
        m_master.OnDamage += m_motion.Damage;
        var lArm = m_master.GetWeapon(WeaponPosition.LArm);
        if (lArm) { lArm.OnAttackMode += m_motion.LArmAttack; }
        var rArm = m_master.GetWeapon(WeaponPosition.RArm);
        if (rArm) { rArm.OnAttackMode += m_motion.RArmAttack; }
    }
    /// <summary>
    /// 機体データ
    /// </summary>
    /// <returns></returns>
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
        m_motion.Wait();
    }
    /// <summary>
    /// 指定した地点へ移動させる
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public void TargetMoveStart(int x, int z)
    {
        m_movelControl.UnitMoveSet(MapManager.Instance.MapDatas, x, z, m_master.GetLiftingForce());
    }
    /// <summary>
    /// 移動終了時のイベントを登録する
    /// </summary>
    /// <param name="action"></param>
    public void SetMoveEvent(Action action)
    {
        m_movelControl.MoveEndEvent += action;
    }   
    /// <summary>
    /// 瞬時に目的地へ移動する
    /// </summary>
    public void MoveSkep()
    {
        m_movelControl.SkipMove();
    }
    /// <summary>
    /// 移動を終了し、位置を保存する
    /// </summary>
    public void MoveEnd()
    {
        m_movelControl.MoveEnd();
    }
    /// <summary>
    /// 初期地点へ戻る
    /// </summary>
    public void ReturnMove()
    {
        m_movelControl.ReturnMove();
    }
    /// <summary>
    /// 標的位置へ向く
    /// </summary>
    /// <param name="target"></param>
    public void TargetLook(Unit target)
    {
        m_movelControl.TargetLook(target.transform.position);
    }
    public void AttackMoveStart()
    {
        m_movelControl.AttackMoveStart();
    }
    public void AttackMoveReturn()
    {
        m_movelControl.AttackMoveReturn();
    }
    /// <summary>
    /// 停止、休息中のユニットを待機状態にする
    /// </summary>
    public virtual void WakeUp()
    {
        if (State == UnitState.Stop || State == UnitState.Rest)
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
    /// 停止、行動中のユニットを休息状態にする
    /// </summary>
    public virtual void UnitRest()
    {
        if (State == UnitState.Stop || State == UnitState.Action)
        {
            State = UnitState.Rest;
            m_motion.Wait();
        }
    }
    /// <summary>
    /// ユニットを停止状態にする
    /// </summary>
    public virtual void TurnEnd()
    {
        if (State != UnitState.Destory)
        {
            State = UnitState.Stop;
        }
    }
    /// <summary>
    /// ユニットの撃破時に呼ばれ、戦闘不能にする
    /// </summary>
    protected virtual void UnitDestroy()
    {
        EffectManager.PlayEffect(EffectType.ExplosionUnit, transform.position);
        State = UnitState.Destory;
        m_motion.Destroy();
    }
    /// <summary>
    /// 攻撃力と命中率に対応した得点を返す
    /// </summary>
    /// <param name="power"></param>
    /// <param name="hit"></param>
    /// <returns></returns>
    public virtual int GetScore(int power, int hit) =>
        BattleManager.Instance.GetPointDurable(m_master.GetMaxHP(), m_master.GetCurrentHP())
        + BattleManager.Instance.GetPointDamage(BattleCalculator.EstimatedDamage(power, m_master.GetAmorPoint(),
            BattleManager.Instance.GetHit( hit , m_master.GetAvoidance())), m_master.GetCurrentHP());
}
