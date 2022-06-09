using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject leftSignPrefab;
    public GameObject rightSignPrefab;
    public Material tileMaterial;
    public Transform spawnPosition;
    private Queue<Tuple<Operation, int>> signOperation;


    void Start()
    {
        for (int i = 0; i < 20; ++i)
        {
            SpawnTile();
        }

        //StartCoroutine(SpawnSigns());
    }

    public void SpawnTile()
    {
        GameObject tile = Instantiate(tilePrefab, spawnPosition.position, Quaternion.identity);
        tile.GetComponent<MeshRenderer>().material = tileMaterial;
        spawnPosition.Translate(Vector3.forward * tilePrefab.GetComponent<MeshRenderer>().bounds.size.x);
    }

    IEnumerator SpawnSigns()
    {
        while (true)
        {
            GameObject left = Instantiate(leftSignPrefab, spawnPosition.position, Quaternion.AngleAxis(90, Vector3.left));
            GameObject right = Instantiate(rightSignPrefab, spawnPosition.position + new Vector3(0.5f, 0f, 0f), Quaternion.AngleAxis(90, Vector3.left));

            left.GetComponent<Sign>().neighbouringSign = right;
            right.GetComponent<Sign>().neighbouringSign = left;

            yield return new WaitForSeconds(5f);
        }
    }
}
