using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectTrigger : MonoBehaviour
{
    [SerializeField] GameObject objectToMove;
    [SerializeField] AudioClip doorOpenSFX;
    [SerializeField] float distanceToMove;
    [SerializeField] float moveSpeed;

    private const Vector3 startingPos = objectToMove.transform.position;
    private bool triggerActive = false;

    private void Update()
    {
        MoveObject();
    }

    //If the trigger is active move the object
    void MoveObject()
    {
        if (triggerActive && (objectToMove.transform.position.y - startingPos.y).magnitude > distanceToMove)
        {
            objectToMove.transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);

            //If the objectToMove is a MoveablePlatfrom also move the objects on the platform
            if (objectToMove.GetComponent<MoveablePlatform>() != null)
                MoveObjectsOnPlatform();
        }
    }

    //Moves all of the game objects on the platform
    void MoveObjectsOnPlatform()
    {
        if (objectToMove.GetComponent<MoveablePlatform>().onPlatform.Count != 0)
        {
            foreach (GameObject GO in objectToMove.GetComponent<MoveablePlatform>().onPlatform)
            {
                GO.transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
            }
        }
    }

    //Activate the trigger
    private void OnTriggerEnter(Collider other)
    {
        triggerActive = true;
        AudioSource.PlayClipAtPoint(doorOpenSFX, transform.position);
        GetComponent<Animator>().SetBool("triggered", true);
    }

    //Deactivate the trigger
    private void OnTriggerExit(Collider other)
    {
        // If the game object was deactivated do nothing
        if (!other.gameObject.activeInHierarchy)
            return;

        triggerActive = false;
        objectToMove.transform.position = startingPos;
        GetComponent<Animator>().SetBool("triggered", false);
    }
}
