using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InventoryOpen : MonoBehaviour
{
    public GameObject invCanvas;
    public GameObject playerMove;

    public GameObject invButton;
    //public GameObject backButton;

    private void Start()
    {
        invCanvas.SetActive(false);
    }

    public void OpenInv()
    {
        playerMove.SetActive(false);
        invButton.SetActive(false);
        invCanvas.SetActive(true);
    }

    public void BackButton()
    {
        playerMove.SetActive(true);
        invButton.SetActive(true);
        invCanvas.SetActive(false);
    }
    
    public void HoverOffWalk()
    {
        playerMove.SetActive(false);
    }

    public void HoverOnWalk()
    {
        playerMove.SetActive(true);
    }
    

}
