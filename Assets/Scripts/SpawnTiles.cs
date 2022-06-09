using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTiles : MonoBehaviour
{
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Leader")
        {
            StartCoroutine(UpdateTiles());
        }
    }

    IEnumerator UpdateTiles()
    {
        yield return new WaitForSeconds(2f);
        FindObjectOfType<FloorManager>().SpawnTile();
        Destroy(gameObject);
    }
}
