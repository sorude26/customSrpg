using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    [SerializeField] ButtonMaster m_actionB;
    [SerializeField] ButtonMaster m_moveEndB;
    [SerializeField] ButtonMaster m_attackB;
    [SerializeField] ButtonMaster m_targetB;
    [SerializeField] ButtonMaster m_decisionB;
    ButtonMaster m_targetButton;
    [SerializeField] float m_changeSpeed = 0.2f;
    bool m_cursorMove;
    bool m_move;
    bool m_attack;
    private void Start()
    {
        m_targetB.OnDecision += Attack;
        AllButtonClose();
    }

    public void CommandOpen()
    {
        GameScene.InputManager.Instance.OnInputArrow += CursorMove;
        GameScene.InputManager.Instance.OnInputDecision += Decision;
        AllButtonClose();
        m_actionB.Open();
        m_targetButton = m_actionB;
    }
    public void CommandClose()
    {
        m_move = false;
        m_attack = false;
        GameScene.InputManager.Instance.OnInputArrow -= CursorMove;
        GameScene.InputManager.Instance.OnInputDecision -= Decision;
        AllButtonClose();
    }
    public void CommandMoveEnd()
    {
        GameScene.InputManager.Instance.OnInputDecision -= StageManager.Instance.Cursor.Decision;
        OnClickButton(1);
    }

    /// <summary>
    /// 決定ボタンを押したとき呼ぶ
    /// </summary>
    public void OnClickDecision()
    {
        if (m_attack)
        {
            BattleManager.Instance.AttackStart();
            CommandClose();
        }
        else
        {
            EventManager.StageGuideViewEnd();
            StageManager.Instance.TurnUnit.GetComponent<PlayerUnit>().ActionEnd();
            CommandClose();
        }
    }
    /// <summary>
    /// キャンセルボタンを押した時に呼ぶ
    /// </summary>
    public void OnClickCancel()
    {
        
    }
    void Decision()
    {
        m_targetButton?.Decision();
    }
    void Attack()
    {
        AllButtonClose();
        m_decisionB.Open();
        m_targetButton = m_decisionB;
        m_attack = true;
    }
    /// <summary>
    /// カーソルを移動させる
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void CursorMove(float x,float y)
    {
        if (m_cursorMove)
        {
            return;
        }
        StartCoroutine(CursorMoveStart(x,y));
    }
    IEnumerator CursorMoveStart(float x,float y)
    {
        m_cursorMove = true;
        if (y > 0)
        {
            m_targetButton?.CursorUp();
        }
        else if (y < 0)
        {
            m_targetButton?.CursorDown();
        }
        else if (x > 0)
        {
            m_targetButton?.CursorRight();
        }
        else if (x < 0)
        {
            m_targetButton?.CursorLeft();
        }
        yield return new WaitForSeconds(m_changeSpeed);
        m_cursorMove = false;
    }
    public void OnClickButton(int type)
    {
        AllButtonClose();
        switch (type)
        {
            case 0://移動選択
                StageManager.Instance.MoveSearch();
                m_actionB.Close();
                m_targetButton = null;
                m_move = true;
                GameScene.InputManager.Instance.OnInputDecision += StageManager.Instance.Cursor.Decision;
                break;
            case 1://移動終了
                m_targetButton = m_moveEndB;
                break;
            case 2://攻撃選択
                m_targetButton = m_attackB;
                break;
            case 3://武装選択
                m_targetButton = m_targetB;
                break;
            case 4://移動後キャンセル選択
                StageManager.Instance.TurnUnit.ReturnMove();
                StageManager.Instance.Cursor.Warp(StageManager.Instance.TurnUnit.CurrentPosX, StageManager.Instance.TurnUnit.CurrentPosZ);
                EventManager.StageGuideViewEnd();
                m_targetButton = m_actionB;
                m_move = false;
                break;
            case 5://攻撃選択後キャンセル選択
                if (!m_move)
                {
                    StageManager.Instance.TurnUnit.ReturnMove();
                    StageManager.Instance.Cursor.Warp(StageManager.Instance.TurnUnit.CurrentPosX, StageManager.Instance.TurnUnit.CurrentPosZ);
                    EventManager.StageGuideViewEnd();
                    m_targetButton = m_actionB;
                }
                else//移動後攻撃択後キャンセル選択
                {
                    EventManager.AttackSearchEnd();
                    m_targetButton = m_moveEndB;
                }
                break;
            case 6:
                EventManager.AttackSearchEnd();
                m_targetButton = m_moveEndB;
                break;
            case 7://待機選択
                m_targetButton = m_decisionB;
                break;
            default:
                m_targetButton = null;
                break;
        }
        m_targetButton?.Open();
    }
    void AllButtonClose()
    {
        ButtonMaster[] buttons = { m_actionB, m_attackB, m_moveEndB, m_targetB, m_decisionB };
        foreach (var button in buttons)
        {
            button.Close();
        }
    }
}
