using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public HeldItemObject heldItem;

    public InventoryObject inventory;

    public GameObject inventoryCanvas;

    private void Awake()
    {
        instance = this;
        inventoryCanvas.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }

    public void OpenIventory()
    {
        inventoryCanvas.SetActive(true);
    }

    public void CloseInventory()
    {
        inventoryCanvas.SetActive(false);
    }
}
