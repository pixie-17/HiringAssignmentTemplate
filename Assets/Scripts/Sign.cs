using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sign : MonoBehaviour
{
    public TMP_Text text;
    public Operation operation;
    public int operand;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "X " + operation + " " + operand; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
