using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    void Update()
    {
        if (GameManager.instance != null && !GameManager.instance.LevelFinished)
        {
            transform.position += Vector3.forward * GameManager.instance.playerSpeed * Time.deltaTime;
        }
    }
}
