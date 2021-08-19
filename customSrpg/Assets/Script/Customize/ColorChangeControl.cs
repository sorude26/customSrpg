using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Customize
{
    public class ColorChangeControl : MonoBehaviour
    {
        public event Action<Color,int> OnColorChange;
        Color m_color;
        [SerializeField] ColorPanel m_panel;
        [SerializeField] GameObject m_target;
        [SerializeField] ColorData m_colorData;
        ColorPanel[] m_panels;
        int m_number;
        public void StartSet()
        {
            m_panels = new ColorPanel[m_colorData.PatternNum * m_colorData.ColorTypeNum * 2];
            int count = 0;
            for (int y = 0; y < m_colorData.ColorTypeNum; y++)
            {
                for (int i = 23; i >= 0; i--)
                {
                    var panel = Instantiate(m_panel, gameObject.transform);
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
            m_color = GetColor(number);
            OnColorChange?.Invoke(m_color, number);
            SetTargetColor(number);
        }
        public void SetColor(Color color, int number)
        {
            m_color = color;
            OnColorChange?.Invoke(m_color,number);
            SetTargetColor(number);
        }
        public void SetTargetColor(int number)
        {
            m_target.transform.position = m_panels[number].gameObject.transform.position;
            m_number = number;
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
    }
}
