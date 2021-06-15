using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MotionType
{
    Wait,
    Walk,
    Jump,
    Landing,
    Fly,
    Dash,
}
/// <summary>
/// 機体ごとにモーションを変更可能にする
/// </summary>
public class MotionController : MonoBehaviour
{
    #region 各部パーツ初期位置
    [SerializeField] protected GameObject Head;
    [SerializeField] protected GameObject Body;
    [SerializeField] protected GameObject armL;
    [SerializeField] protected GameObject armL1;
    [SerializeField] protected GameObject armL2;
    [SerializeField] protected GameObject armR;
    [SerializeField] protected GameObject armR1;
    [SerializeField] protected GameObject armR2;
    [SerializeField] protected GameObject leg;
    [SerializeField] protected GameObject legL1;
    [SerializeField] protected GameObject legL2;
    [SerializeField] protected GameObject legR1;
    [SerializeField] protected GameObject legR2;
    protected Quaternion headRotaion = Quaternion.Euler(0, 0, 0);
    protected float headRSpeed = 1.0f;
    protected Quaternion bodyRotaion = Quaternion.Euler(0, 0, 0);
    protected float bodyRSpeed = 1.0f;
    protected Quaternion lArmRotaion = Quaternion.Euler(0, 0, 0);
    protected float lArmRSpeed = 1.0f;
    protected Quaternion rArmRotaion = Quaternion.Euler(0, 0, 0);
    protected float rArmRSpeed = 1.0f;
    protected Quaternion lArm1Rotaion = Quaternion.Euler(0, 0, 0);
    protected float lArm1RSpeed = 1.0f;
    protected Quaternion lArm2Rotaion = Quaternion.Euler(0, 0, 0);
    protected float lArm2RSpeed = 1.0f;
    protected Quaternion rArm1Rotaion = Quaternion.Euler(0, 0, 0);
    protected float rArm1RSpeed = 1.0f;
    protected Quaternion rArm2Rotaion = Quaternion.Euler(0, 0, 0);
    protected float rArm2RSpeed = 1.0f;
    protected Quaternion legRotaion = Quaternion.Euler(0, 0, 0);
    protected float legRSpeed = 1.0f;
    protected Quaternion legL1Rotaion = Quaternion.Euler(0, 0, 0);
    protected float legL1RSpeed = 1.0f;
    protected Quaternion legL2Rotaion = Quaternion.Euler(0, 0, 0);
    protected float legL2RSpeed = 1.0f;
    protected Quaternion legR1Rotaion = Quaternion.Euler(0, 0, 0);
    protected float legR1RSpeed = 1.0f;
    protected Quaternion legR2Rotaion = Quaternion.Euler(0, 0, 0);
    protected float legR2RSpeed = 1.0f;
    protected Vector3 targtPos = new Vector3(0, 0, 0);
    protected float actionSpeed = 4.0f;
    #endregion

    protected MotionType motionType = MotionType.Wait;
    public bool StayMotion { get; set; }
    //protected Player owner;
    private void Update()
    {
        switch (motionType)
        {
            case MotionType.Wait:
                WaitMotion();
                break;
            case MotionType.Walk:
                WalkMotion();
                break;
            case MotionType.Jump:
                JumpMotion();
                break;
            case MotionType.Landing:
                LandingMotion();
                break;
            case MotionType.Fly:
                FlyMotion();
                break;
            case MotionType.Dash:
                DashMotion();
                break;
            default:
                WaitMotion();
                break;
        }
    }
    private void LateUpdate()
    {
        PartsMotion();
    }
    protected virtual void WaitMotion()
    {
        #region 待機
        headRotaion = Quaternion.Euler(0, 0, 0);
        headRSpeed = 10f;
        bodyRotaion = Quaternion.Euler(0, 0, 0);
        bodyRSpeed = 10f;
        lArmRotaion = Quaternion.Euler(0, 0, 0);
        lArmRSpeed = 10f;
        rArmRotaion = Quaternion.Euler(0, 0, 0);
        rArmRSpeed = 10f;
        legRotaion = Quaternion.Euler(0, 0, 0);
        legRSpeed = 10f;
        legL1Rotaion = Quaternion.Euler(0, 0, 0);
        legL2Rotaion = Quaternion.Euler(0, 0, 0);
        legR1Rotaion = Quaternion.Euler(0, 0, 0);
        legR2Rotaion = Quaternion.Euler(0, 0, 0);
        legL1RSpeed = 10f;
        legL2RSpeed = 10f;
        legR1RSpeed = 10f;
        legR2RSpeed = 10f;
        #endregion
    }
    [SerializeField] protected float moveSpeed = 15f;
    protected float moveMotionTimer = 0;
    protected bool motionStart = false;
    /// <summary>
    /// ターゲットを指定して攻撃する
    /// </summary>
    //protected virtual void TargetShot()
    //{
    //    Vector3 targetPos = owner.target.position;
    //    Vector3 targetDir = targetPos - owner.transform.position;
    //    targetDir.y = 0.0f;
    //    Quaternion p = Quaternion.Euler(0, 180, 0);
    //    Quaternion endRot = Quaternion.LookRotation(targetDir) * p;
    //    bodyRotaion = endRot;
    //    bodyRSpeed = 10f;
    //    rArmRotaion = Quaternion.Euler(0, 0, 0);
    //    rArmRSpeed = 10f;
    //}
    /// <summary>
    /// 歩行
    /// </summary>
    protected virtual void WalkMotion()
    {
        #region 歩行
        if (!motionStart)
        {
            legL1RSpeed = moveSpeed * 0.2f;
            legL2RSpeed = moveSpeed * 0.2f;
            legR1RSpeed = moveSpeed * 0.2f;
            legR2RSpeed = moveSpeed * 0.2f;
            lArmRSpeed = moveSpeed * 0.2f;
            rArmRSpeed = moveSpeed * 0.2f;
            bodyRSpeed = moveSpeed * 0.2f;
            legRSpeed = moveSpeed * 0.2f;
            headRSpeed = moveSpeed * 0.2f;
            motionStart = true;
            moveMotionTimer = 0;
        }
        moveMotionTimer += moveSpeed * Time.deltaTime;
        if (moveMotionTimer <= 10)
        {
            legL1Rotaion = Quaternion.Euler(-40, -20, 5);
            legL2Rotaion = Quaternion.Euler(20, 0, 0);
            legR1Rotaion = Quaternion.Euler(20, -20, -5);
            legR2Rotaion = Quaternion.Euler(10, 0, 0);
            lArmRotaion = Quaternion.Euler(-10, 0, 0);
            rArmRotaion = Quaternion.Euler(10, 0, 0);
            bodyRotaion = Quaternion.Euler(0, -10, 0);
            legRotaion = Quaternion.Euler(0, 20, 0);
            headRotaion = Quaternion.Euler(0, 10, 0);
        }
        else if (moveMotionTimer <= 20)
        {
            legL1Rotaion = Quaternion.Euler(20, 20, 5);
            legL2Rotaion = Quaternion.Euler(10, 0, 0);
            legR1Rotaion = Quaternion.Euler(-40, 20, -5);
            legR2Rotaion = Quaternion.Euler(20, 0, 0);
            lArmRotaion = Quaternion.Euler(10, 0, 0);
            rArmRotaion = Quaternion.Euler(-10, 0, 0);
            bodyRotaion = Quaternion.Euler(0, 10, 0);
            legRotaion = Quaternion.Euler(0, -20, 0);
            headRotaion = Quaternion.Euler(0, -10, 0);
        }
        else
        {
            moveMotionTimer = 0;
        }
        #endregion
    }
    /// <summary>
    /// ジャンプ
    /// </summary>
    protected virtual void JumpMotion()
    {
        legL1RSpeed = moveSpeed;
        legL2RSpeed = moveSpeed;
        legR1RSpeed = moveSpeed;
        legR2RSpeed = moveSpeed;
        lArmRSpeed = moveSpeed;
        rArmRSpeed = moveSpeed;
        bodyRSpeed = moveSpeed;
        legRSpeed = moveSpeed;
        headRSpeed = moveSpeed;
        legL1Rotaion = Quaternion.Euler(-40, 0, 0);
        legL2Rotaion = Quaternion.Euler(80, 0, 0);
        legR1Rotaion = Quaternion.Euler(0, 0, 0);
        legR2Rotaion = Quaternion.Euler(0, 0, 0);
        lArmRotaion = Quaternion.Euler(0, 0, -5);
        rArmRotaion = Quaternion.Euler(0, 0, 5);
        bodyRotaion = Quaternion.Euler(10, 0, 0);
        legRotaion = Quaternion.Euler(0, 0, 0);
        headRotaion = Quaternion.Euler(0, 0, 0);
    }
    /// <summary>
    /// 飛行
    /// </summary>
    protected virtual void FlyMotion()
    {
        legL1RSpeed = moveSpeed;
        legL2RSpeed = moveSpeed;
        legR1RSpeed = moveSpeed;
        legR2RSpeed = moveSpeed;
        lArmRSpeed = moveSpeed;
        rArmRSpeed = moveSpeed;
        bodyRSpeed = moveSpeed;
        legRSpeed = moveSpeed;
        headRSpeed = moveSpeed;
        legL1Rotaion = Quaternion.Euler(0, 0, -3);
        legL2Rotaion = Quaternion.Euler(0, 0, 0);
        legR1Rotaion = Quaternion.Euler(0, 0, 3);
        legR2Rotaion = Quaternion.Euler(0, 0, 0);
        lArmRotaion = Quaternion.Euler(0, 0, -8);
        rArmRotaion = Quaternion.Euler(0, 0, 8);
        bodyRotaion = Quaternion.Euler(30, 0, 0);
        legRotaion = Quaternion.Euler(40, 0, 0);
        headRotaion = Quaternion.Euler(-10, 0, 0);
    }
    /// <summary>
    /// 着地
    /// </summary>
    protected virtual void LandingMotion()
    {
        legL1RSpeed = 8f;
        legL2RSpeed = 8f;
        legR1RSpeed = 8f;
        legR2RSpeed = 8f;
        lArmRSpeed = 8f;
        rArmRSpeed = 8f;
        bodyRSpeed = 8f;
        legRSpeed = 8f;
        headRSpeed = 8f;
        legL1Rotaion = Quaternion.Euler(-10, 0, -1);
        legL2Rotaion = Quaternion.Euler(10, 0, 0);
        legR1Rotaion = Quaternion.Euler(-10, 0, 1);
        legR2Rotaion = Quaternion.Euler(10, 0, 0);
        lArmRotaion = Quaternion.Euler(-5, 0, -5);
        rArmRotaion = Quaternion.Euler(-5, 0, 5);
        bodyRotaion = Quaternion.Euler(10, 0, 0);
        legRotaion = Quaternion.Euler(0, 0, 0);
        headRotaion = Quaternion.Euler(10, 0, 0);
        StayMotion = true;
    }
    /// <summary>
    /// ダッシュ
    /// </summary>
    protected virtual void DashMotion()
    {
        legL1RSpeed = 28f;
        legL2RSpeed = 28f;
        legR1RSpeed = 28f;
        legR2RSpeed = 28f;
        lArmRSpeed = 28f;
        rArmRSpeed = 28f;
        bodyRSpeed = 28f;
        legRSpeed = 28f;
        headRSpeed = 28f;
        legL1Rotaion = Quaternion.Euler(-20, 0, 0);
        legL2Rotaion = Quaternion.Euler(20, 0, 0);
        legR1Rotaion = Quaternion.Euler(-20, 0, 0);
        legR2Rotaion = Quaternion.Euler(20, 0, 0);
        lArmRotaion = Quaternion.Euler(50, 0, 0);
        rArmRotaion = Quaternion.Euler(50, 0, 0);
        bodyRotaion = Quaternion.Euler(10, 20, 0);
        legRotaion = Quaternion.Euler(0, 0, 0);
        headRotaion = Quaternion.Euler(10, -20, 0);
    }
    /// <summary>
    /// モーション制御
    /// </summary>
    protected virtual void PartsMotion()
    {
        #region 動作制御
        Head.transform.localRotation = Quaternion.Lerp(Head.transform.localRotation, headRotaion, headRSpeed * Time.deltaTime);
        Body.transform.localRotation = Quaternion.Lerp(Body.transform.localRotation, bodyRotaion, bodyRSpeed * Time.deltaTime);
        armL.transform.localRotation = Quaternion.Lerp(armL.transform.localRotation, lArmRotaion, lArmRSpeed * Time.deltaTime);
        armL1.transform.localRotation = Quaternion.Lerp(armL1.transform.localRotation, lArm1Rotaion, lArm1RSpeed * Time.deltaTime);
        armL2.transform.localRotation = Quaternion.Lerp(armL2.transform.localRotation, lArm2Rotaion, lArm2RSpeed * Time.deltaTime);
        armR.transform.localRotation = Quaternion.Lerp(armR.transform.localRotation, rArmRotaion, rArmRSpeed * Time.deltaTime);
        armR1.transform.localRotation = Quaternion.Lerp(armR1.transform.localRotation, rArm1Rotaion, rArm1RSpeed * Time.deltaTime);
        armR2.transform.localRotation = Quaternion.Lerp(armR2.transform.localRotation, rArm2Rotaion, rArm2RSpeed * Time.deltaTime);
        leg.transform.localRotation = Quaternion.Lerp(leg.transform.localRotation, legRotaion, legRSpeed * Time.deltaTime);
        legL1.transform.localRotation = Quaternion.Lerp(legL1.transform.localRotation, legL1Rotaion, legL1RSpeed * Time.deltaTime);
        legL2.transform.localRotation = Quaternion.Lerp(legL2.transform.localRotation, legL2Rotaion, legL2RSpeed * Time.deltaTime);
        legR1.transform.localRotation = Quaternion.Lerp(legR1.transform.localRotation, legR1Rotaion, legR1RSpeed * Time.deltaTime);
        legR2.transform.localRotation = Quaternion.Lerp(legR2.transform.localRotation, legR2Rotaion, legR2RSpeed * Time.deltaTime);
        #endregion
    }
    /// <summary>
    /// 指定したモーションを再生する
    /// </summary>
    /// <param name="type"></param>
    public virtual void MotionTypeChange(MotionType type)
    {
        if (motionType != type)
        {
            motionType = type;
            motionStart = false;
        }
    }
    /// <summary>
    /// プレイヤーを設定する
    /// </summary>
    /// <param name="player"></param>
    //public void SetOwner(Player player)
    //{
    //    owner = player;
    //}
}

