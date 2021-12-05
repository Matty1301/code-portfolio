using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Puzzle : MonoBehaviour
{
    private bool trapIsActive;
    public int keyPlate = -1;
    public int disarmedByKeyPlate = -1;
    private int lastKeyPlate;

    private GameObject[] pillars;
    private ArrowHole[] arrowHoles;

    private void Awake()
    {
        trapIsActive = true;
        lastKeyPlate = 0;

        pillars = GameObject.FindGameObjectsWithTag("Puzzle");
        arrowHoles = GameObject.FindObjectsOfType<ArrowHole>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (keyPlate == lastKeyPlate + 1)
        {
            GetComponent<AudioSource>().Play();
            for (int i = 0; i < pillars.Length; i++)
            {
                pillars[i].GetComponent<Puzzle>().DisarmTrap(keyPlate);
            }
        }
        else if (trapIsActive)
        {
            FireArrows();
        }
    }

    private void FireArrows()
    {
        for (int i = 0; i < arrowHoles.Length; i++)
        {
            arrowHoles[i].Fire();
        }
    }

    private void DisarmTrap(int keyPlate)
    {
        if (keyPlate == disarmedByKeyPlate)
            trapIsActive = false;
        lastKeyPlate++;
    }
}