using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectStageManager : MonoBehaviour
{

    /// <summary>
    /// ステージ開始
    /// </summary>
    public void SortiePlayer()
    {
        FadeController.Instance.StartFadeOut(ChangeScene);
    }
    void ChangeScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
