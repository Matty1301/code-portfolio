using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityDrone : MonoBehaviour
{
    private Collider[] colliders;

    void Start()
    {
        colliders = GetComponentsInChildren<Collider>();
    }

    void Update()
    {
        foreach (Collider c in colliders)
        {
            c.tag = "Enemy";
        }
    }
}
