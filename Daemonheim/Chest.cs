using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    private bool isOpen;
    private SpawnItem spawner;
    Animation anim;
    GameObject itemPickup;
    AudioSource source;

    private void Awake()
    {
        isOpen = false;
        spawner = GetComponent<SpawnItem>();
        anim = GetComponent<Animation>();
        source = GetComponent<AudioSource>();
    }

    public void OnInteraction()
    {
        if (!isOpen)
        {
            isOpen = true;
            anim.Play();
            source.Play();
            itemPickup = spawner.CreateSpawn();
        }
    }
}
