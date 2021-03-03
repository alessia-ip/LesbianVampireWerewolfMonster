using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public  List<Item> invList;
    public List<GameObject> invSlotList;

    [Header("Item Sprites")]
    public Sprite cigarSprite;
    public Sprite bookSprite;
    public Sprite paperSprite;

    public Image heldImage;
    private string heldItem;
    public string HeldItem
    {
        get { return heldItem; }
        set
        {
            heldItem = value; 
            
        }
    }

    private void Awake()
    {
        instance = this;
        invList = new List<Item>();
    }

    public void AddItem(Item item)
    {
        invList.Add(item);
        SetInvImage();
    }

    public void SetInvImage()
    {
        int i = invList.Count - 1;
        switch (invList[i].itemType)
        {
            case Item.ItemType.Book:
                invSlotList[i].GetComponent<InvButton>().itemType = InvButton.ItemType.Book;
                invSlotList[i].GetComponent<Image>().sprite = bookSprite;
                break;
            case Item.ItemType.Cigar:
                invSlotList[i].GetComponent<InvButton>().itemType = InvButton.ItemType.Cigar;
                invSlotList[i].GetComponent<Image>().sprite = cigarSprite;
                break;
            case Item.ItemType.Paper:
                invSlotList[i].GetComponent<InvButton>().itemType = InvButton.ItemType.Paper;
                invSlotList[i].GetComponent<Image>().sprite = paperSprite;
                break;
            case Item.ItemType.Nothing:
                invSlotList[i].GetComponent<InvButton>().itemType = InvButton.ItemType.Nothing;
                invSlotList[i].GetComponent<Image>().sprite = null;
                break;
        }
    }
}