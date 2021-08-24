using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene
{
    /// <summary>
    /// コマンド操作を管理するクラス
    /// </summary>
    public class CommandManager : MonoBehaviour
    {
        [SerializeField] CommandControlBase[] m_sceneAllCommand;
        int m_number = 0;
        public void StartSet()
        {
            foreach (var command in m_sceneAllCommand)
            {
                command.StartSet();
            }
            SetCursor();
        }
        public void ChangeCommand(int number)
        {
            OutCursor();
            m_number = number;
            SetCursor();
        }
        void SetCursor()
        {
            InputManager.Instance.OnInputArrow += m_sceneAllCommand[m_number].CursorMove;
            m_sceneAllCommand[m_number].SelectCommand();
        }
        void OutCursor()
        {
            InputManager.Instance.OnInputArrow -= m_sceneAllCommand[m_number].CursorMove;
            m_sceneAllCommand[m_number].OutCommand();
        }
    }
}