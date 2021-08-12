using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Customize
{
    public class ColorChangeControl : MonoBehaviour
    {
        Color m_color;
        void Start()
        {

        }
        public void SetColor(Color color)
        {
            m_color = color;
        }
    }
}
