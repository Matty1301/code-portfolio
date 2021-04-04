using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portcullis : Door
{
    public AudioClip onApproach;
    public AudioClip onOpen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && source.time == 0)  //Check that an audio clip is not playing already
        {
            source.clip = onApproach;
            source.Play();
        }
    }

    public override void OnInteraction()
    {
        if (!isOpen)
        {
            isOpen = true;
            WaitForSeconds(0.5f);
            anim.Play();
            source.clip = onOpen;
            source.Play();
        }
    }

    IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
