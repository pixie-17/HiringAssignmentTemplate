using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sign : MonoBehaviour
{
    public TMP_Text text;
    public Operation operation;
    public int operand;
    public GameObject neighbouringSign;
    public Material mat;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "X " + operation + " " + operand;
        GetComponent<MeshRenderer>().material = mat;
    }

    private void Update()
    {
        text.text = "X " + operation + " " + operand;
    }
}
