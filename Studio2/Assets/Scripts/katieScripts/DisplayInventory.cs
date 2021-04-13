using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public static DisplayInventory instance;
    public InventoryObject inventory;

    public int xSpaceBetweenItems;
    public int ySpaceBetweenItems;
    public int numberOfColumns;
    public int xStart;
    public int yStart;

    public GameObject invItemPrefab;

    private Dictionary<ItemObject, GameObject> itemDisplayed = new Dictionary<ItemObject, GameObject>();

    private void Awake()
    {
        instance = this;
        CreateDisplay();
    }

    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            var obj = Instantiate(invItemPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponent<Image>().sprite = inventory.Container[i].itemSprite;
            obj.GetComponent<HoldCombine>().itemInfo = inventory.Container[i];
            itemDisplayed.Add(inventory.Container[i], obj);
        }
    }
    public void UpdateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (itemDisplayed.ContainsKey(inventory.Container[i]))
            {
                //do nothing
            }
            else
            {
                var obj = Instantiate(invItemPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponent<Image>().sprite = inventory.Container[i].itemSprite;
                obj.GetComponent<HoldCombine>().itemInfo = inventory.Container[i];
                itemDisplayed.Add(inventory.Container[i], obj);
            }
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(xStart + (xSpaceBetweenItems * (i % numberOfColumns)),
            yStart + (-ySpaceBetweenItems * (i / numberOfColumns)), 0f);
    }
}
