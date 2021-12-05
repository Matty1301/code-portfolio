using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.LEGO.Behaviours.Actions;

public class BlinkAndDestroy : MonoBehaviour
{
    private float timeLeft = 4.0f;

    private float blinkPeriod = 0.8f;
    private float blinkFrequency = 0.1f;

    private float timeBlink;

    [SerializeField, Tooltip("If this explode action is triggered then trigger BlinkAndDestroy")]
    private ExplodeAction explodeAction;

    void Start()
    {
        timeLeft += Random.Range(-0.3f, 0.3f);
    }

    void Update()
    {
        if (explodeAction.m_Detonated)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= blinkPeriod)
            {
                if (timeBlink <= 0.0f)
                {
                    timeBlink += blinkFrequency;
                    transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeSelf);
                }

                timeBlink -= Time.deltaTime;
            }

            if (timeLeft <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
