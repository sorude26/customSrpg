using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UIControl
{
    public class CommandBox : CommandButton<CommandBox>
    {
        [Tooltip("コマンド名表示テキスト")]
        [SerializeField] Text[] m_commandText;
        [Tooltip("実行ボタン")]
        [SerializeField] Button m_button;
        /// <summary> クリック時のイベント </summary>
        public override event Action<int> OnCommand;
        public override void StartSet(int id, Action<int> action)
        {

        }
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
        public override void OnClickCommand()
        {
            OnCommand?.Invoke(CommandID);
        }
        public override CommandBox SelectCommand()
        {
            m_button.Select();
            return this;
        }

        public override void OutCommand()
        {

        }
    }
}
