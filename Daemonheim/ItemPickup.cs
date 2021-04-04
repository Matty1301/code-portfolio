using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    public ItemPickupSO itemDefinition;
    private CharacterInventory charInv;
    private CharacterStats playerStats;
    private GameObject player;
    private Vector3 spawnPoint;

    private void Awake()
    {
        spawnPoint = transform.position;
    }

    private void Start()
    {
        charInv = CharacterInventory.instance;
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<CharacterStats>();
    }

    //Start the coroutine to play the rising animation
    public void StartRising()
    {
        Tutorial.Instance.ShowPrompt(2);
        StartCoroutine(Rising());
    }

    //Play the rising animation
    IEnumerator Rising()
    {
        while (transform.position.y < spawnPoint.y + 1)
        {
            transform.Translate(Vector3.up * Time.deltaTime * 0.5f);
            yield return null;
        }
    }

    private void StoreItemInInventory()
    {
        charInv.StoreItem(itemDefinition);
        player.GetComponent<AudioSource>().clip = SoundManager.Instance.SoundFX.Find(sfx => sfx.Effect == SoundEffect.ItemPickup).Clip;
        player.GetComponent<AudioSource>().Play();
        Tutorial.Instance.ShowPrompt(3);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            OnInteraction();
    }

    public void OnInteraction()
    {
        //If the item can be stored store the item
        if (itemDefinition.isStorable)
        {
            StoreItemInInventory();

            //If the item is a weapon and there is currently no weapon equipped equip the weapon
            if (itemDefinition.itemType == ItemTypeDefinitions.WEAPON && playerStats.GetCurrentWeapon() == null)
                itemDefinition.UseItem();
        }
        else
        {
            //If the item cant be stored use it immediately
            itemDefinition.UseItem();
        }

        Destroy(this.gameObject);
    }
}
