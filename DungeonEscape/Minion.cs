using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.LEGO.Behaviours.Actions;
using Unity.LEGO.Behaviours.Triggers;

public class Minion : MonoBehaviour
{
    private DisableNearbyTrigger disableNearbyTrigger;

    [SerializeField]
    private MoveAction moveAction;

    [SerializeField]
    private LookAtAction lookAtAction;

    private NearbyTrigger nearbyTrigger;

    private bool isSafe = false;

    private Vector3 wizardPos = new Vector3(0, 0, -72.8f);
    private GameObject safePos;

    float distanceToMove = 0;

    void Start()
    {
        disableNearbyTrigger = GetComponent<DisableNearbyTrigger>();

        nearbyTrigger = GetComponentInChildren<NearbyTrigger>();

        safePos = new GameObject();
        safePos.transform.position = wizardPos + new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(0.0f, 1.0f)).normalized * 6.4f;
        safePos.transform.position = new Vector3(safePos.transform.position.x, 0.5f, safePos.transform.position.z);
    }

    void Update()
    {
        if (transform.position.z >= -61.6f)
            return;

        else if (!isSafe)
        {
            if (disableNearbyTrigger.enabled)
            {
                disableNearbyTrigger.enabled = false;
            }
            else
            {
                MakeSafe();
            }
        }

        else if (distanceToMove >= 0.8f && !moveAction.m_Active)
        {
            moveAction.m_Active = true;
            distanceToMove -= 0.8f;
        }

        else if (distanceToMove < 0.8f && !moveAction.m_Active && !lookAtAction.m_Active)
        {
            lookAtAction.m_LookAt = LookAtAction.LookAt.Player;
            lookAtAction.m_Active = true;
        }
    }

    void MakeSafe()
    {
        //disableNearbyTrigger.enabled = false;
        nearbyTrigger.enabled = false;
        moveAction.enabled = true;
        //moveAction.m_Collide = false;

        lookAtAction.m_LookAt = LookAtAction.LookAt.Transform;
        lookAtAction.m_TransformModeTransform = safePos.transform;
        lookAtAction.m_Active = true;

        distanceToMove = (safePos.transform.position - transform.position).magnitude;

        isSafe = true;
    }
}
