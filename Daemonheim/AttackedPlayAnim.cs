using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedPlayAnim : MonoBehaviour, IAttackable
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OnAttack(GameObject attacker, Attack attack)
    {
        anim.SetTrigger("Impact");
    }
}
