using System.Collections;
using UnityEngine;

/* Controls the spawning of tiles */
public class FloorManager : MonoBehaviour
{
    public static FloorManager Instance { get; set; }
    private TileObjectPooler _pool;

    [SerializeField]
    private int _pathLength = 25;
    
    [SerializeField]
    private int _reserveTiles = 5;

    [SerializeField]
    private GameObject _prefab;

    [SerializeField]
    private Transform _tileSpawnPoint;

    private float step = 1f;

    private void Awake()
    {
        Instance = this;
        InitializeObjectPool();
    }

    private void Start()
    {
        if (_prefab.TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer))
        {
            step = meshRenderer.bounds.size.z;
        }

        for (int i = 0; i < _pathLength; ++i)
        {
            SpawnTile();
        }
    }

    private void InitializeObjectPool()
    {
        _pool = gameObject.AddComponent<TileObjectPooler>();
        _pool.MaxPoolCount = _pathLength + _reserveTiles;
        _pool.Prefab = _prefab;
    }

    private void SpawnTile()
    {
        Tile tile = _pool.Spawn();
        tile.transform.position = _tileSpawnPoint.position;
        tile.transform.rotation = Quaternion.identity;
        tile.transform.SetParent(transform, true);
        _tileSpawnPoint.Translate(Vector3.forward * step);
    }

    private void OnEnable()
    {
        Tile.OnLeftScreen += SpawnTile;
    }

    private void OnDisable()
    {
        Tile.OnLeftScreen -= SpawnTile;
    }
}
