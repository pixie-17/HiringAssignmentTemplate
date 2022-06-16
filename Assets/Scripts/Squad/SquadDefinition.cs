using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class SquadDefinition
{
    [InfoBox("The spawn position of leader is at center of formation = the local position of whole formation!")]
    [field: SerializeField]
    public SquadTemplate SquadTemplate { get; set; }
    [field: SerializeField]
    public int Count { get; set; }
}
