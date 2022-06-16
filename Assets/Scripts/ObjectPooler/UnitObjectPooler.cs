using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitObjectPooler : ObjectPooler<Unit>
{
    public override int MaxPoolCount { get; set; } = 16;
    public override GameObject Prefab { get; set; }
}
