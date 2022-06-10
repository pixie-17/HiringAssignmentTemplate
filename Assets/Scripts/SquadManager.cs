using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SquadManager : MonoBehaviour
{
    private Queue<GameObject> squad = new Queue<GameObject>();

    public GameObject characterPrefab, leaderPrefab;
    public Material[] unitMaterials; // Red = 100, Blue = 25, Green = 10, Orange = 5, Yellow = 1
    public Transform[] spawnPositions;
    public float angle;
    public int count;
    public TMP_Text CountText { get; set; }
    public bool InCollision { get; set; }


    public void Awake()
    {
        GenerateSquad(spawnPositions[0].position);
    }

    public void Update()
    {
        if (CountText != null)
        {
            CountText.text = "" + count;
        }
    }

    public void GenerateSquad(Vector3 center)
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
                        GameObject unit = Instantiate(prefab, center + spawnPositions[spawnIndex].localPosition, Quaternion.AngleAxis(angle, Vector3.up), this.transform);
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

            if (squad.Count != 0)
            {
                CountText = squad.Peek().GetComponentInChildren<TMP_Text>();
            }
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

    public void Jump()
    {
        foreach (GameObject unit in squad)
        {
            unit.GetComponent<Animator>().SetBool("levelFinished", true);
        }
    }
}

