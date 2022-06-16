using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [field: SerializeField]
    public bool Survival { get; set; }
    public bool LevelFinished { get; set; }
    public static GameManager Instance = null;
    private int _score = 0;

    public delegate void LevelStopped(int score);
    public static event LevelStopped OnStop;

    private void Awake()
    {
        Instance = this;
        LevelFinished = false;
    }

    private void FinishLevel()
    {
        LevelFinished = true;
        UIManager.Instance.OpenFinished();
    }

    private void FailLevel()
    {
        UIManager.Instance.OpenFailed();
        OnStop(_score);
        Time.timeScale = 0f;
    }

    private void UpdateScore()
    {
        _score++;
    }

    private void OnEnable()
    {
        PlayerManager.OnRecompute += UpdateScore;
        PlayerManager.OnDeath += FailLevel;
        PlayerMovement.OnFinish += FinishLevel;
    }
    private void OnDisable()
    {
        PlayerManager.OnRecompute -= UpdateScore;
        PlayerManager.OnDeath -= FailLevel;
        PlayerMovement.OnFinish -= FinishLevel;
    }
}
