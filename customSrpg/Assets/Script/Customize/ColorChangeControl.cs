using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameScene;

namespace Customize
{
    public class ColorChangeControl : MonoBehaviour
    {
        public event Action<Color,int> OnColorChange;
        Color m_color;
        [SerializeField] ColorPanel m_panel;
        [SerializeField] ColorData m_colorData;
        [SerializeField] GameObject m_panelBase;
        ColorPanel[] m_panels;
        int m_number = 0;
        public void StartSet()
        {
            m_panels = new ColorPanel[m_colorData.PatternNum * m_colorData.ColorTypeNum * 2];
            int count = 0;
            for (int y = 0; y < m_colorData.ColorTypeNum; y++)
            {
                for (int i = 23; i >= 0; i--)
                {
                    var panel = Instantiate(m_panel, m_panelBase.transform);
                    panel.SetColor(m_colorData.GetColor(m_colorData.PatternNum * y * 2 + i), count);
                    panel.OnClickColor += SetColor;
                    m_panels[count] = panel;
                    count++;
                }
            }
        }
        public Color GetColor(int number)
        {
            return m_panels[number].GetColor();
        }
        public void SetColor(int number)
        {
            foreach (var panel in m_panels)
            {
                panel.OutSelect();
            }
            m_color = GetColor(number);
            OnColorChange?.Invoke(m_color, number);
            m_number = number;
            m_panels[number].OnSelect();
        }
        public void CursorMove(float x,float y)
        {
            if (y > 0)
            {
                CursorUp();
            }
            else if (y < 0)
            {
                CursorDown();
            }
            else if (x > 0)
            {
                CursorRight();
            }
            else if (x < 0)
            {
                CursorLeft();
            }
        }
        public void CursorUp()
        {
            m_number -= m_colorData.PatternNum * 2;
            if (m_number < 0)
            {
                m_number += m_panels.Length;
            }
            SetColor(m_number);
        }
        public void CursorDown()
        {
            m_number += m_colorData.PatternNum * 2;
            if (m_number >= m_panels.Length)
            {
                m_number -= m_panels.Length;
            }
            SetColor(m_number);
        }
        public void CursorLeft()
        {
            m_number--;
            if (m_number < 0)
            {
                m_number += m_panels.Length;
            }
            SetColor(m_number);
        }
        public void CursorRight()
        {
            m_number++;
            if (m_number >= m_panels.Length)
            {
                m_number = 0;
            }
            SetColor(m_number);
        }
        public void OpenPanel()
        {
            m_panelBase.transform.localScale = Vector3.one;
        }
        public void ClosePanel()
        {
            m_panelBase.transform.localScale = Vector3.zero;
        }
    }
}
