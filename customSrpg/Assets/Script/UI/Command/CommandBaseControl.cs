using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UIControl
{
    public class CommandBaseControl : MonoBehaviour
    {
        public static CommandBaseControl Instance { get; private set; }
        [SerializeField] CommandBase[] m_allCommandBase;
        [SerializeField] float m_changeSpeed = 0.2f;
        bool m_cursorMove;
        public event Action<float, float> OnMove;
        public event Action OnDecide;
        bool m_moveMode;
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            GameScene.InputManager.Instance.OnInputArrow += CommandMove;
            m_moveMode = true;
            GameScene.InputManager.Instance.OnInputDecision += CommandDecide;
            m_allCommandBase[0].StartSet();
            m_allCommandBase[0].SelectCommand();
        }
        public void SetAction(Action<float,float> move,Action decide)
        {
            OnMove = move;
            OnDecide = decide;
        }
        public void CommandDecide()
        {
            OnDecide?.Invoke();
        }
        /// <summary>
        /// カーソルを移動させる
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void CommandMove(float x, float y)
        {
            if (m_cursorMove)
            {
                return;
            }
            StartCoroutine(CommandMoveStart(x, y));
        }
        IEnumerator CommandMoveStart(float x, float y)
        {
            m_cursorMove = true;
            OnMove?.Invoke(x, y);
            yield return new WaitForSeconds(m_changeSpeed);
            m_cursorMove = false;
        }
        public void CommandMoveSet()
        {
            if (!m_moveMode)
            {
                GameScene.InputManager.Instance.OnInputArrow += CommandMove;
                m_moveMode = true;
            }
        }
        public void CommandMoveOff()
        {
            if (m_moveMode)
            {
                GameScene.InputManager.Instance.OnInputArrow -= CommandMove;
                m_moveMode = false;
            }
        }
    }
}