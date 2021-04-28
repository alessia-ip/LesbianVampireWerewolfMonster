using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldCombine : MonoBehaviour
{
    public GameObject holdButton;
    public GameObject combineButton;

    public ItemObject itemInfo;

    private void Start()
    {
        holdButton.SetActive(false);
        combineButton.SetActive(false);
    }

    public void OnHove()
    {
        holdButton.SetActive(true);
        combineButton.SetActive(true);
    }

    public void OffHover()
    {
        holdButton.SetActive(false);
        combineButton.SetActive(false);
    }

    public void HoldClick()
    {
        InventoryManager.instance.heldItem.heldItem = itemInfo;
        InventoryManager.instance.heldItemInvImage.sprite = InventoryManager.instance.heldItem.heldItem.itemSprite;
        InventoryManager.instance.regMenuItemInvImage.sprite = InventoryManager.instance.heldItem.heldItem.itemSprite;
    }

    public void CombineClick()
    {
        string held = InventoryManager.instance.heldItem.heldItem.nameOfItem.ToUpper();
        string combine = itemInfo.nameOfItem.ToUpper();
        string heldPlusCombine = held + combine;
        string combinePlusHeld = combine + held;

        bool foundCombo = false; 
        for (int i = 0; i < InventoryManager.instance.correctComboList.Count; i++)
        {
            if (heldPlusCombine == InventoryManager.instance.correctComboList[i].ToUpper() || combinePlusHeld == InventoryManager.instance.correctComboList[i].ToUpper())
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
            InventoryManager.instance.inventory.Container.Remove(InventoryManager.instance.heldItem.heldItem);
            DisplayInventory.instance.UpdateDisplay();
            InventoryManager.instance.ResetHeldItemImages();
            for (int i = 0; i < InventoryManager.instance.createdObjects.Count; i++)
            {
                if (InventoryManager.instance.createdObjects[i].comboParents.ToUpper() == heldPlusCombine ||
                    InventoryManager.instance.createdObjects[i].comboParents.ToUpper() == combinePlusHeld)
                {
                    InventoryManager.instance.inventory.AddItem(InventoryManager.instance.createdObjects[i]);
                    DisplayInventory.instance.UpdateDisplay();
                    break;
                }
            }
        }
        else
        {
            //do nothing
        }
    }
}
