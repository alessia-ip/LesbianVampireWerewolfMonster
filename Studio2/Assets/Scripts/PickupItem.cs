using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private bool playerCloseEnough = false;

    private string playerTag = "Player";

    private void OnMouseDown()
    {
        if (playerCloseEnough == true)
        {
            this.gameObject.SetActive(false);
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
