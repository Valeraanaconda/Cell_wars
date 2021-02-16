using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    private void Awake()
    {
    }
    public void GoToLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
    public void Exit() => Application.Quit();
    public void ReloadLevl(int level)
    {
        SceneManager.LoadScene(level);
        Time.timeScale = 1;

    }

    public void PlayAds()
    {
    }

}
