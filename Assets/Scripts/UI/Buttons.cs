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
        Time.timeScale = 1;

    }
    public void Exit() => Application.Quit();
    public void ReloadLevl(int level)
    {
        SceneManager.LoadScene(level);
        Time.timeScale = 1;
    }

    public void AddImprovement(int number_improvment)
    {
        if (number_improvment == 1)
        {
            Global_variables.boost_spawn_speed++;
        }
        if (number_improvment == 2)
        {
            Global_variables.boost_speed++;
        }
    }
    public void PlayAds()
    {
    }

}
