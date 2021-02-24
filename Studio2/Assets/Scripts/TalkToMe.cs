using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkToMe : MonoBehaviour
{
    private bool playerCloseEnough = false;

    private string playerTag = "Player";

    
    //TODO make these instances later. This was just easier for now lol
    public GameObject dialogueCanvas;
    public GameObject movementScript;


    private void OnMouseDown()
    {
        if (playerCloseEnough == true)
        {
            movementScript.SetActive(false);
            dialogueCanvas.SetActive(true);
            ContextCursorScript.instance.Talk();

        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.gameObject.tag;
        if (player == playerTag)
        {
            Debug.Log("COLLISION");
            playerCloseEnough = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var player = other.gameObject.tag;
        if (player == playerTag)
        {
            playerCloseEnough = false;
        }
    }
}