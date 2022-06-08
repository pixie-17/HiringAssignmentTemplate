using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform startPosition;
    private GameObject player;
    public float speed = 1.85f;

    void Start()
    {
        player = Instantiate(playerPrefab, startPosition.position, Quaternion.identity);
        player.transform.parent = startPosition.gameObject.transform;
    }

    void Update()
    {
        float offset = speed * Time.deltaTime;
        player.transform.position = new Vector3(0.5f, 0, player.transform.position.z + offset);
    }
}
