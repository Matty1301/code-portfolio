using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWall : MonoBehaviour
{
    public GameObject blackWolf, whiteWolf;
    private GameObject activeChar;
    private Color color;

    void Start()
    {
        color = Color.white;
    }

    void Update()
    {
        GetActiveChar();
        UpdateAlbedo();
    }

    void GetActiveChar()
    {
        if (blackWolf.activeInHeirarchy)
            activeChar = blackWolf;
        else if (whiteWolf.activeInHeirarchy)
            activeChar = whiteWolf;
    }

    //Calaculates and sets the albedo (transparency) of the game object
    void UpdateAlbedo()
    {
        //The game obejct becomes more opaque as the activeChar gets closer
        if (Mathf.Abs(activeChar.transform.position.x - transform.position.x) <= 50)
            color.a = 1 - (Mathf.Abs(activeChar.transform.position.x - transform.position.x) / 50);

        //Further than 50 (m) the game object is invisible
        else if (color.a != 0)
            color.a = 0;

        //Sets the albedo to the calaculated value
        GetComponent<MeshRenderer>().material.SetColor("_TintColor", color);
    }
}
