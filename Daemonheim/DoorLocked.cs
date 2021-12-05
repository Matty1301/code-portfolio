using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLocked : MonoBehaviour, IInteractable
{
    protected AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public virtual void OnInteraction()
    {
        source.Play();
    }
}
