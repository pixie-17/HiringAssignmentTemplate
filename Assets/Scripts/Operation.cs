using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public enum OperationType
{
    ADD,
    SUBTRACT,
    MULTIPLY,
    DIVIDE
}

[System.Serializable]
public class Operation
{
    public OperationType type;

    public override string ToString()
    {
        switch (type)
        {
            case OperationType.ADD:
                return "+";
            case OperationType.SUBTRACT:
                return "-";
            case OperationType.MULTIPLY:
                return "*";
            case OperationType.DIVIDE:
                return "/";
            default:
                return "Unknown operation";
        }
    }

    public int Compute(int x, int operand)
    {
        switch (type)
        {
            case OperationType.ADD:
                return x + operand;
            case OperationType.SUBTRACT:
                return x - operand;
            case OperationType.MULTIPLY:
                return x * operand;
            case OperationType.DIVIDE:
                return x / operand;
            default:
                return 0;
        }
    }
}
