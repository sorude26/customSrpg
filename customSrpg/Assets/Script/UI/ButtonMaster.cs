using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UIのボタンを操作する
/// </summary>
public class ButtonMaster : MonoBehaviour
{
    [Tooltip("選択位置の表示")]
    [SerializeField] protected RectTransform m_targetMark;
    [Tooltip("選択肢")]
    [SerializeField] protected Button[] m_buttons;
    /// <summary> 選択位置番号 </summary>
    protected int m_buttonNum = 0;
    
    /// <summary>
    /// 選択肢を開く
    /// </summary>
    public virtual void Open()
    {
        gameObject.SetActive(true);
        m_buttonNum = 0;
        StartCoroutine(CursorSet());
    }
    /// <summary>
    /// 選択肢を閉じる
    /// </summary>
    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
    /// <summary>
    /// カーネル位置を上に移動する
    /// </summary>
    public virtual void CursorUp()
    {
        if (m_buttonNum > 0)
        {
            m_buttonNum--;
        }
        CursorMove();
    }
    /// <summary>
    /// カーネル位置を下に移動する
    /// </summary>
    public virtual void CursorDown()
    {
        if (m_buttonNum < m_buttons.Length - 1)
        {
            m_buttonNum++;
        }
        CursorMove();
    }
    /// <summary>
    /// カーソルを左へ移動する
    /// </summary>
    public virtual void CursorLeft() { }
    /// <summary>
    /// カーソルを右へ移動する
    /// </summary>
    public virtual void CursorRight() { }
    /// <summary>
    /// カーソルを移動させる
    /// </summary>
    protected virtual void CursorMove()
    {
        m_targetMark.transform.localPosition = m_buttons[m_buttonNum].transform.localPosition;
    }
    /// <summary>
    /// ボタンを押す
    /// </summary>
    public virtual void Decision()
    {
        m_buttons[m_buttonNum].onClick.Invoke();
    }
    protected IEnumerator CursorSet()
    {
        yield return new WaitForEndOfFrame();
        CursorMove();
    }
}
