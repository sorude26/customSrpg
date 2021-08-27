using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIControl
{
    public abstract class CommandAction<T> : MonoBehaviour
    {
        [SerializeField] protected string[] m_commandNames;
        [SerializeField] protected CommandButton<T> m_commandPrefab;
        public string[] CommandNams { get => m_commandNames; }
        public CommandButton<T> CommandPrefab { get => m_commandPrefab; }
        public int CommandNum { get => m_commandNames.Length; }
    }
}
