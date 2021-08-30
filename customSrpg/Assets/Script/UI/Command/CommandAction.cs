using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIControl
{
    public abstract class CommandAction : MonoBehaviour
    {
        [SerializeField] protected string[] m_commandNames;
        public string[] CommandNams { get => m_commandNames; }
        public abstract int CommandNum { get; }
        public abstract void SetData(CommandBase[] commands);
    }
}
