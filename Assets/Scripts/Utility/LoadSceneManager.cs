using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    public void TitleScene()
    {
        SoundManager soundManager = SoundManager.GetInstance();
        soundManager.PlayBgm("MainBgm", true);
        SceneManager.LoadScene(0);
    }

    public void GameplayScene()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

}
