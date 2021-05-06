using System;
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
    
    //[HideInInspector]
    public List<string> correctComboList;
    [HideInInspector]
    public GameObject currentItemDialogue;
    
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
    public GameObject Akari;
    
    //file stuff
    /*private const string DIR = "/Resources";
    private const string FILE_COMBOS = DIR + "/CorrectCombosFile.txt";
    private string FILE_PATH_COMBOS;
    private TextAsset FILE_COMBOS;*/
    
    private void Awake()
    {
        instance = this;
        inventoryCanvas.SetActive(true);
    }

    private void Start()
    {
        //FILE_PATH_COMBOS = Application.dataPath + FILE_COMBOS;
        //FILE_COMBOS = Resources.Load<TextAsset>("CorrectCombosFile") as TextAsset;
        SetCorrectComboList();
        inventoryCanvas.SetActive(false);
        playerMovement.SetActive(true);
    }

    public void SetCorrectComboList()
    {
        //string[] fileData = FILE_COMBOS.text.Split('\n');
        
        //Debug.Log(fileData[1]);
        //Debug.Log(fileData[0]);
        //Debug.Log(fileData[2]);
        //string[] fileData = File.ReadAllLines(FILE_PATH_COMBOS);
        // for (int i = 0; i < fileData.Length; i++)
        // {
        //     Debug.Log("Item " + i);
        //     correctComboList.Add(fileData[i].ToUpper());
        // }

        // correctComboList[0] = "RubyMP".ToUpper();
        // correctComboList[1] = "RoseVial".ToUpper();
        // correctComboList[2] = "RubyPowderBloodVial".ToUpper();
        // correctComboList[3] = "CigarFireplace".ToUpper();
        // correctComboList[4] = "LitCigarBlankPaper".ToUpper();

        for (int i = 0; i < createdObjects.Count; i++)
        {
            correctComboList.Add(createdObjects[i].comboParents.ToUpper());
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
        Akari.GetComponent<Collider2D>().enabled = false;
    }

    public void CloseInventory()
    {
        inventoryCanvas.SetActive(false);
        playerMovement.SetActive(true);
        Akari.GetComponent<Collider2D>().enabled = true;
    }

    public void PickUpButton()
    {
        inventory.AddItem(currentItem);
        DisplayInventory.instance.UpdateDisplay();
        itemDialogueCanvas.SetActive(false);
        playerMovement.GetComponent<testing>().enabled = true;
        Destroy(currentItemGameObj);
    }

    public void ItemDialogueCombineButton()
    {
        if (heldItem.heldItem != null)
        {
            string held = heldItem.heldItem.nameOfItemNoSpaces.ToUpper();
            string combine = currentItem.nameOfItemNoSpaces.ToUpper();
            string heldPlusCombine = held + combine;
            string combinePlusHeld = combine + held;
            Debug.Log(heldPlusCombine);
            Debug.Log(combinePlusHeld);
            Debug.Log(correctComboList[0].ToUpper());

            bool foundCombo = false;

            for (int i = 0; i < correctComboList.Count; i++)
            {
                if (heldPlusCombine == correctComboList[i].ToUpper() ||
                    combinePlusHeld == correctComboList[i].ToUpper())
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
