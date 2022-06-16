using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignManager : MonoBehaviour
{
    public static SignManager Instance { get; set; }

    private Dictionary<GameObject, SignObjectPooler> _pools;

    [SerializeField]
    private int _minSignsInRow = 2;

    [SerializeField]
    private int _maxSignsInRow = 3;

    [SerializeField]
    private int _initSigns = 3;

    [SerializeField]
    private List<GameObject> _prefabs;

    [SerializeField]
    private int _maxPoolCount;

    [SerializeField]
    private int _basePoolCount;

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
        if (GameManager.Instance.Survival) {
            InitializeObjectPoolFromList(_prefabs);
        } else
        {
            InitializeObjectPool();
        }
        signPairs = new Queue<SignNeighbors>(signPairsList);
    }

    private void InitializeObjectPool()
    {
        _pools = new Dictionary<GameObject, SignObjectPooler>();
        foreach (SignNeighbors signs in signPairsList)
        {
            foreach (SignTemplate template in signs.neighbors)
            {
                if (!_pools.ContainsKey(template.Prefab))
                {
                    _pools[template.Prefab] = gameObject.AddComponent<SignObjectPooler>();
                    _pools[template.Prefab].MaxPoolCount = _basePoolCount;
                    _pools[template.Prefab].Prefab = template.Prefab;
                }
                else
                {
                    _pools[template.Prefab].MaxPoolCount += _basePoolCount;
                }
            }
        }
    }

    private void InitializeObjectPoolFromList(List<GameObject> prefabs)
    {
        _pools = new Dictionary<GameObject, SignObjectPooler>();
        foreach (GameObject prefab in prefabs)
        {
            if (!_pools.ContainsKey(prefab))
            {
                _pools[prefab] = gameObject.AddComponent<SignObjectPooler>();
                _pools[prefab].MaxPoolCount = _basePoolCount;
                _pools[prefab].Prefab = prefab;
            }
            else
            {
                _pools[prefab].MaxPoolCount += _basePoolCount;
            }
        }
    }

    private void Start()
    {
        for (int i = 0; i < _initSigns; ++i)
        {
            SpawnSigns();
        }
    }

    private void InitializeSigns()
    {
        int count = Random.Range(_minSignsInRow, _maxSignsInRow + 1);
        List<Equation> equations = EquationOracle.Instance.ChooseEquations(count);

        SignNeighbors signs = new SignNeighbors();

        foreach (Equation equation in equations)
        {
            SignTemplate template = new SignTemplate();
            template.Prefab = _prefabs[Random.Range(0, _prefabs.Count)];
            template.Equation = equation;
            signs.neighbors.Add(template);
        }

        signPairs.Enqueue(signs);
    }

    private void SpawnSigns()
    {
        if (GameManager.Instance.Survival) InitializeSigns();

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
                if (sign == null) continue;
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

    private SignObjectPooler GetObjectPooler(GameObject prefab)
    {
        if (!_pools.ContainsKey(prefab))
        {
            Debug.Log("Unknown Prefab!");
            return null;
        }
        return _pools[prefab];
    }

    private Sign SpawnSign(SignTemplate template, Vector3 scale, int idx)
    {
        SignObjectPooler pool = GetObjectPooler(template.Prefab);
        
        if (pool == null)
        {
            Debug.Log("Unknown prefab!");
            return null;
        }
        
        Sign sign = GetObjectPooler(template.Prefab).Spawn();
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
