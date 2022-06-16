using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TileObjectPooler : ObjectPooler<Tile>
{
    public override int MaxPoolCount { get; set; } = 25;
    public override GameObject Prefab { get; set; }
}
