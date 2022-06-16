using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Squad
{
    public static PlayerManager Instance { get; set; }

    private Dictionary<GameObject, UnitObjectPooler> _pools;

    [SerializeField]
    private int _basePoolCount;

    [field: SerializeField]
    public float VerticalSpeed { get; set; }
    
    [field: SerializeField]
    public float HorizontalSpeed { get; set; }

    public delegate void FailedLevel();
    public static event FailedLevel OnDeath;

    public delegate void ChangeCountSign(Sign sign);
    public static event ChangeCountSign OnHitSign;

    public delegate void Recomputed();
    public static event Recomputed OnRecompute;

    private void Awake()
    {
        Instance = this;
        InitializeObjectPools();
        base.Initialize();
    }

    private new void RecomputeSquad(int newCount)
    {
        InCollision = true;
        if (newCount < 1) // Fail level
        {
            Die();
        }
        else
        {
            OnRecompute();
            base.RecomputeSquad(newCount);
        }
        CancelCollision();
    }

    protected override UnitObjectPooler GetObjectPooler(GameObject prefab)
    {
        if (!_pools.ContainsKey(prefab))
        {
            Debug.Log("Unknown Prefab!");
            return null;
        }
        return _pools[prefab];
    }

    private void InitializeObjectPools()
    {
        _pools = new Dictionary<GameObject, UnitObjectPooler>();
        foreach (var pair in _characterDictionary.costPairs)
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

    private void CollideWithRegularSign(Sign sign)
    {
        if (!InCollision)
        {
            int result = sign.Equation.Compute(Count);
            OnHitSign(sign);
            RecomputeSquad(result);
        }
    }

    private void Die()
    {
        DestroySquad();
        OnDeath();
    }

    private void OnEnable()
    {
        PlayerMovement.OnHit += CollideWithRegularSign;
        EnemySquad.OnHit += RecomputeSquad;
        EnemySquad.OnDeath += Die;
    }

    private void OnDisable()
    {
        PlayerMovement.OnHit -= CollideWithRegularSign;
        EnemySquad.OnHit -= RecomputeSquad;
        EnemySquad.OnDeath -= Die;
    }
}
