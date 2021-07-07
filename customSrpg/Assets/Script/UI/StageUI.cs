using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ButtonType 
{
    Move,
    Attack,
    Rest,
    Back,
}

public class StageUI : MonoBehaviour
{
    [SerializeField] GameObject m_decisionMassage;
    [SerializeField] ButtonMaster m_actionB;
    [SerializeField] ButtonMaster m_moveEndB;
    [SerializeField] ButtonMaster m_attackB;
    [SerializeField] ButtonMaster m_targetB;
    ButtonMaster m_targetButton;
    [SerializeField] float m_changeSpeed = 0.2f;
    public event Action OnDecision;
    public event Action OnCancel;
    bool m_move;
    private void Start()
    {
        ButtonMaster[] buttons = { m_actionB, m_attackB, m_moveEndB, m_targetB };
        foreach (var button in buttons)
        {
            button.Close();
        }
    }

    public void CommandOpen()
    {
        Stage.InputManager.Instance.OnInputArrow += CursorMove;
        Stage.InputManager.Instance.OnInputDecision += Decision;
        ButtonMaster[] buttons = { m_attackB, m_moveEndB, m_targetB };
        foreach (var button in buttons)
        {
            button.Close();
        }
        m_actionB.Open();
        m_targetButton = m_actionB;
    }
    public void CommandClose()
    {
        Stage.InputManager.Instance.OnInputArrow -= CursorMove;
        Stage.InputManager.Instance.OnInputDecision -= Decision;
        ButtonMaster[] buttons = { m_actionB, m_attackB, m_moveEndB, m_targetB };
        foreach (var button in buttons)
        {
            button.Close();
        }
    }
    public void CommandMoveEnd()
    {
        OnClickButton(1);
    }
    /// <summary>
    /// メッセージを開く
    /// </summary>
    public void OpenMassage()
    {
        m_decisionMassage.SetActive(true);
    }
    /// <summary>
    /// 決定ボタンを押したとき呼ぶ
    /// </summary>
    public void OnClickDecision()
    {
        m_decisionMassage.SetActive(false);
        OnDecision?.Invoke();
    }
    /// <summary>
    /// キャンセルボタンを押した時に呼ぶ
    /// </summary>
    public void OnClickCancel()
    {
        m_decisionMassage.SetActive(false);
        OnCancel?.Invoke();
    }
    void Decision()
    {
        m_targetButton?.Decision();
    }
    /// <summary>
    /// カーソルを移動させる
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void CursorMove(float x,float y)
    {
        if (m_move)
        {
            return;
        }
        StartCoroutine(CursorMoveStart(x,y));
    }
    IEnumerator CursorMoveStart(float x,float y)
    {
        m_move = true;
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
        m_move = false;
    }
    public void OnClickButton(int type)
    {
        ButtonMaster[] buttons = { m_actionB, m_attackB, m_moveEndB, m_targetB };
        foreach (var button in buttons)
        {
            button.Close();
        }
        switch (type)
        {
            case 0://移動選択
                StageManager.Instance.MoveSearch();
                m_actionB.Close();
                m_targetButton = null;
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
                StageManager.Instance.CursorWap(StageManager.Instance.TurnUnit.CurrentPosX, StageManager.Instance.TurnUnit.CurrentPosZ);
                EventManager.StageGuideViewEnd();
                m_targetButton = m_actionB;
                break;
            case 5://攻撃選択後キャンセル選択
                StageManager.Instance.TurnUnit.ReturnMove();
                StageManager.Instance.CursorWap(StageManager.Instance.TurnUnit.CurrentPosX, StageManager.Instance.TurnUnit.CurrentPosZ);
                EventManager.StageGuideViewEnd();
                m_targetButton = m_actionB;
                break;
            case 6://武装選択後キャンセル選択
                m_targetButton = m_attackB;
                break;
            case 7://待機選択
                m_targetButton = null;
                OpenMassage();
                break;
            default:
                m_targetButton = null;
                break;
        }
        m_targetButton?.Open();
    }
}
