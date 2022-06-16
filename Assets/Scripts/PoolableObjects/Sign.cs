using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sign : PoolableObject<Sign>
{
    [SerializeField]
    private TMP_Text _text;

    public Equation Equation { get; set; }
    public GameObject Prefab { get; set; }

    public List<Sign> NeighboringSigns { get; set; }

    void Start()
    {
        UpdateText();
    }

    public void InitializeFromConfig(SignTemplate template)
    {
        Equation = template.Equation;
        Prefab = template.Prefab;
        UpdateText();
    }

    private void UpdateText()
    {
        _text.text = "X " + Equation;
    }

    protected new void Reset()
    {
        transform.position = Vector3.zero;
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.identity;
        NeighboringSigns.Clear();
    }

    public override void ReturnToPool()
    {
        Pool.Release(this);
    }

    public void ReturnNeighborsToPool()
    {
        foreach (Sign neighbor in NeighboringSigns)
        {
            if (neighbor.isActiveAndEnabled)
            {
                neighbor.ReturnToPool();
            }
        }
    }
}
