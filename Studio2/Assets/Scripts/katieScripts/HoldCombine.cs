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
}
