using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private const int mainMenu = 0;
    private const int levelMenu = 1;

    public void goToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void goToLevel(int index)
    {
        SceneManager.LoadScene("Level" + (index).ToString());
    }

    public void goToLevelMenu()
    {
        SceneManager.LoadScene(levelMenu);
    }

    public void resetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void exit()
    {
        Application.Quit();
    }

}
