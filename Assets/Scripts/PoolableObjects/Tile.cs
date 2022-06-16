using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(MeshRenderer))]
public class Tile : PoolableObject<Tile>
{
    private MeshRenderer meshRenderer;
    public delegate void LeftScreen();
    public static event LeftScreen OnLeftScreen;

    public override void ReturnToPool()
    {
        Pool.Release(this);
    }

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (gameObject.activeInHierarchy && IsBehindCamera())
        {
            OnLeftScreen();
            ReturnToPool();
        }
    }

    private bool IsBehindCamera()
    {
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.y - meshRenderer.bounds.center.y));
        return meshRenderer.bounds.center.z < screenBounds.z;
    }
}
