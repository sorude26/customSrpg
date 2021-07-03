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
    [SerializeField] ButtonMaster m_targetButton;
    [SerializeField] float m_changeSpeed = 0.2f;
    public event Action OnDecision;
    public event Action OnCancel;
    bool m_move;
    private void Start()
    {
        Stage.InputManager.Instance.OnInputArrow += CursorMove;
        m_targetButton = m_actionB;
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
    public void CursorMove(float x,float y)
    {
        if (m_move)
        {
            return;
        }
        StartCoroutine(CursorMoveStart(y));
    }
    IEnumerator CursorMoveStart(float y)
    {
        m_move = true;
        if (y > 0)
        {
            CursorUp();
        }
        else if (y < 0)
        {
            CursorDown();
        }
        yield return new WaitForSeconds(m_changeSpeed);
        m_move = false;
    }
    public void CursorUp()
    {
        m_targetButton.CursorUp();
    }
    public void CursorDown()
    {
        m_targetButton.CursorDown();
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
                m_targetButton = m_actionB;
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
                m_targetButton = m_actionB;
                break;
            case 5://攻撃選択後キャンセル選択
                EventManager.AttackSearchEnd();
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
