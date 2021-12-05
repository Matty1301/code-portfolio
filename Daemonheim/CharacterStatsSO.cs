using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewStats", menuName = "Character/Stats", order = 1)]
public class CharacterStatsSO : ScriptableObject
{
    public Events.EventIntegerEvent OnLevelUp;
    public Events.EventIntegerEvent OnPlayerLostHealth;
    public Events.EventIntegerEvent OnPlayerGainedHealth;
    public UnityEvent OnPlayerDeath;
    public UnityEvent OnPlayerInitialized;

    [System.Serializable]
    public class CharLevel
    {
        public int charLevel;
        public int maxHealth;
        public int baseDamage;
        public int baseResistance;
        public int requiredXP;
    }

    #region Fields
    public bool isPlayer = false;

    public AttackDefinitionSO weapon { get; private set; }
    public GameObject newWeapon { get; private set; }

    public int maxHealth;
    public int currentHealth;
    public int baseDamage;
    public int currentDamage;
    public int baseResistance;
    public int currentResistance;
    public int XP;
    public int charLevel;
    public int requiredXP;

    public CharLevel[] charLevels;
    #endregion

    #region Stat Increasers
    public void GainHealth(int healthGained)
    {
        healthGained = Mathf.Min(healthGained, maxHealth - currentHealth);
        currentHealth += healthGained;
        StatsTracker.Instance.healthGained += healthGained;

        if (isPlayer)
        {
            OnPlayerGainedHealth.Invoke(healthGained);
        }
    }

    public void GainExperience(int experienceGained)
    {
        XP += experienceGained;

        if (charLevel < charLevels.Length)
        {
            if (XP >= charLevels[charLevel].requiredXP)
            {
                LevelUp();
            }
        }
    }

    public void EquipWeapon(ItemPickupSO weaponPickup, CharacterInventory charInv, GameObject weaponSlot)
    {
        if (newWeapon == null)
        {
            weapon = weaponPickup.weaponSlotObject;
            newWeapon = Instantiate(weaponPickup.weaponSlotObject.weaponPrefab, weaponSlot.transform);
            currentDamage = baseDamage + weaponPickup.itemAmount;
        }
    }
    #endregion

    #region Stat Decreasers
    public void LoseHealth(int healthLost)
    {
        currentHealth -= healthLost;

        if (isPlayer)
        {
            OnPlayerLostHealth.Invoke(healthLost);
        }

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public bool UnequipWeapon(ItemPickupSO weaponPickup, CharacterInventory charInv, GameObject weaponSlot)
    {
        bool previousWeaponSame = false;
        if (weapon != null)
        {
            if (weapon == weaponPickup.weaponSlotObject)
            {
                previousWeaponSame = true;
            }
            else
            {
                //Destroy(weaponSlot.transform.GetChild(0).gameObject);
                Destroy(newWeapon);
                newWeapon = null;
                weapon = null;
                currentDamage = baseDamage;
            }
        }
        return previousWeaponSame;
    }
    #endregion

    #region Character Level Up and Death
    private void Death()
    {
        if (isPlayer)
        {
            OnPlayerDeath.Invoke();
        }
    }

    private void LevelUp()
    {
        charLevel = charLevels[charLevel - 1].charLevel;

        int healthLost = maxHealth - currentHealth;
        maxHealth = charLevels[charLevel - 1].maxHealth;
        currentHealth = maxHealth - healthLost;

        int damageBonus = currentDamage - baseDamage;
        baseDamage = charLevels[charLevel - 1].baseDamage;
        currentDamage = baseDamage + damageBonus;

        baseResistance = charLevels[charLevel - 1].baseResistance;
        currentResistance = baseResistance;

        requiredXP = charLevels[charLevel - 1].requiredXP;

        //Trigger the OnLevelUp event
        OnLevelUp.Invoke(charLevel);
    }
    #endregion
}
