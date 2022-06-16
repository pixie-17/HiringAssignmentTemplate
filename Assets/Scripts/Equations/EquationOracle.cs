using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquationOracle : MonoBehaviour
{
    public static EquationOracle Instance { get; set; }

    [SerializeField]
    private float _successPercentage = 0.25f;

    [SerializeField]
    private int _maxOperand;

    private int _maxPossibleCount = 1;
    private List<OperationType> operations = new List<OperationType>();

    private void Awake()
    {
        operations = System.Enum.GetValues(typeof(OperationType)).Cast<OperationType>().ToList();
        Instance = this;
    }

    public List<Equation> ChooseEquations(int count)
    {
        List<Equation> equations = new List<Equation>();

        /* Generate <count> equations without checking if they are winning 
         * (wee need at least one winning equation which is managed in <while> loop 
         */
        
        int max = int.MinValue;

        for (int i = 0; i < count; ++i)
        {
            Equation equation = GetEquation();
            equations.Add(equation);
            max = Mathf.Max(max, equation.Compute(_maxPossibleCount));
        }

        while (max < 2)
        {
            Equation equation = GetEquation();
            max = Mathf.Max(max, equation.Compute(_maxPossibleCount));
            if (max > 2)
            {
                equations[equations.Count - 1] = equation;
                break;
            }
        }

        equations.Shuffle();

        _maxPossibleCount = max;
        return equations;
    }

    private Equation GetEquation()
    {
        Equation equation = new Equation();
        equation.Operation = new Operation(operations[(int)(Random.value * (float)(operations.Count - 1))]);
        equation.Operand = (int)(Random.Range(1f, (float)_maxOperand));
        return equation;
    }

    public int ChooseEnemyCount()
    {
        int count = (int)(Random.Range(_successPercentage * _maxPossibleCount, (float)_maxPossibleCount));
        _maxPossibleCount -= count;
        return count;
    }
}
