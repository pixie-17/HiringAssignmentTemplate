using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class ObjectPooler<T> : MonoBehaviour where T : PoolableObject<T>
{
    private int _defaultCount = 3;
    public abstract int MaxPoolCount { get; set; }
    public abstract GameObject Prefab { get; set; }

    public IObjectPool<T> Pool
    {
        get
        {
            if (_pool == null)
            {
                _pool = new ObjectPool<T>(CreateItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, _defaultCount, MaxPoolCount);
            }
            return _pool;
        }
    }

    private IObjectPool<T> _pool;
    
    private T CreateItem()
    {
        GameObject obj = Instantiate(Prefab, Vector3.zero, Quaternion.identity);

        if (!obj.TryGetComponent<T>(out T component))
        {
            Debug.LogWarning("Component script missing!");
            component = obj.AddComponent<T>();
        }

        component.Pool = Pool;

        return component;
    }

    private void OnTakeFromPool(T obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void OnReturnedToPool(T obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(T obj)
    {
        Destroy(obj.gameObject);
    }

    public T Spawn()
    {
        return Pool.Get();
    }
}
