using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 数値を表示する
/// </summary>
public class DamgeText : MonoBehaviour
{
    [Tooltip("表示時間")]
    [SerializeField] float m_viewTime = 0.8f;
    [Tooltip("通常のフォントサイズ")]
    [SerializeField] int m_norumaruSize = 100;
    Text m_text;
    float m_viewTimer = 0;
    private void Awake()
    {
        m_text = GetComponentInChildren<Text>();
        gameObject.SetActive(false);
    }
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        m_viewTimer -= Time.deltaTime;
        if (m_viewTimer > 0)
        {
            return;
        }
        gameObject.SetActive(false);
    }
    /// <summary>
    /// 指定した場所で数値を表示する
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="pos"></param>
    public void Play(int damage,Vector3 pos)
    {
        gameObject.SetActive(true);
        transform.position = pos;
        m_text.text = damage.ToString();
        m_text.fontSize = m_norumaruSize;
        m_viewTimer = m_viewTime;
    }
    /// <summary>
    /// 指定した場所、サイズ、時間で数値を表示する
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="pos"></param>
    /// <param name="size"></param>
    /// <param name="time"></param>
    public void Play(int damage, Vector3 pos, int size, float time)
    {
        gameObject.SetActive(true);
        transform.position = pos;
        m_text.text = damage.ToString();
        m_text.fontSize = size;
        m_viewTimer = time;
    }
    /// <summary>
    /// 再生中はTrueを返す
    /// </summary>
    /// <returns></returns>
    public bool IsActive()
    {
        return gameObject.activeInHierarchy;
    }
}
