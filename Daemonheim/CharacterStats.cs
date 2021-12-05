using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterStatsSO characterDefinition_Template;
    public CharacterStatsSO characterDefinition { get;  private set; }
    public GameObject characterWeaponSlot;
    private CharacterInventory charInv;
    private Controller controller;

    #region Constructors
    public CharacterStats()
    {
        charInv = CharacterInventory.instance;
    }
    #endregion

    #region Initializations
    private void Awake()
    {
        if (characterDefinition_Template != null)
        {
            characterDefinition = Instantiate(characterDefinition_Template);
        }

        controller = GetComponent<Controller>();
    }
    #endregion

    #region Stat Increasers
    public void GainHealth(int healthGained)
    {
        characterDefinition.GainHealth(healthGained);
    }

    public void GainExperience(int experienceGained)
    {
        characterDefinition.GainExperience(experienceGained);
    }
    #endregion

    #region Stat Decreasers
    public void LoseHealth(int healthLost)
    {
        characterDefinition.LoseHealth(healthLost);

        if (GetHealth() <= 0)
        {
            controller.Death();
        }
    }
    #endregion

    #region Weapon Change
    public void ChangeWeapon(ItemPickupSO weaponPickup)
    {
        if (!characterDefinition.UnequipWeapon(weaponPickup, charInv, characterWeaponSlot))
        {
            characterDefinition.EquipWeapon(weaponPickup, charInv, characterWeaponSlot);
        }
        controller.UpdateWeapon();
    }

    public void EquipWeapon(ItemPickupSO weaponPickup)
    {
        characterDefinition.EquipWeapon(weaponPickup, charInv, characterWeaponSlot);

        controller.UpdateWeapon();
    }
    #endregion

    #region Reporters
    public int GetHealth()
    {
        return characterDefinition.currentHealth;
    }

    public GameObject GetCurrentWeapon()
    {
        return characterDefinition.newWeapon;
    }

    public AttackDefinitionSO GetCurrentAttack()
    {
        return characterDefinition.weapon;
    }

    public int GetDamage()
    {
        return characterDefinition.currentDamage;
    }

    public int GetResistance()
    {
        return characterDefinition.currentResistance;
    }

    public int GetExperience()
    {
        return characterDefinition.XP;
    }
    #endregion
}
