using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructedDestroyObject : MonoBehaviour, IDestructible
{
    public void OnDestrcution(GameObject destroyer)
    {
        Destroy(gameObject);
    }
}
