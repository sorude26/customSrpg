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

    protected MotionType motionType = MotionType.Wait;
    private Animator m_anime;
    private void Start()
    {
        m_anime = GetComponent<Animator>();
    }

    public virtual void Wait()
    {
        m_anime.Play("HumanWait");
    }
    /// <summary>
    /// 右手武器で攻撃する
    /// </summary>
    public virtual void TargetShotRArm()
    {
        m_anime.Play("HumanAttackRArm");
    }
    public virtual void TargetShotLArm()
    {
        m_anime.Play("HumanAttackLArm");
    }
    /// <summary>
    /// 歩行
    /// </summary>
    public virtual void Walk()
    {
        m_anime.Play("HumanWalk");
    }
    public void GuardRArm()
    {
        m_anime.Play("HumanGuardRArmWait");
    }

    public void Damage()
    {
        m_anime.Play("HumanWaitDamage");
    }
    public void HeavyDamage()
    {
        m_anime.Play("HumanWaitHeavyDamage");
    }
    /// <summary>
    /// 指定したモーションを再生する
    /// </summary>
    /// <param name="type"></param>
    public virtual void MotionTypeChange(MotionType type)
    {

    }

}

