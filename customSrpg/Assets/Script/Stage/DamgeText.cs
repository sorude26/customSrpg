using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamgeText : MonoBehaviour
{
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
    public void Play(int damage,Vector3 pos)
    {
        gameObject.SetActive(true);
        transform.position = pos;
        m_text.text = damage.ToString();
        m_viewTimer = 0.8f;
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
