using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Item/BasicItem")]
public class ItemObject : ScriptableObject
{
    public string nameOfItemNoSpaces;
    public string nameOfItemInGame;
    public Sprite itemSprite;//for future sprite
    [Header("If item is created from combine input combine parents")]
    public string comboParents;
}
