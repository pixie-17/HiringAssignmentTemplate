using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignManager : MonoBehaviour
{
    public static SignManager Instance { get; set; }

    private SignObjectPooler _pool;

    [SerializeField]
    private int _initSigns = 3;

    [SerializeField]
    private GameObject _prefab;

    [SerializeField]
    private int _maxPoolCount;

    [SerializeField]
    private float verticalStep = 4f;

    [SerializeField]
    private Vector3 _signRotation;

    [SerializeField]
    private Transform _signSpawnPoint;

    [SerializeField]
    private GameObject _endPrefab;

    private GameObject _endObject;

    [SerializeField]
    private List<SignNeighbors> signPairsList;
    private Queue<SignNeighbors> signPairs;

    public delegate void SpawnedSign(Vector3 position);
    public static event SpawnedSign OnSpawn;

    private void Awake()
    {
        Instance = this;
        InitializeObjectPool();
        signPairs = new Queue<SignNeighbors>(signPairsList);
    }

    private void InitializeObjectPool()
    {
        _pool = gameObject.AddComponent<SignObjectPooler>();
        _pool.MaxPoolCount = _maxPoolCount;
        _pool.Prefab = _prefab;
    }

    private void Start()
    {
        for (int i = 0; i < _initSigns; ++i)
        {
            SpawnSigns();
        }
    }

    private void SpawnSigns()
    {
        if (signPairs.Count != 0)
        {
            List<SignTemplate> signsToSpawn = signPairs.Dequeue().neighbors;
            List<Sign> signs = new List<Sign>();
           
            float scalingFactor = 1f / (float)signsToSpawn.Count;
            Vector3 scale = new Vector3(scalingFactor, 1f, scalingFactor);

            int idx = 0;
            foreach (SignTemplate template in signsToSpawn)
            {

                Sign sign = SpawnSign(template, scale, idx);
                signs.Add(sign);
                idx++;
            }

            foreach (Sign sign in signs)
            {
                sign.NeighboringSigns = signs;
            }

            OnSpawn(_signSpawnPoint.position + new Vector3(0.5f, 0f, verticalStep / 2f));
            _signSpawnPoint.Translate(verticalStep * Vector3.forward);
        } else
        {
            if (!GameManager.Instance.Survival && _endObject == null)
            {
                SpawnEnd();
            }
        }
    }

    private Sign SpawnSign(SignTemplate template, Vector3 scale, int idx)
    {

        Sign sign = _pool.Spawn();
        sign.InitializeFromConfig(template);
        sign.transform.localScale = scale;
        float step = 0.5f;

        if (sign.TryGetComponent(out MeshRenderer renderer))
        {
            step = renderer.bounds.size.x;
        }

        sign.transform.position = _signSpawnPoint.position + new Vector3(step * idx, 0f, 0f);
        sign.transform.rotation = Quaternion.Euler(_signRotation);
        return sign;
    }

    private void SpawnEnd()
    {
        _endObject = Instantiate(_endPrefab, _signSpawnPoint.position, Quaternion.Euler(_signRotation));
    }

    private void DestroyEnd()
    {
        Destroy(_endObject);
    }

    private void ReturnSignToPool(Sign sign)
    {
        sign.ReturnToPool();
    }

    private void RespawnSigns(Sign sign)
    {
        sign.ReturnNeighborsToPool();
        SpawnSigns();
    }

    private void OnEnable()
    {
        PlayerManager.OnHitSign += RespawnSigns;
        PlayerMovement.OnFinish += DestroyEnd;
    }

    private void OnDisable()
    {
        PlayerManager.OnHitSign -= RespawnSigns;
        PlayerMovement.OnFinish -= DestroyEnd;
    }
}
