using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System;

public class PrefabPair<T>
{
    [field: SerializeField]
    public T value;
    [field: SerializeField]
    public GameObject prefab;
}

public abstract class PrefabDictionary<T> : SerializedScriptableObject
{
    public List<PrefabPair<T>> costPairs;
}

[CreateAssetMenu(fileName = "CharacterDictionary", menuName = "ScriptableObjects/CharacterDictionary", order = 1)]
public class CharacterDictionary : PrefabDictionary<int>
{
    public void Sort()
    {
        Comparer<PrefabPair<int>> descendingComparer = Comparer<PrefabPair<int>>.Create((x, y) => y.value.CompareTo(x.value));
        costPairs.Sort(descendingComparer);
    }
}