using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    [SerializeField] float m_fadeSpeed = 1f;
    [SerializeField] Image m_fadePanel = default;
    Color m_fadePanelColor;
    private void Awake()
    {
        m_fadePanelColor = m_fadePanel.color;
        m_fadePanel.gameObject.SetActive(false);
    }
    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }
    public void StartFadeIn(Action fadeInAction)
    {
        StartCoroutine(FadeIn(fadeInAction));
    }
    public void StartFadeOut(Action fadeOutAction)
    {
        StartCoroutine(FadeOut(fadeOutAction));
    }
    public void StartFadeOutIn(Action fadeInAction)
    {
        StartCoroutine(FadeOutIn(fadeInAction));
    }
    public void StartFadeOutIn(Action fadeOutAction, Action fadeInAction)
    {
        StartCoroutine(FadeOutIn(fadeOutAction, fadeInAction));
    }
    IEnumerator FadeIn(Action action)
    {
        yield return FadeIn();
        action.Invoke();
    }
    IEnumerator FadeOut(Action action)
    {
        yield return FadeOut();
        action.Invoke();
    }
    IEnumerator FadeOutIn(Action action)
    {
        yield return FadeOut();
        yield return FadeIn();
        action.Invoke();
    }
    IEnumerator FadeOutIn(Action fadeOutAction,Action fadeInAction)
    {
        yield return FadeOut();
        fadeOutAction.Invoke();
        yield return FadeIn();
        fadeInAction.Invoke();
    }
    IEnumerator FadeIn()
    {
        m_fadePanel.gameObject.SetActive(true);
        float a = 1;
        while (a > 0)
        {
            a -= m_fadeSpeed * Time.deltaTime;
            if (a <= 0)
            {
                a = 0;
            }
            m_fadePanel.color = m_fadePanelColor * new Color(1, 1, 1, a);
            yield return new WaitForEndOfFrame();
        }
        m_fadePanel.gameObject.SetActive(false);
    }
    IEnumerator FadeOut()
    {
        m_fadePanel.gameObject.SetActive(true);
        float a = 0;
        while (a < 1f)
        {
            a += m_fadeSpeed * Time.deltaTime;
            if (a >= 1f)
            {
                a = 1f;
            }
            m_fadePanel.color = m_fadePanelColor * new Color(1, 1, 1, a);
            yield return new WaitForEndOfFrame();
        }
    }
}
