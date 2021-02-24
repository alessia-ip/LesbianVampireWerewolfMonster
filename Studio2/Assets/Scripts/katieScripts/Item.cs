using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Cigar, Book, Paper, Nothing
    }

    public ItemType itemType = ItemType.Nothing;
    public void SendToInv()
    {
        Inventory.instance.AddItem(gameObject.GetComponent<Item>());
    }

    private void OnMouseDown()
    {
        SendToInv();
        Destroy(gameObject);
    }
}
