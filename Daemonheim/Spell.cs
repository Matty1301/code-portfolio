﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell.asset", menuName = "Attack/Spell")]
public class Spell : AttackDefinitionSO
{
    public Projectile projectileToFire;
    public float projectileSpeed;

    public void Cast(GameObject Caster, Vector3 HotSpot)
    {
        Projectile projectile = Instantiate(projectileToFire, HotSpot, Caster.transform.rotation);
        projectile.Fire(Caster, projectileSpeed, range);

        //projectile.gameObject.layer = LayerMask.NameToLayer("Projectile");

        projectile.ProjectileCollided += OnProjectileCollided;
    }

    private void OnProjectileCollided(GameObject Caster, GameObject Target)
    {
        if (Caster == null || Target == null)
            return;

        var casterStats = Caster.GetComponent<CharacterStats>();
        var targetStats = Target.GetComponent<CharacterStats>();

        var attack = CreateAttack(casterStats, targetStats);

        var attackables = Target.GetComponentsInChildren(typeof(IAttackable));
        foreach(IAttackable a in attackables)
        {
            a.OnAttack(Caster, attack);
        }
    }
}
