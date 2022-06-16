using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Main game controller */
public class GameManager : MonoBehaviour
{
    [field: SerializeField]
    public bool Survival { get; set; }
    public bool LevelFinished { get; set; }
    public static GameManager Instance = null;

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
        Time.timeScale = 0f;
    }

    private void OnEnable()
    {
        PlayerManager.OnDeath += FailLevel;
        PlayerMovement.OnFinish += FinishLevel;
    }
    private void OnDisable()
    {
        PlayerManager.OnDeath -= FailLevel;
        PlayerMovement.OnFinish -= FinishLevel;
    }
}
