using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ItemDialogue", menuName = "ScriptableObjects/Dialogue/Item")]
public class ItemDialogueObject : ScriptableObject
{
    public string itemName;
    public string dialogue;
    public Sprite itemSprite;
    public Sprite mcSprite;
    public ItemDialogueObject nextLine;

    public bool isEnd;
    public ItemDialogueObject nextConvo;

    public bool hasPickup;

    [Header("if item has combine this line will be what the player says if wrong")]
    public ItemDialogueObject nextLineWrong;
    [Header("Only non pickup item can check hasCombine")]
    public bool hasCombine;
    //public GameObject pickupButton;
}
