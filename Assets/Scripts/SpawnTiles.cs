using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTiles : MonoBehaviour
{
    public void OnCollisionExit(Collision collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(UpdateTiles());
        }
    }

    IEnumerator UpdateTiles()
    {
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<FloorManager>().SpawnTile();
        Destroy(gameObject);
    }
}
