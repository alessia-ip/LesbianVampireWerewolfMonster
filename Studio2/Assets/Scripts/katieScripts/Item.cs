using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemObject item;

    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
        sprite.sprite = item.itemSprite;
    }

    private void OnMouseDown()
    {
        InventoryManager.instance.inventory.AddItem(item);
        DisplayInventory.instance.UpdateDisplay();
        Destroy(gameObject);
    }
}
