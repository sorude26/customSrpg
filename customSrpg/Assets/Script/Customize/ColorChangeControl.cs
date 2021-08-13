using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Customize
{
    public class ColorChangeControl : MonoBehaviour
    {
        public event Action<Color> OnColorChange;
        Color m_color;
        [SerializeField] ColorPanel m_panel;
        byte[] m_colorPattern = { 20, 40, 63, 86, 109, 132, 155, 188, 211, 234, 247, 255 };
        Vector3[] m_rgbPattern = {
            new Vector3(1, 0, 0), new Vector3(1, 0.3f, 0), new Vector3(1, 0.5f, 0), new Vector3(1, 0.8f, 0), new Vector3(1, 1, 0), new Vector3(0.5f, 1, 0),
            new Vector3(0, 1, 0), new Vector3(0, 1, 0.5f), new Vector3(0, 1, 1), new Vector3(0, 0.8f, 1), new Vector3(0, 0.5f, 1), new Vector3(0, 0.2f, 1),
            new Vector3(0, 0, 1), new Vector3(0.5f, 0, 1), new Vector3(1, 0, 1), new Vector3(1, 0, 0.8f), new Vector3(1, 0, 0.5f), new Vector3(1, 1, 1) };
        private void Start()
        {
            StartSet();
        }
        public void StartSet()
        {
            int count = 0;
            for (int y = 0; y < m_rgbPattern.Length; y++)
            {
                for (int i = 11; i >= 0; i--)
                {
                    var panel = Instantiate(m_panel, gameObject.transform);
                    panel.SetColor(new Color32((byte)(m_colorPattern[i] * m_rgbPattern[y].x),
                        (byte)(m_colorPattern[i] * m_rgbPattern[y].y),
                        (byte)(m_colorPattern[i] * m_rgbPattern[y].z), 255),count);
                    panel.OnClickColor += SetColor;
                    count++;
                }
            }
        }
        public void SetColor(Color color, int number)
        {
            m_color = color;
            OnColorChange?.Invoke(m_color);
        }
    }
}
