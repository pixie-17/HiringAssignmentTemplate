using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject pauseMenu, finishedMenu, failedMenu;

    public void Start()
    {
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OpenFinished()
    {
        finishedMenu.SetActive(true);
    }

    public void OpenFailed()
    {
        failedMenu.SetActive(true);
    }
}