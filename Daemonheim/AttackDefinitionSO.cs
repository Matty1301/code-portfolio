using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack.asset", menuName = "Attack/BaseAttack")]
public class AttackDefinitionSO : ScriptableObject
{
    public GameObject weaponPrefab;

    public float cooldown;
    public float range;
    public float minDamage;
    public float maxDamage;
    public float criticalMulitiplier;
    public float criticalChance;

    private float coreDamage;
    private bool isCritical;

    public Attack CreateAttack(CharacterStats wielderStats, CharacterStats defenderStats)
    {
        if (wielderStats != null)
            coreDamage = wielderStats.GetDamage();
        else
            coreDamage = 0;
        coreDamage += Random.Range(minDamage, maxDamage);

        isCritical = Random.value < criticalChance;
        if (isCritical)
            coreDamage *= criticalMulitiplier;

        if (defenderStats != null)
            coreDamage -= defenderStats.GetResistance();

        return new Attack((int)coreDamage, isCritical);
    }
}
