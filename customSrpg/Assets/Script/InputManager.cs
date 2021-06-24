using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage
{
    public class InputManager : MonoBehaviour
    {
        public static Stage.InputManager Instance { get; private set; }
        private void Awake()
        {
            Instance = this;
        }
        void Update()
        {

        }
    }
}
