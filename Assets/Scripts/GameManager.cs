using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerLeader { get; set; }
    public bool survival;
    public SquadManager playerSquad;
    public float playerSpeed, enemySpeed;
    public SurvivalMode survivalMode;
    public FloorManager floorManager;
    public bool LevelFinished { get; set; }
    public static GameManager instance = null;

    private void Awake()
    {
        instance = this;
        LevelFinished = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerLeader = GameObject.FindWithTag("Leader");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerLeader = GameObject.FindWithTag("Leader");
        
        if (LevelFinished)
        {
            playerSquad.Jump();
        }
    }
}
