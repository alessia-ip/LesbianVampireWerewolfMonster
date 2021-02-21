using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Inventory inventory;
    public enum ItemType
    {
        Cigar, Book, Paper, Random
    }

    public ItemType itemType = ItemType.Random;
    public void SendToInv()
    {
        inventory.AddItem(gameObject.GetComponent<Item>());
    }

    private void OnMouseDown()
    {
        SendToInv();
        Destroy(gameObject);
    }
}
