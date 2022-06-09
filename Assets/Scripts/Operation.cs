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
}
