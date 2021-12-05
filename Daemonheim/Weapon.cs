using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon.asset", menuName = "Attack/Weapon")]
public class Weapon : AttackDefinitionSO
{
    private string defenderLayerName;

    public void Swing(GameObject wielder, Vector3 HotSpot)
    {
        if (wielder.layer == LayerMask.NameToLayer("Player"))
            defenderLayerName = "Enemy";
        else
            defenderLayerName = "Player";

        var collidedObjects = Physics.OverlapSphere(HotSpot, 1.5f, 1 << LayerMask.NameToLayer(defenderLayerName));

        foreach (var collision in collidedObjects)
        {
            var collisionGo = collision.gameObject;

            //if (Physics.GetIgnoreLayerCollision(LayerMask.NameToLayer("Weapons"), collisionGo.layer))
            //{
            //    continue;
            //}

            if (collisionGo == wielder)
            {
                continue;
            }

            ExecuteAttack(wielder, collisionGo);
        }
    }

    public void ExecuteAttack(GameObject attacker, GameObject defender)
    {
        if (defender == null)
            return;

        var attackerStats = attacker.GetComponent<CharacterStats>();
        var defenderStats = defender.GetComponent<CharacterStats>();

        var attack = CreateAttack(attackerStats, defenderStats);

        var attackables = defender.GetComponentsInChildren(typeof(IAttackable));
        foreach (IAttackable a in attackables)
        {
            a.OnAttack(attacker, attack);
        }
    }
}
