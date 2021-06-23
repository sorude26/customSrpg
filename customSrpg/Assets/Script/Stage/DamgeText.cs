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
    public void SetDamage(int damage)
    {
        gameObject.SetActive(true);
        m_text.text = damage.ToString();
        m_viewTimer = 1f;
    } 
}
