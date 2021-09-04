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
        int m_colorNum;
        Color m_color;
        public void SetColor(int color,int number)
        {
            m_colorNum = color;
            m_colorImage.color = GameManager.Instanse.GetColor(color);
            m_number = number;
            m_color = GameManager.Instanse.GetColor(color);
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
