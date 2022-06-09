using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterManager : MonoBehaviour
{
    public GameObject characterPrefab;
    public Transform spawnPosition;
    private GameObject character;
    public float speed = 2f;
    public int count = 1;
    public static CharacterManager instance = null;
    public int maxPossibleScore = 1;
    public TMP_Text text;

    public void Awake()
    {

        instance = this;
        character = Instantiate(characterPrefab, spawnPosition.position, Quaternion.identity);
        character.transform.parent = spawnPosition.gameObject.transform;
        text = character.GetComponentInChildren<TMP_Text>();
    }

    public void Update()
    {
       text.text = "" + count;
    }
}
