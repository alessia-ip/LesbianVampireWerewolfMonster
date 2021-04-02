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

    private void Awake()
    {
        instance = this;
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
}
