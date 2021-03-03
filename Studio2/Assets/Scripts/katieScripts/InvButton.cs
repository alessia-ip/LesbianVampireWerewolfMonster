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

    public bool holdButton = false;
    public Text buttonText;

    private void Start()
    {
        if (holdButton)
        {
            buttonText.text = "HOLD";
        }
        else
        {
            buttonText.text = "Nothing";
        }

    }

    public void SlotClick()
    {
        switch (itemType)
        {
            case ItemType.Cigar:
                Inventory.instance.tempHold = "Cigar";
                Debug.Log("cigar");
                break;
            case ItemType.Book:
                Inventory.instance.tempHold = "Book";
                Debug.Log("book");
                break;
            case ItemType.Paper:
                Inventory.instance.tempHold = "Paper";
                Debug.Log("paper");
                break;
            case ItemType.Nothing:
                Inventory.instance.tempHold = "Nothing";
                Debug.Log("nothing");
                break;
        }
    }

    public void HoldButton()
    {
        Inventory.instance.HeldItem = Inventory.instance.tempHold;
    }
}
