using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; set; }
    private Dictionary<GameObject, UnitObjectPooler> _pools;

    [SerializeField]
    private int _basePoolCount;
    
    [field: SerializeField]
    public float Speed { get; set; }
    
    [SerializeField]
    private List<SquadDefinition> _squadConfigsList;
    private Queue<SquadDefinition> _squadConfigs;

    private void Awake()
    {
        Instance = this;
        _squadConfigs = new Queue<SquadDefinition>(_squadConfigsList);
        InitializeObjectPools();
    }

    public UnitObjectPooler GetObjectPooler(GameObject prefab)
    {
        if (!_pools.ContainsKey(prefab))
        {
            Debug.Log("Unknown Prefab!");
            return null;
        }
        return _pools[prefab];
    }

    private void SpawnSquad(Vector3 position)
    {
        if (_squadConfigs.Count != 0)
        {
            SquadDefinition config = _squadConfigs.Dequeue();
            GameObject parent = new GameObject("EnemySquad");
            EnemySquad squad = parent.AddComponent<EnemySquad>();
            squad.InitializeFromConfigFile(config, position);
        }
    }

    private void InitializeObjectPools()
    {
        _pools = new Dictionary<GameObject, UnitObjectPooler>();
        foreach (SquadDefinition config in _squadConfigsList)
        {
            CharacterDictionary characterDictionary = config.UnitValues;
            foreach (var pair in characterDictionary.costPairs)
            {
                if (!_pools.ContainsKey(pair.prefab))
                {
                    _pools[pair.prefab] = gameObject.AddComponent<UnitObjectPooler>();
                    _pools[pair.prefab].MaxPoolCount = _basePoolCount;
                    _pools[pair.prefab].Prefab = pair.prefab;
                }
                else
                {
                    _pools[pair.prefab].MaxPoolCount += _basePoolCount;
                }
            }
        }
    }

    private void OnEnable()
    {
        SignManager.OnSpawn += SpawnSquad;
    }

    private void OnDisable()
    {
        SignManager.OnSpawn -= SpawnSquad;
    }
}
