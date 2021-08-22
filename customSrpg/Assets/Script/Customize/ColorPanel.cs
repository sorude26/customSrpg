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
        [SerializeField] GameObject m_targetMark;
        public event Action<int> OnClickColor;
        int m_number;
        Color m_color;
        public void SetColor(Color32 color,int number)
        {
            m_colorImage.color = color;
            m_number = number;
            m_color = color;
            m_targetMark.SetActive(false);
        }
        public Color GetColor()
        {
            return m_color;
        }
        public void OnClick()
        {
            OnClickColor?.Invoke(m_number);
            m_targetMark.SetActive(true);
        }
        public void OnSelect()
        {
            m_targetMark.SetActive(true);
        }
        public void OutSelect()
        {
            m_targetMark.SetActive(false);
        }
        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            OnClick();
        }
    }
}
