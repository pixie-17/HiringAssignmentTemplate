using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    public GameObject characterPrefab, leaderPrefab;
    public Material[] unitMaterials; // Red = 100, Blue = 25, Green = 10, Orange = 5, Yellow = 1
    public Transform spawnPosition;
    private GameObject leader;
    public int count = 1;
    public TMP_Text text;
    public Transform[] spawnPositions;
    public Queue<GameObject> squad = new Queue<GameObject>();
    
    public void Awake()
    {
        GenerateSquad();
    }

    public void Update()
    {
        text.text = "" + count;
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
                        GameObject unit = Instantiate(prefab, spawnPosition.localPosition + spawnPositions[spawnIndex].localPosition, Quaternion.identity, this.transform);
                        unit.GetComponentInChildren<SkinnedMeshRenderer>().material = unitMaterials[i];
                        squad.Enqueue(unit);
                        spawnIndex++;
                    }
                }
            }

            leader = squad.Peek();
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
