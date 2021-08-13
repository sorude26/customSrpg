using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Customize
{
    public class ColorChangeControl : MonoBehaviour
    {
        Color m_color;
        [SerializeField] Image m_panel;
        private void Start()
        {
            StartSet();
        }
        public void StartSet()
        {
            int max = 5;
            int r = 5;
            int g = 0;
            int b = 0;
            int min = 0;
            for (int a = 0; a < 5; a++)
            {
                for (int k = 0; k < 6; k++)
                {
                    for (int i = 0; i < 45; i++)
                    {
                        var panel = Instantiate(m_panel, gameObject.transform);
                        panel.color = new Color32((byte)(r * 51), (byte)(g * 51), (byte)(b * 51), 255);
                        if (r == max && g < max && b == min)
                        {
                            g++;
                        }
                        else if (r > min && g == max && b == min)
                        {
                            r--;
                        }
                        else if (g == max && b < max && r == min)
                        {
                            b++;
                        }
                        else if (b == max && g > min && r == min)
                        {
                            g--;
                        }
                        else if (b == max && r < max && g == min)
                        {
                            r++;
                        }
                        else if (b > min && r == max && g == min)
                        {
                            b--;
                        }
                    }
                    min++;
                    r = max;
                    g = min;
                    b = min;
                }
                max--;
                min = 0;
                r = max;
                g = min;
                b = min;
            }            
        }
        public void SetColor(Color color)
        {
            m_color = color;
        }
    }
}
