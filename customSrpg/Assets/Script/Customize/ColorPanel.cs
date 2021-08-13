using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Customize
{
    public class ColorPanel : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] Image m_colorImage;
        public event Action<Color, int> OnClickColor;
        int m_number;
        Color m_color;
        public void SetColor(Color32 color,int number)
        {
            m_colorImage.color = color;
            m_number = number;
            m_color = color;
        }
        public void OnClick()
        {
            OnClickColor?.Invoke(m_color, m_number);
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            OnClick();
        }
    }
}
