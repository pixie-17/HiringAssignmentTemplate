using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

/* Controller for survival mode */
public class SurvivalMode : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject signPrefab;
    //public Transform spawnPosition;
    public float signStep;
    public Material[] signMaterials;
    public TMP_Text scoreText;
    public int score = 0;
    private List<OperationType> operations = new List<OperationType>();
    private int maxPoints = 1;


    void Start()
    {
        operations = System.Enum.GetValues(typeof(OperationType)).Cast<OperationType>().ToList();
        StartCoroutine(SpawnSigns());
        StartCoroutine(SpawnEnemies());
    }

    void Update()
    {
        scoreText.text = "SCORE: " + score;
        //spawnPosition.position += GameManager.instance.playerSpeed * Time.deltaTime * Vector3.forward;
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(signStep * 0.5f);
        while (true)
        {
            yield return new WaitForSeconds(signStep);
            //Vector3 position = spawnPosition.position + new Vector3(0.5f, 0, 0);
            Vector3 position = GameManager.instance.floorManager.spawnPosition.position + new Vector3(0.5f, 0, 0);
            GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.AngleAxis(180f, Vector3.up));
            enemy.GetComponent<SquadManager>().count = ChooseCount();
            maxPoints -= enemy.GetComponent<SquadManager>().count;
            enemy.GetComponent<SquadManager>().DestroySquad();
            enemy.GetComponent<SquadManager>().GenerateSquad(position);
        }
    }

    IEnumerator SpawnSigns()
    {

        while (true)
        {
            yield return new WaitForSeconds(signStep);
            Vector3 position = GameManager.instance.floorManager.spawnPosition.position;
            GameObject signPair = Instantiate(signPrefab, position, Quaternion.identity);

            Sign[] signs = signPair.GetComponentsInChildren<Sign>();
            int max = int.MinValue;

            while (max < 2)
            {
                for (int i = 0; i < signs.Length; ++i)
                {
                    signs[i].GetComponent<MeshRenderer>().material = signMaterials[(int)(Random.value * (float)signMaterials.Length)];
                    signs[i].operation = new Operation(operations[(int)(Random.value * (float)(operations.Count - 1))]);
                    signs[i].operand = (int)(Random.Range(1f, 12f));
                    max = System.Math.Max(max, signs[i].operation.Compute(maxPoints, signs[i].operand));
                }
            }

            maxPoints = max;
        }
    }


    public int ChooseCount()
    {
        return (int) (Random.Range((0.65f * maxPoints), (float) maxPoints - 1f));
    }
}
