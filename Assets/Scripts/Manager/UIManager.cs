using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseMenu;

    [SerializeField]
    private GameObject _finishedMenu;
    
    [SerializeField]
    private GameObject _failedMenu;

    [SerializeField]
    private TMP_Text _scoreText;

    public static UIManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1f;
    }

    private void Pause()
    {
        if (!GameManager.Instance.LevelFinished && _pauseMenu != null)
        {
            _pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void Resume()
    {
        if (!GameManager.Instance.LevelFinished && _pauseMenu != null)
        {
            _pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void OpenFinished()
    {
        if (_finishedMenu != null)
        {
            _finishedMenu.SetActive(true);
        }
    }

    public void OpenFailed()
    {
        if (_failedMenu != null)
        {
            _failedMenu.SetActive(true);
        }
    }

    private void UpdateScoreText(int score)
    {
        if (_scoreText != null)
        {
            _scoreText.text += " " + score;
        }
    }

    private void OnEnable()
    {
        GameManager.OnStop += UpdateScoreText;
    }

    private void OnDisable()
    {
        GameManager.OnStop -= UpdateScoreText;
    }


}
