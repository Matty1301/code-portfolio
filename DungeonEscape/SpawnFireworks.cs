using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFireworks : MonoBehaviour
{
    public GameObject fireworks;
    private GameObject firework;

    void Start()
    {
        InvokeRepeating("SpawnFirework", 1, 1);
    }

    void SpawnFirework()
    {
        Vector3 randPos = new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), 40);
        firework = Instantiate(fireworks, this.transform);
        firework.transform.localPosition = randPos;
        Destroy(firework);
    }
}
