using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructedDropPickup : MonoBehaviour, IDestructible
{
    private CharacterStats stats;
    private SpawnItem spawner;
    private GameObject itemPickup;

    private void Awake()
    {
        stats = GetComponent<CharacterStats>();
        spawner = GetComponent<SpawnItem>();
    }

    public void OnDestrcution(GameObject destroyer)
    {
        itemPickup = spawner.CreateSpawn();

        if (itemPickup == null)
            return;

        if (itemPickup.GetComponent<ItemPickup>().itemDefinition.weaponSlotObject == stats.GetCurrentAttack())
        {
            if (stats.GetCurrentWeapon() != null)
                Destroy(stats.GetCurrentWeapon());
        }
    }
}
