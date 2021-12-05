using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private Door interactable = default;
    [SerializeField] private GameObject lights = default;
    private Animation anim;
    private AudioSource source;
    private bool pressed;

    private void Awake()
    {
        anim = GetComponent<Animation>();
        source = GetComponent<AudioSource>();
        pressed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!pressed)
        {
            anim.Play();
            source.Play();
            interactable.OnInteraction();
            lights.SetActive(true);
            pressed = true;
        }
    }
}
