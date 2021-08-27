using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene
{
    /// <summary>
    /// カーソルの機能を持つUIコマンド操作の基底クラス
    /// </summary>
    public abstract class CommandControlBase : MonoBehaviour
    {
        /// <summary> 現在選択中のコマンドナンバー </summary>
        protected int m_number;
        /// <summary> 横列の最大値 </summary>
        protected int m_horizonalMax;
        /// <summary> 縦列の最大値 </summary>
        protected int m_verticalMax;
        /// <summary> コマンドの最大値 </summary>
        protected int m_maxNum;
        /// <summary> コマンドの最小値 </summary>
        protected int m_minNum;

        /// <summary> コマンドの初期化処理 </summary>
        public abstract void StartSet();
        /// <summary> コマンドの選択時処理 </summary>
        public abstract void SelectCommand();
        public abstract void DecisionCommand();
        public abstract void OutCommand();
        /// <summary>カーソルの移動後処理 </summary>
        protected virtual void CursorSet(int number)
        {
            SelectCommand();
        }
        /// <summary>カーソルの移動入力処理 </summary>
        public virtual void CursorMove(float x, float y)
        {
            if (y > 0)
            {
                CursorUp();
            }
            else if (y < 0)
            {
                CursorDown();
            }
            else if (x > 0)
            {
                CursorRight();
            }
            else if (x < 0)
            {
                CursorLeft();
            }
        }
        /// <summary>カーソルの上移動処理 </summary>
        public virtual void CursorUp()
        {
            m_number -= m_horizonalMax;
            if (m_number < 0)
            {
                m_number += m_maxNum;
            }
            CursorSet(m_number);
        }
        /// <summary>カーソルの下移動処理 </summary>
        public virtual void CursorDown()
        {
            m_number += m_horizonalMax;
            if (m_number >= m_maxNum)
            {
                m_number -= m_maxNum;
            }
            CursorSet(m_number);
        }
        /// <summary>カーソルの左移動処理 </summary>
        public virtual void CursorLeft()
        {
            m_number--;
            if (m_number < m_minNum)
            {
                m_number += m_maxNum;
            }
            CursorSet(m_number);
        }
        /// <summary>カーソルの右移動処理 </summary>
        public virtual void CursorRight()
        {
            m_number++;
            if (m_number >= m_maxNum)
            {
                m_number = m_minNum;
            }
            CursorSet(m_number);
        }
    }
}
