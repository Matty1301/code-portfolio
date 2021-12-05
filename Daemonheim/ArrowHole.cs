using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHole : MonoBehaviour
{
    public AttackDefinitionSO attack;

    public void Fire()
    {
        ((Arrow)attack).Fire(gameObject);
    }
}
