using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTypeDefinitions { HEALTH, WEAPON };

[CreateAssetMenu(fileName = "New Item", menuName = "ItemPickup", order = 1)]
public class ItemPickupSO : ScriptableObject
{
    public ItemTypeDefinitions itemType = ItemTypeDefinitions.HEALTH;
    public int itemAmount;
    public int spawnChanceWeight;

    public Rigidbody itemSpawnObject = null;
    public AttackDefinitionSO weaponSlotObject = null;

    public bool isEquipped = false;
    public bool isInteractable = false;
    public bool isStorable = false;
    public bool isUnique = false;
    public bool isIndestructable = false;
    public bool isQuestItem = false;
    public bool isStackable = false;
    public bool destroyOnUse = false;

    public Sprite icon;

    private CharacterStats playerStats;
    private GameObject player;

    public bool UseItem()
    {
        if (playerStats == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerStats = player.GetComponent<CharacterStats>();
        }

        switch (itemType)
        {
            case ItemTypeDefinitions.HEALTH:
                {
                    if (playerStats.GetHealth() < playerStats.characterDefinition.maxHealth)
                    {
                        player.GetComponent<AudioSource>().clip = SoundManager.Instance.SoundFX.Find(sfx => sfx.Effect == SoundEffect.DrinkPotion).Clip;
                        player.GetComponent<AudioSource>().Play();
                        playerStats.GainHealth(itemAmount);
                        return true;
                    }
                    else
                    {
                        Debug.Log("You already have full health");
                        return false;
                    }
                }
            case ItemTypeDefinitions.WEAPON:
                {
                    //player.GetComponent<Animator>().Rebind();
                    playerStats.ChangeWeapon(this);
                    return true;
                }
            default:
                return false;
        }
    }
}
