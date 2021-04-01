using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "HeldItem", menuName = "ScriptableObjects/Item/HeldItem")]
public class HeldItemObject : ScriptableObject
{
    public ItemObject heldItem;
}
