using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    private void Start()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("4015611",false);
        }
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
        if (number_improvment == 1 && Global_variables.boost_spawn_speed<5)
        {
            Global_variables.boost_spawn_speed++;
        }
        if (number_improvment == 2 && Global_variables.boost_speed < 5)
        {
            Global_variables.boost_speed++;
        }
    }
    public void PlayAds()
    {
        if (Advertisement.IsReady())
        {
            Debug.Log("show");
            Advertisement.Show("video");
            Global_variables.money += 100;
        }
    }

    public void GameMenu(GameObject obj)
    {
        obj.SetActive(true);
        Time.timeScale = 0;

    }
    public void GameMenu_back(GameObject obj)
    {
        obj.SetActive(false);
        Time.timeScale = 1;
    }


}
