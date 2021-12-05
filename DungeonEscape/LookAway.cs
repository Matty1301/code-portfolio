using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAway : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.position = transform.parent.position - (player.transform.position - transform.parent.position).normalized;
    }
}
