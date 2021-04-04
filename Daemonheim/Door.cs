using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    protected Animation anim;
    protected AudioSource source;
    protected bool isOpen;

    private void Awake()
    {
        anim = GetComponent<Animation>();
        source = GetComponent<AudioSource>();
        isOpen = false;
    }

    public virtual void OnInteraction()
    {
        if (!isOpen)
        {
            anim.Play();
            source.Play();
            isOpen = true;
        }
    }
}
