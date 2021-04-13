using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public HeldItemObject heldItem;

    public InventoryObject inventory;

    public GameObject inventoryCanvas;
    
    public Image heldItemInvImage;

    [HideInInspector]
    public ItemObject currentItem;
    [HideInInspector]
    public GameObject currentItemGameObj;
    
    [FormerlySerializedAs("dialogueCanvas")] [Header("Repeat Info used for Items")]
    public GameObject itemDialogueCanvas;
    public GameObject mcSpriteSpot;
    public GameObject itemSpriteSpot;
    public GameObject pickupButton;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public GameObject player;
    public GameObject playerMovement;
    
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
        itemDialogueCanvas.SetActive(false);
        playerMovement.SetActive(true);
        Destroy(currentItemGameObj);
    }
}
