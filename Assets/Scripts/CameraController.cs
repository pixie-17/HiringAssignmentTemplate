using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Movement player;

    void Update()
    {
        transform.position += Vector3.forward * player.speed * Time.deltaTime;
    }
}
