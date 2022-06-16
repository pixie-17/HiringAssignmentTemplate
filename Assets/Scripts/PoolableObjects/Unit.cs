using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Unit : PoolableObject<Unit>
{
    public TMP_Text _countText;
    public override void ReturnToPool()
    {
        Pool.Release(this);
    }

    private void OnEnable()
    {
        gameObject.SetActive(true);
        _countText.gameObject.SetActive(false);
    }

    protected new void Reset()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        _countText.gameObject.SetActive(false);
    }
}