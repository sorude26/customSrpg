using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] GameObject m_nextButton;
    void Start()
    {
        FadeController.Instance.StartFadeIn();
        m_nextButton.SetActive(false);
    }
    public void OnClickStart()
    {
        m_nextButton.SetActive(true);
    }
    public void NextScene()
    {
        FadeController.Instance.StartFadeOut(SceneChange);
    }
    void SceneChange()
    {
        SceneManager.LoadScene("CustomizeScene");
    }
    public void LoadData()
    {
        FadeController.Instance.StartFadeOut(SceneChange);
        GameManager.Instanse.Load();
    }
}
