using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameScene
{
    /// <summary>
    /// 全てのコマンドが持つ機能
    /// </summary>
    interface ICommand
    {
        //public abstract event Action OnCommand;
        /// <summary> コマンドの初期化処理 </summary>
        void StartSet();
        /// <summary> コマンドの選択時処理 </summary>
        void SelectCommand();
    }
}