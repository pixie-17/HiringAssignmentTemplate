using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/* Controls the spawning of single tile */
public class FloorManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public Material tileMaterial;
    public Transform spawnPosition;

    void Start()
    {
        for (int i = 0; i < 25; ++i)
        {
            SpawnTile();
        }
    }

    public void SpawnTile()
    {
        GameObject tile = Instantiate(tilePrefab, spawnPosition.position, Quaternion.identity);
        tile.GetComponent<MeshRenderer>().material = tileMaterial;
        spawnPosition.Translate(Vector3.forward * tilePrefab.GetComponent<MeshRenderer>().bounds.size.x);
    }
}
