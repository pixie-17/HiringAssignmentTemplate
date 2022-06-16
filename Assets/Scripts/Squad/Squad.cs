using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
using System;
using System.Collections;

/* Controls the formation of units */
public abstract class Squad : MonoBehaviour
{
    [InfoBox("The spawn position of leader is at center of formation = the first position in formation!")]
    [SerializeField]
    protected GameObject _formationPrefab;

    [field: SerializeField]
    protected CharacterDictionary _characterDictionary;

    [SerializeField]
    protected float _angle;

    [SerializeField]
    protected int _count;
    public int Count { 
        
        get
        {
            return _count;
        }
    }

    public bool InCollision { get; set; }

    protected TMP_Text _countText;
    protected Queue<Unit> _squad = new Queue<Unit>();
    protected int _formationLength;
    
    private List<Vector3> _spawnPoints;

    protected void Initialize()
    {
        InitializeBasicVariables();
        SortUnitValues();
        GenerateSquad(transform.position);
    }

    private void InitializeBasicVariables()
    {
        _formationLength = _formationPrefab.transform.childCount;
        _spawnPoints = new List<Vector3>();
        foreach (Transform child in _formationPrefab.transform)
        {
            _spawnPoints.Add(child.transform.localPosition);
        }
    }

    private void SortUnitValues()
    {
        _characterDictionary.Sort();
    }

    private void ChangeCountText(int count)
    {
        if (_countText != null)
        {
            _countText.text = "" + count;
        }
    }

    private void GenerateSquad(Vector3 center)
    {
        if (_count != 0)
        {
            int n = _count;
            int slotIndex = 0;

            while (n > 0)
            {
                for (int i = 0; i < _characterDictionary.costPairs.Count && slotIndex < _formationLength; ++i)
                {
                    int cost = _characterDictionary.costPairs[i].value;
                    UnitObjectPooler pool = GetObjectPooler(_characterDictionary.costPairs[i].prefab);

                    if (pool == null)
                    {
                        continue;
                    }

                    if (n - cost >= 0)
                    {
                        n -= cost;
                        SpawnUnit(center + _spawnPoints[slotIndex], pool);
                        slotIndex++;
                    }
                }

                if (slotIndex >= _formationLength)
                {
                    break;
                }
            }

            UpdateCountTextRef();
            ChangeCountText(_count);
        }
    }

    protected abstract UnitObjectPooler GetObjectPooler(GameObject prefab);

    protected void DestroySquad()
    {
        while (_squad.Count != 0)
        {
            Unit unit = _squad.Dequeue();
            unit.ReturnToPool();
        }
    }

    protected void RecomputeSquad(int value)
    {
        _count = value;
        DestroySquad();
        GenerateSquad(transform.position);
    }

    private void SpawnUnit(Vector3 slotPosition, UnitObjectPooler pool)
    {
        Unit unit = pool.Spawn();
        unit.transform.position = slotPosition;
        unit.transform.rotation = Quaternion.AngleAxis(_angle, Vector3.up);
        _squad.Enqueue(unit);
    }

    protected void CancelCollision()
    {
        StartCoroutine(RemoveCollisionFlag());
    }

    IEnumerator RemoveCollisionFlag()
    {
        yield return new WaitForSeconds(0.5f);
        InCollision = false;
    }

    private void UpdateCountTextRef()
    {
        if (_squad.Count != 0)
        {
            _countText = _squad.Peek()._countText;
            _countText.gameObject.SetActive(true);
        }
    }
}

