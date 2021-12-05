using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnItem : MonoBehaviour, ISpawns
{
    [Serializable]
    public struct ItemDefinition
    {
        public ItemPickupSO itemPickup;
        public int spawnChance;
    }
    public ItemDefinition[] itemDefinitions;

    private int whichToSpawn = 0;
    private int totalSpawnChance = 0;
    private int chosen = 0;

    public GameObject itemSpawned { get; set; }
    public ItemPickup itemType { get; set; }

    void Start()
    {
        foreach (ItemDefinition ip in itemDefinitions)
        {
            totalSpawnChance += ip.spawnChance;
        }
        totalSpawnChance = Math.Max(totalSpawnChance, 100);
        chosen = Random.Range(0, totalSpawnChance);
    }

    public GameObject CreateSpawn()
    {
        foreach (ItemDefinition ip in itemDefinitions)
        {
            whichToSpawn += ip.spawnChance;
            if (whichToSpawn > chosen)
            {
                itemSpawned = Instantiate(ip.itemPickup.itemSpawnObject, transform.position, Quaternion.identity).gameObject;

                itemType = itemSpawned.GetComponent<ItemPickup>();
                itemType.itemDefinition = ip.itemPickup;
                itemSpawned.GetComponent<ItemPickup>().StartRise();
                return itemSpawned;
            }
        }
        return null;
    }
}
