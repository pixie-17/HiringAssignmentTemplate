using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignObjectPooler : ObjectPooler<Sign>
{
    public override int MaxPoolCount { get; set; } = 8;
    public override GameObject Prefab { get; set; }
}
