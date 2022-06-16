using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Equation // For more complex equations could be done as a parser - input string output equation or a scriptable object (but this could mean a lot of files for predefined equations)
{
    [SerializeField]
    private Operation operation;
    [SerializeField]
    private int operand;

    public override string ToString()
    {
        return operation.ToString() + " " + operand;
    }

    public int Compute(int value)
    {
        return operation.Compute(value, operand);
    }
}
