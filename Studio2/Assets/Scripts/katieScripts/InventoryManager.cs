﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    [Header("List of ItemObjects that are created")]
    public List<ItemObject> createdObjects;
    [Header("Scriptable Objects for held item and inventory")]
    public HeldItemObject heldItem;

    public InventoryObject inventory;

    [Header("Canvas Variables found in inventory canvas")]
    public GameObject inventoryCanvas;
    
    public Image heldItemInvImage;
    public Image regMenuItemInvImage;


    [HideInInspector]
    public ItemObject currentItem;
    [HideInInspector]
    public GameObject currentItemGameObj;
    
    [Header("Repeat Info used for Items")]
    public GameObject itemDialogueCanvas;
    public GameObject mcSpriteSpot;
    public GameObject itemSpriteSpot;
    public GameObject pickupButton;
    public GameObject combineButton;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public GameObject player;
    public GameObject playerMovement;
    
    //file stuff
    private const string DIR = "/Files";
    private const string FILE_COMBOS = DIR + "/CorrectCombosFile.txt";
    private string FILE_PATH_COMBOS;

    [HideInInspector]
    public List<string> correctComboList;
    [HideInInspector]
    public GameObject currentItemDialogue;
    
    private void Awake()
    {
        instance = this;
        inventoryCanvas.SetActive(true);
    }

    private void Start()
    {
        FILE_PATH_COMBOS = Application.dataPath + FILE_COMBOS;
        SplitComboFile();
        inventoryCanvas.SetActive(false);
        playerMovement.SetActive(true);
    }

    public void SplitComboFile()
    {
        string[] fileData = File.ReadAllLines(FILE_PATH_COMBOS);
        for (int i = 0; i < fileData.Length; i++)
        {
            correctComboList.Add(fileData[i]);
        }
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

    public void ItemDialogueCombineButton()
    {
        string held = heldItem.heldItem.nameOfItem.ToUpper();
        string combine = currentItem.nameOfItem.ToUpper();
        string heldPlusCombine = held + combine;
        string combinePlusHeld = combine + held;

        bool foundCombo = false;
    
        for (int i = 0; i < correctComboList.Count; i++)
        {
            if (heldPlusCombine == correctComboList[i].ToUpper() || combinePlusHeld == correctComboList[i].ToUpper())
            {
                foundCombo = true;
                break;
            }
            else
            {
                foundCombo = false;
            }
        }

        if (foundCombo)
        {
            inventory.Container.Remove(heldItem.heldItem);
            DisplayInventory.instance.UpdateDisplay();
            ResetHeldItemImages();
            for (int i = 0; i < createdObjects.Count; i++)
            {
                if (createdObjects[i].comboParents.ToUpper() == heldPlusCombine ||
                    createdObjects[i].comboParents.ToUpper() == combinePlusHeld)
                {
                    inventory.AddItem(createdObjects[i]);
                    DisplayInventory.instance.UpdateDisplay();
                    break;
                }
            }
            currentItemDialogue.GetComponent<ItemDialogueSOHandler>().currentBlock = currentItemDialogue
                .GetComponent<ItemDialogueSOHandler>().currentBlock.nextLine;
            currentItemDialogue.GetComponent<ItemDialogueSOHandler>().DialogueUpdate();
        }
        else
        {
            currentItemDialogue.GetComponent<ItemDialogueSOHandler>().currentBlock = currentItemDialogue
                .GetComponent<ItemDialogueSOHandler>().currentBlock.nextLineWrong;
            currentItemDialogue.GetComponent<ItemDialogueSOHandler>().DialogueUpdate();
        }
    }

    public void ResetHeldItemImages()
    {
        heldItem.heldItem = null;
        heldItemInvImage.sprite = null;
        regMenuItemInvImage.sprite = null;
    }
}
