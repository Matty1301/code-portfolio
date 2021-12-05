using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructedAwardExperience : MonoBehaviour, IDestructible
{
    public void OnDestrcution(GameObject destroyer)
    {
        int XP = GetComponent<CharacterStats>().GetExperience();
        destroyer.GetComponent<CharacterStats>().GainExperience(XP);
    }
}
