using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterManager : MonoBehaviour
{
    public GameObject characterPrefab, leaderPrefab;
    public Material[] unitMaterials; // Red = 100, Blue = 25, Green = 10, Orange = 5, Yellow = 1
    public Transform spawnPosition;
    private GameObject leader;
    public float speed = 2f;
    public int count = 1;
    public static CharacterManager instance = null;
    public int maxPossibleScore = 1;
    public TMP_Text text;
    public Transform[] spawnPositions;
    public Queue<GameObject> squad = new Queue<GameObject>();
    public bool levelFinished = false;
    
    public void Awake()
    {
        instance = this;
        leader = Instantiate(leaderPrefab, spawnPosition.position, Quaternion.identity);
        leader.GetComponentInChildren<SkinnedMeshRenderer>().material = unitMaterials[unitMaterials.Length - 1];
        text = leader.GetComponentInChildren<TMP_Text>();
        squad.Enqueue(leader);
        leader.transform.parent = spawnPosition.gameObject.transform;
    }

    public void Update()
    {
        text.text = "" + count;

        if (levelFinished)
        {
            foreach (GameObject unit in squad)
            {
                unit.GetComponent<Animator>().SetBool("levelFinished", true);
            }
        }
    }

    public void GenerateSquad()
    {
        if (count != 0)
        {
            int n = count;
            int spawnIndex = 0;
            List<int> units = new List<int>() { 100, 25, 10, 5, 1 };
            while (n > 0)
            {
                for (int i = 0; i < units.Count && spawnIndex < spawnPositions.Length; ++i)
                {
                    if (n - units[i] >= 0)
                    {
                        n -= units[i];
                        GameObject prefab = spawnIndex == 0 ? leaderPrefab : characterPrefab;
                        GameObject unit = Instantiate(prefab, leader.transform.position + spawnPositions[spawnIndex].localPosition, Quaternion.identity);
                        unit.GetComponentInChildren<SkinnedMeshRenderer>().material = unitMaterials[i];
                        squad.Enqueue(unit);
                        spawnIndex++;
                    }
                }

                if (spawnIndex == spawnPositions.Length)
                {
                    break;
                }
            }

            leader = squad.Peek();
            leader.transform.parent = spawnPosition.gameObject.transform;
            text = leader.GetComponentInChildren<TMP_Text>();
        }
    }

    public void DestroySquad()
    {
        while (squad.Count != 0)
        {
            GameObject unit = squad.Dequeue();
            Destroy(unit);
        }
    }
    
}
