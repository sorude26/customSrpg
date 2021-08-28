using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIControl
{
    public abstract class CommandAction : MonoBehaviour
    {
        [SerializeField] protected string[] m_commandNames;
        [SerializeField] protected CommandBox m_commandPrefab;
        public string[] CommandNams { get => m_commandNames; }
        public CommandBox CommandPrefab { get => m_commandPrefab; }
        public abstract int CommandNum { get; }
        public abstract void SetData(CommandBox[] commands);
    }
}
