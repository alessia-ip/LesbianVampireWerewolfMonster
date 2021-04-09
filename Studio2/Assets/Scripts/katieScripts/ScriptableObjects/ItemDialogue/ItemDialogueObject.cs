using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDialogue", menuName = "ScriptableObjects/Dialogue/Item")]
public class ItemDialogueObject : ScriptableObject
{
    public string header;
    public string character;
    public string dialogue;
    public Sprite itemSprite;
    public Sprite mcSprite;
    public ItemDialogueObject nextLine;

    public bool isEnd;
    public ItemDialogueObject nextConvo;

    public bool hasPickup;
    public GameObject pickupButton;
}
