using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Equation // For more complex equations could be done as a parser - input string output equation or a scriptable object (but this could mean a lot of files for predefined equations)
{
    [field: SerializeField]
    public Operation Operation { get; set; }
    [field: SerializeField]
    public int Operand { get; set; }

    public override string ToString()
    {
        return Operation.ToString() + " " + Operand;
    }

    public int Compute(int value)
    {
        return Operation.Compute(value, Operand);
    }
}
