using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "SquadTemplate", menuName = "ScriptableObjects/SquadTemplate", order = 1)]
public class SquadTemplate : SerializedScriptableObject
{
    [InfoBox("The spawn position of leader is at center of formation = the local position of whole formation!")]
    [field: SerializeField]
    public GameObject FormationPrefab { get; set; }
    [field: SerializeField]
    public CharacterDictionary UnitValues { get; set; }
    [field: SerializeField]
    public float Angle { get; set; }
}
