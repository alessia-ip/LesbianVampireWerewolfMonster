using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    public static DisplayInventory instance;
    private InventoryObject inventory;

    public int xSpaceBetweenItems;
    public int ySpaceBetweenItems;
    public int numberOfColumns;
    public int xStart;
    public int yStart;

    private Dictionary<ItemObject, GameObject> itemDisplayed = new Dictionary<ItemObject, GameObject>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        inventory = InventoryManager.instance.inventory;
        CreateDisplay();
    }

    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            var obj = Instantiate(inventory.Container[i].canvasImage, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
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
                var obj = Instantiate(inventory.Container[i].canvasImage, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
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
