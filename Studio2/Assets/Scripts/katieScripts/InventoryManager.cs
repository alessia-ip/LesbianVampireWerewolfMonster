using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public HeldItemObject heldItem;

    public InventoryObject inventory;

    public GameObject inventoryCanvas;

    public GameObject playerMovement;

    public Image heldItemInvImage;

    public GameObject itemCanvas;
    
    [HideInInspector]
    public ItemObject currentItem;
    [HideInInspector]
    public GameObject currentItemGameObj;

    private void Awake()
    {
        instance = this;
        inventoryCanvas.SetActive(true);
    }

    private void Start()
    {
        inventoryCanvas.SetActive(false);
        playerMovement.SetActive(true);
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }

    public void OpenInventory()
    {
        inventoryCanvas.SetActive(true);
        playerMovement.SetActive(false);
    }

    public void CloseInventory()
    {
        inventoryCanvas.SetActive(false);
        playerMovement.SetActive(true);
    }

    public void PickUpButton()
    {
        inventory.AddItem(currentItem);
        DisplayInventory.instance.UpdateDisplay();
        itemCanvas.SetActive(false);
        playerMovement.SetActive(true);
        Destroy(currentItemGameObj);
    }
}
