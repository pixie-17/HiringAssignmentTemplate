using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject characterPrefab;
    public Transform spawnPosition;
    private GameObject character;
    public float speed = 2f;

    void Awake()
    {
        character = Instantiate(characterPrefab, spawnPosition.position, Quaternion.identity);
        character.transform.parent = spawnPosition.gameObject.transform;
    }
}
