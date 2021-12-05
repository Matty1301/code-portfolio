using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedPlaySound : MonoBehaviour, IAttackable
{
    private AudioSource source;
    public AudioClip clip;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void OnAttack(GameObject attacker, Attack attack)
    {
        source.clip = clip;
        source.Play();
        //SoundManager.Instance.PlaySoundEffect(SoundEffect.MobDamage);
    }
}
