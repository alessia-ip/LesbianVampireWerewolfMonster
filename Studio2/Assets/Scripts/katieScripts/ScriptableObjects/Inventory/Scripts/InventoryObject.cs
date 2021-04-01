using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new inventory", menuName = "ScriptableObjects/Inventory")]
public class InventoryObject : ScriptableObject
{
    //list of picked up items
    public List<ItemObject> Container = new List<ItemObject>();

    public void AddItem(ItemObject item)
    {
        Container.Add(item);
    }
}
