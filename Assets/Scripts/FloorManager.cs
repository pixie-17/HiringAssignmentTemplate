using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public Transform spawnPosition;

    void Start()
    {
        for (int i = 0; i < 40; ++i)
        {
            SpawnTile();
        }
    }

    public void SpawnTile()
    {
        Instantiate(tilePrefab, spawnPosition.position, Quaternion.identity);
        spawnPosition.Translate(Vector3.forward * tilePrefab.GetComponent<MeshRenderer>().bounds.size.x);
    }
}
