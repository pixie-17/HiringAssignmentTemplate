using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject signPrefab;
    public Material leftSignMaterial;
    public Material rightSignMaterial;
    public Material tileMaterial;
    public Transform spawnPosition;

    void Start()
    {
        for (int i = 0; i < 20; ++i)
        {
            SpawnTile();
        }

        StartCoroutine(SpawnSigns());
    }

    public void SpawnTile()
    {
        GameObject tile = Instantiate(tilePrefab, spawnPosition.position, Quaternion.identity);
        tile.GetComponent<MeshRenderer>().material = tileMaterial;
        spawnPosition.Translate(Vector3.forward * tilePrefab.GetComponent<MeshRenderer>().bounds.size.x);
    }

    IEnumerator SpawnSigns()
    {
        GameObject leftSign, rightSign;

        while (true)
        {
            leftSign = Instantiate(signPrefab, spawnPosition.position, Quaternion.AngleAxis(90, Vector3.left));
            leftSign.GetComponent<MeshRenderer>().material = leftSignMaterial;
            rightSign = Instantiate(signPrefab, spawnPosition.position + new Vector3(0.5f, 0f, 0f), Quaternion.AngleAxis(90, Vector3.left));
            rightSign.GetComponent<MeshRenderer>().material = rightSignMaterial;
            yield return new WaitForSeconds(5f);
        }
    }
}
