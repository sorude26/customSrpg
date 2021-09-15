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
        public event Action<float, float> OnInputArrowLate;
        public event Action OnInputDecision;
        [SerializeField] float m_lateTime = 0.2f;
        bool lateInput = default;
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
                if (new Vector2(h, v).sqrMagnitude > 0.1f)
                {
                    OnInputArrow?.Invoke(h, v);
                    if (!lateInput)
                    {
                        lateInput = true;
                        StartCoroutine(LateInputArrow(h, v));
                    }
                }
            }
        }

        IEnumerator LateInputArrow(float h, float v)
        {
            OnInputArrowLate?.Invoke(h, v);
            yield return new WaitForSeconds(m_lateTime);
            lateInput = false;
        }
    }
}
