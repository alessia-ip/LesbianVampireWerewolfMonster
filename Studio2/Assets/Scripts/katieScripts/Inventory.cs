using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static List<Item> invList;
    public List<Image> invSlotList;
    
    [Header("Item Sprites")]
    public Sprite cigarSprite;
    public Sprite bookSprite;
    public Sprite paperSprite;
    //public Sprite randomSprite;

    private void Awake()
    {
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
                invSlotList[i].enabled = true;
                invSlotList[i].sprite = bookSprite;
                Debug.Log("book");
                break;
            case Item.ItemType.Cigar:
                invSlotList[i].enabled = true;
                invSlotList[i].sprite = cigarSprite;
                Debug.Log("cigar");
                break;
            case Item.ItemType.Paper:
                invSlotList[i].enabled = true;
                invSlotList[i].sprite = paperSprite;
                Debug.Log("paper");
                break;
            case Item.ItemType.Random:
                invSlotList[i].enabled = false;
                Debug.Log("random");
                break;
        }
    }
}
