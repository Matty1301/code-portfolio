using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.LEGO.Behaviours.Actions;

public class DisableNearbyTrigger : MonoBehaviour
{
    [SerializeField]
    int m_Distance = 5;

    [SerializeField, Tooltip("The list of actions to disable.")]
    private List<Action> actions = new List<Action>();

    float distanceToPlayer;

    void Update()
    {
        distanceToPlayer = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).magnitude;

        if (distanceToPlayer < m_Distance)
        {
            foreach (Action a in actions)
            {
                if (a != null && a.enabled)
                {
                    a.enabled = false;
                }
            }
        }
        else if (distanceToPlayer >= m_Distance)
        {
            foreach (Action a in actions)
            {
                if (a != null && !a.enabled)
                {
                    a.enabled = true;
                }
            }
        }
    }
}
