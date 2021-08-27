using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene
{
    /// <summary>
    /// 全入力を管理する
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }
        public event Action<float, float> OnInputArrow;
        public event Action OnInputDecision;
        private void Awake()
        {
            Instance = this;
        }
        void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                OnInputDecision?.Invoke();
                return;
            }
            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                float h = Input.GetAxisRaw("Horizontal");
                float v = Input.GetAxisRaw("Vertical");
                if (new Vector2(h,v).sqrMagnitude > 0.1f)
                {
                   OnInputArrow?.Invoke(h, v);
                }
            }
        }
    }
}
