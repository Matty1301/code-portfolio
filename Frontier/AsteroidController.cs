using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour, PooledObjectController
{
    private Rigidbody2D rigidBody;
    private Collider2D playerForceFieldCollider;

    private float gameBoundX;
    private float gameBoundY;

    private float maxVelocity = 1;


    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerForceFieldCollider = GameObject.Find("Player").transform.Find("ForceField").GetComponent<Collider2D>();

        gameBoundX = GameManager.Instance.gameBoundX;
        gameBoundY = GameManager.Instance.gameBoundY;

        OnSpawnObject();
    }

    public void OnSpawnObject()
    {
        rigidBody.velocity = new Vector2(Random.Range(-maxVelocity, maxVelocity), Random.Range(-maxVelocity, maxVelocity));
        rigidBody.angularVelocity = Random.Range(-360, 360);
    }

    private void Update()
    {
        //Once object has gone fully off screen deactivate it so that it can be reused
        if (Mathf.Abs(transform.position.x) > gameBoundX + 1
            || Mathf.Abs(transform.position.y) > gameBoundY + 1)    //Pad 1 to ensure objects are fully off screen before being deactivated
            gameObject.SetActive(false);
    }

    //If the object enters the player's force field launch the object away from the player
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision = playerForceFieldCollider)
        {
            Vector2 direction = (transform.position - playerForceFieldCollider.transform.position).normalized;
            rigidBody.AddForce(direction * 0.5f, ForceMode2D.Impulse);
        }
    }
}
