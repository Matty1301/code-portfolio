using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePlatform : MonoBehaviour
{
    public List<GameObject> onPlatform { get; private set; } //Needs to be accessible by MoveObjectTrigger.cs

    void Start()
    {
        onPlatform = new List<GameObject>();
    }

    //When a game object enters the platfrom add it to the list
    private void OnCollisionEnter(Collision collision)
    {
        //Check if the object is already in the list before adding it
        if (!onPlatform.Contains(collision.gameObject))
        {
            onPlatform.Add(collision.gameObject);
        }
    }

    //When a game object leaves the platfrom remove it from the list
    private void OnCollisionExit(Collision collision)
    {
        // If the game object was deactivated do nothing
        if (!collision.gameObject.activeInHierarchy)
            return;

        //Check that the object is still in the list before attempting to remove it
        if (onPlatform.Contains(collision.gameObject))
        {
            onPlatform.Remove(collision.gameObject);
        }
    }
}
