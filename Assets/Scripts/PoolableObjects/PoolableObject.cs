using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class PoolableObject<T> : MonoBehaviour where T : Component
{
    public IObjectPool<T> Pool { get; set; }

    private void OnDisable()
    {
        Reset();
    }

    protected void Reset()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    public abstract void ReturnToPool();
}
