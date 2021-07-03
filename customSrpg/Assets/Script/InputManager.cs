using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage
{
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
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            if (h != 0 || v != 0)
            {
                OnInputArrow?.Invoke(h, v);
            }
        }
    }
}
