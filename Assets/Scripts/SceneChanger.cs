using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChanger : MonoBehaviour
{
    private const int mainMenu = 0;
    private const int levelMenu = 1;
    private const int survival = 2;

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void GoToLevel(int index)
    {
        SceneManager.LoadScene("Level" + (index).ToString());
    }

    public void GoToLevelMenu()
    {
        SceneManager.LoadScene(levelMenu);
    }

    public void GoToSurvival()
    {
        SceneManager.LoadScene(survival);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Exit()
    {
        Application.Quit();
    }

}
