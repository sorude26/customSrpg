using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageMassage : MonoBehaviour
{
    [Tooltip("メッセージ")]
    [SerializeField] string[] m_massages;
    [Tooltip("各メッセージに対応する色、X:R,Y:G,Z:B")]
    [SerializeField] Vector3[] m_colors;
    [SerializeField] Text m_text;
    [SerializeField] Image m_image;
    [SerializeField] float m_viewTime = 1f;
    [SerializeField] float m_viewSpeed = 1f;
    float m_viewTimer = 0;
    float m_clearScale = 0;
    public bool ViewNow { get; private set; }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    /// <summary>
    /// ターンを表示
    /// </summary>
    /// <param name="massageNum"></param>
    /// <returns></returns>
    public IEnumerator View(uint massageNum)
    {
        gameObject.SetActive(true);
        ViewNow = true;
        m_viewTimer = 0;
        m_clearScale = 0;
        m_text.text = m_massages[massageNum];
        m_text.fontSize = 150;
        bool start = true;
        bool end = false;
        while (!end)
        {
            if (start)
            {
                m_clearScale += m_viewSpeed * Time.deltaTime;
                if (m_clearScale >= 1f)
                {
                    m_clearScale = 1f;
                    start = false;
                }
            }
            else
            {
                if (m_viewTimer < m_viewTime)
                {
                    m_viewTimer += Time.deltaTime;
                }
                else
                {
                    m_clearScale -= m_viewSpeed * Time.deltaTime;
                    if (m_clearScale <= 0)
                    {
                        m_clearScale = 0;
                        end = true;
                    }
                }
            }
            m_text.color = new Color(1, 1, 1, m_clearScale);
            m_image.color = new Color(m_colors[massageNum].x, m_colors[massageNum].y, m_colors[massageNum].z, m_clearScale);
            yield return new WaitForEndOfFrame();
        }
        ViewNow = false;
        gameObject.SetActive(false);
    }
    /// <summary>
    /// 勝敗を表示
    /// </summary>
    /// <param name="massageNum"></param>
    /// <returns></returns>
    public IEnumerator LastMessageView(uint massageNum)
    {
        gameObject.SetActive(true);
        ViewNow = true;
        m_viewTimer = 0;
        m_clearScale = 0;
        m_text.text = m_massages[massageNum];
        m_text.fontSize = 150;
        bool start = true;
        while (start)
        {
            m_clearScale += m_viewSpeed * Time.deltaTime;
            if (m_clearScale >= 1f)
            {
                m_clearScale = 1f;
                start = false;
            }
            m_text.color = new Color(1, 1, 1, m_clearScale);
            m_image.color = new Color(m_colors[massageNum].x, m_colors[massageNum].y, m_colors[massageNum].z, m_clearScale);
            yield return new WaitForEndOfFrame();
        }
        ViewNow = false;
    }
    public IEnumerator ShortMessageView(string massage,float inSpeed,float viewTime,int size)
    {
        gameObject.SetActive(true);
        ViewNow = true;
        m_viewTimer = 0;
        m_clearScale = 0;
        m_text.text = massage;
        m_text.fontSize = size;
        bool start = true;
        bool end = false;
        while (!end)
        {
            if (start)
            {
                m_clearScale += inSpeed * m_viewSpeed * Time.deltaTime;
                if (m_clearScale >= 1f)
                {
                    m_clearScale = 1f;
                    start = false;
                }
            }
            else
            {
                if (m_viewTimer < viewTime)
                {
                    m_viewTimer += Time.deltaTime;
                }
                else
                {
                    m_clearScale -= inSpeed * m_viewSpeed * Time.deltaTime;
                    if (m_clearScale <= 0)
                    {
                        m_clearScale = 0;
                        end = true;
                    }
                }
            }
            m_text.color = new Color(1, 1, 1, m_clearScale);
            m_image.color = new Color(0, 0, 0, m_clearScale);
            yield return new WaitForEndOfFrame();
        }
        ViewNow = false;
        gameObject.SetActive(false);
    }
}
