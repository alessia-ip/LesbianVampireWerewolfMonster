using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Dialogue Block", 
    menuName = "ScriptableObjects/Dialogue Block",
    order = 0)]

public class DialogueScriptableObject : ScriptableObject
{
    public string header;
    public string character;
    public string dialogue;
    public Sprite characterPortrait;
    public DialogueScriptableObject nextLine;
    //If this is the end block, the 'next convo' line will be used to begin the next conversation
    public bool isEnd;
    public DialogueScriptableObject nextConvo;
    //This is for if the player has the option to choose a piece of dialogue
    public bool hasReply;
    public string[] replies;
    public DialogueScriptableObject[] replyAnswers;
    //This is for if you can trigger alternative dialogue by holding an item
    public bool hasEvent; 
    public GameObject eventItem;
    public string altDialogue; //only needed for events
    public DialogueScriptableObject alternativeBlock;
}
