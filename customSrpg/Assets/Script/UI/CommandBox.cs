using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandBox : MonoBehaviour
{
    [Tooltip("コマンド名表示テキスト")]
    [SerializeField] Text[] m_commandText;
    [Tooltip("実行ボタン")]
    [SerializeField] Button m_button;
    /// <summary> クリック時のイベント </summary>
    public event Action OnClickEvent;
    /// <summary>
    /// コマンド名設定
    /// </summary>
    /// <param name="command"></param>
    public void SetText(string command)
    {
        for (int i = 0; i < m_commandText.Length; i++)
        {
            m_commandText[i].text = command;
        }
    }
    /// <summary>
    /// 選択時の処理
    /// </summary>
    public void OnClickCommand()
    {
        OnClickEvent?.Invoke();
    }
    public void SelectCommand()
    {
        m_button.Select();
    }
}
