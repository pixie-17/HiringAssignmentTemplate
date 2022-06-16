using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySquad : Squad
{
    public delegate void FailedLevel();
    public static event FailedLevel OnDeath;

    public delegate void Hit(int value);
    public static event Hit OnHit;

    public void InitializeFromConfigFile(SquadDefinition config, Vector3 point)
    {
        _formationPrefab = config.FormationPrefab;
        _angle = config.Angle;
        _count = config.Count;
        _characterDictionary = config.UnitValues;
        transform.position = point;

        base.Initialize();

        SetSquadReference();
    }

    private void SetSquadReference()
    {
        foreach (Unit unit in _squad)
        {
            if (unit.gameObject.TryGetComponent(out EnemyMovement mover))
            {
                mover.Squad = this;
            } else
            {
                Debug.Log("Missing mover script!");
            }
        }
    }

    public void RecomputeSquad()
    {
        InCollision = true;
        int playerUnitCount = PlayerManager.Instance.Count;
        int result = playerUnitCount - _count;

        if (result < 1) // Fail level
        {
            RecomputeSquad(_count - playerUnitCount);
            OnDeath();
        }
        else
        {
            DestroySquad();
            OnHit(result);
            Destroy(gameObject);
        }
        CancelCollision();
    }

    protected override UnitObjectPooler GetObjectPooler(GameObject prefab)
    {
        return EnemyManager.Instance.GetObjectPooler(prefab);
    }

    protected override string GetParentName()
    {
        return "EnemySquad";
    }
}
