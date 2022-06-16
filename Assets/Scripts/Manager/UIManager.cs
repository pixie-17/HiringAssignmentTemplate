using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseMenu;

    [SerializeField]
    private GameObject _finishedMenu;
    
    [SerializeField]
    private GameObject _failedMenu;
    
    public static UIManager Instance { get; set; }

    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OpenFinished()
    {
        _finishedMenu.SetActive(true);
    }

    public void OpenFailed()
    {
        _failedMenu.SetActive(true);
    }
}
