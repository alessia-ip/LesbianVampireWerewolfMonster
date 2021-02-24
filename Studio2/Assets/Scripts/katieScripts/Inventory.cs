using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public  List<Item> invList;
    public List<InvButton> invSlotList;

    [Header("Item Sprites")]
    public Sprite cigarSprite;
    public Sprite bookSprite;
    public Sprite paperSprite;
    //public Sprite randomSprite;

    public string tempHold;
    private string heldItem;
    public string HeldItem
    {
        get { return heldItem; }
        set
        {
            heldItem = value; 
            Debug.Log("Holding: " + heldItem);
        }
    }

    private void Awake()
    {
        instance = this;
        invList = new List<Item>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(invList.Count);
        }
    }

    public void AddItem(Item item)
    {
        invList.Add(item);
        Debug.Log(item.itemType);
        SetInvImage();
    }

    public void SetInvImage()
    {
        int i = invList.Count - 1;
        switch (invList[i].itemType)
        {
            case Item.ItemType.Book:
                invSlotList[i].itemType = InvButton.ItemType.Book;
                invSlotList[i].buttonText.text = "Book";
                Debug.Log("book");
                break;
            case Item.ItemType.Cigar:
                invSlotList[i].itemType = InvButton.ItemType.Cigar;
                invSlotList[i].buttonText.text = "Cigar";
                Debug.Log("cigar");
                break;
            case Item.ItemType.Paper:
                invSlotList[i].itemType = InvButton.ItemType.Paper;
                invSlotList[i].buttonText.text = "Paper";
                Debug.Log("paper");
                break;
            case Item.ItemType.Nothing:
                invSlotList[i].itemType = InvButton.ItemType.Nothing;
                invSlotList[i].buttonText.text = "Nothing";
                Debug.Log("random");
                break;
        }
    }
}
