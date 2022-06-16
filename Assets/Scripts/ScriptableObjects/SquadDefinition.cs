using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "SquadDefinition", menuName = "ScriptableObjects/SquadDefinition", order = 1)]
public class SquadDefinition : SerializedScriptableObject
{
    [InfoBox("The spawn position of leader is at center of formation = the local position of whole formation!")]
    [field: SerializeField]
    public GameObject FormationPrefab { get; set; }
    [field: SerializeField]
    public CharacterDictionary UnitValues { get; set; }
    [field: SerializeField]
    public float Angle { get; set; }
    [field: SerializeField]
    public int Count { get; set; }
}
