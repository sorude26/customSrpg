using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIControl
{
    public abstract class CommandButton : MonoBehaviour
    {
        /// <summary> コマンドのID </summary>
        public int CommandID { get; protected set; }
        /// <summary> コマンドのイベント </summary>
        public virtual event Action<int> OnCommand;
        /// <summary> コマンドの初期化処理 </summary>
        public abstract void StartSet(int id, Action<int> action);
        /// <summary> コマンドの選択時処理 </summary>
        public abstract void SelectCommand();
        /// <summary> コマンドの選択解除時処理 </summary>
        public abstract void OutCommand();
        /// <summary> コマンドの決定時 </summary>
        public virtual void OnClickCommand()
        {
            OnCommand?.Invoke(CommandID);
        }
    }
}
