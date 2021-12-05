using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class AttackedTakeDamage : MonoBehaviour, IAttackable
{
    private CharacterStats stats;

    private void Awake()
    {
        stats = GetComponent<CharacterStats>();
    }

    public void OnAttack(GameObject attacker, Attack attack)
    {
        if (attacker.tag == "Player")
            StatsTracker.Instance.damageDealt += attack.Damage;

        stats.LoseHealth(attack.Damage);

        if (stats.GetHealth() <= 0)
        {
            //if (attacker.GetComponent<CharacterStats>() != null)
            //attacker.GetComponent<CharacterStats>().GainExperience(stats.GetExperience());

            if (attacker.tag == "Player")
                StatsTracker.Instance.enemiesKilled++;

            var destructibles = GetComponents(typeof(IDestructible));
            foreach (IDestructible d in destructibles)
            {
                d.OnDestrcution(attacker);
            }
        }
    }
}
