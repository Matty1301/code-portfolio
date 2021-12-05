using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawns
{
    GameObject itemSpawned { get; set; }
    ItemPickup itemType { get; set; }

    GameObject CreateSpawn();
}
