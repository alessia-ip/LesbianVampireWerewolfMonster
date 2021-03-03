using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvButton : MonoBehaviour
{
    public enum ItemType
    {
        Cigar, Book, Paper, Nothing
    }
    public ItemType itemType = ItemType.Nothing;
    
    public void SlotClick()
    {
        switch (itemType)
        {
            case ItemType.Cigar:
                Inventory.instance.HeldItem = "Cigar";
                break;
            case ItemType.Book:
                Inventory.instance.HeldItem = "Book";
                break;
            case ItemType.Paper:
                Inventory.instance.HeldItem = "Paper";
                break;
            case ItemType.Nothing:
                Inventory.instance.HeldItem = "Nothing";
                break;
        }

        Inventory.instance.heldImage.sprite = this.GetComponentInParent<Image>().sprite;
    }
}