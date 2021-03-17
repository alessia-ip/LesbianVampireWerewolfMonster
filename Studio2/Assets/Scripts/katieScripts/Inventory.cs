using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public  List<string> invList;
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
        invList = new List<string>();
    }

    public void AddItem(string itemTypeForList)
    {
        invList.Add(itemTypeForList);
        SetInvImage();
    }

    public void SetInvImage()
    {
        int i = invList.Count - 1;
        switch (invList[i])
        {
            case "Book":
                invSlotList[i].GetComponent<InvButton>().itemType = "Book";
                invSlotList[i].GetComponent<Image>().sprite = bookSprite;
                break;
            case "Cigar":
                invSlotList[i].GetComponent<InvButton>().itemType = "Cigar";
                invSlotList[i].GetComponent<Image>().sprite = cigarSprite;
                break;
            case "Paper":
                invSlotList[i].GetComponent<InvButton>().itemType = "Paper";
                invSlotList[i].GetComponent<Image>().sprite = paperSprite;
                break;
        }
    }
}