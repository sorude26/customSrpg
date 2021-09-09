using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    void Start()
    {
        FadeController.Instance.StartFadeIn();
    }
    public void NextScene()
    {
        FadeController.Instance.StartFadeOut(SceneChange);
    }
    void SceneChange()
    {
        SceneManager.LoadScene("CustomizeScene");
    }
}
