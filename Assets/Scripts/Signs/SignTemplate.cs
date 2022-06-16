using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SignNeighbors
{
    [field: SerializeField]
    public List<SignTemplate> neighbors;
}

[System.Serializable]
public class SignTemplate
{
    [field: SerializeField]
    public Material Material { get; set; }
    
    [field: SerializeField]
    public Equation Equation { get; set; }
}