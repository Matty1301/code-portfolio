using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject caster;
    private Collider[] colliders;
    private float speed;
    private float range;

    private float distanceTravelled;

    public event Action<GameObject, GameObject> ProjectileCollided;

    public void Fire(GameObject Caster, float Speed, float Range)
    {
        caster = Caster;
        speed = Speed;
        range = Range;

        distanceTravelled = 0f;

        colliders = caster.GetComponentsInChildren<Collider>();
        for (int i = 0; i < colliders.Length; i++)
            Physics.IgnoreCollision(colliders[i], GetComponent<Collider>());
    }

    void Update()
    {
        float distanceToTravel = speed * Time.deltaTime;

        transform.Translate(Vector3.forward * distanceToTravel);

        distanceTravelled += distanceToTravel;
        if (distanceTravelled > range)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (ProjectileCollided != null)
        {
            ProjectileCollided(caster, collision.gameObject);
        }
        Destroy(gameObject);
    }
}
