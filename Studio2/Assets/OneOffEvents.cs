using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OneOffEvents : MonoBehaviour
{

    [Header("Dialogue Box")]
    public TextMeshProUGUI dialogueCheck;

    [Header("Map Check Variables")]
    public DialogueScriptableObject mapGetDialogue;
    public GameObject mapButton;
    public GameObject mapCollider;

    [Header("Ritual Start Variables")] 
    public DialogueScriptableObject akariRitualDialogue;
    public GameObject fadeToBlack;
    public GameObject akariCharacterInGame;
    public GameObject mxPaws;
    public DialogueScriptableObject mxPawsNewStart;
    public GameObject gardener;
    public DialogueScriptableObject gardenerNewStart;
    public testing resetGrid;

    [Header("Ritual End Variables")] 
    public DialogueScriptableObject ritualEndDialogue;
    public Button libraryButton;
    public Button loungeButton;

    [Header("Password")] 
    public DialogueScriptableObject passwordDialogue;
    public Button hubRoom;

    [Header("HubEnd")] 
    public DialogueScriptableObject lastLine;

    // Update is called once per frame
    void Update()
    {
        
        //check if the blank character has been spoken 
        //TODO - blank character that takes up the whole screen
        
        
        
        //turn items off until you talk to akari
        
        
        
        //checking if the player has gotten the map at the beginning of the game
        if (dialogueCheck.text == mapGetDialogue.altDialogue)
        {
            mapButton.SetActive(true);
            mapCollider.SetActive(true);
        }

        //check if the ritual has started - when it goes dark
        if (dialogueCheck.text == akariRitualDialogue.dialogue)
        {
            akariCharacterInGame.GetComponent<SpriteRenderer>().enabled = false;
            akariCharacterInGame.GetComponent<Collider2D>().enabled = false;

            mxPaws.GetComponent<CharacterDialogueSOHandler>().startBlock = mxPawsNewStart;
            gardener.GetComponent<CharacterDialogueSOHandler>().startBlock = gardenerNewStart;
            
            fadeToBlack.SetActive(true); //TODO ANIMATE THIS
            resetGrid.MapWalk();
        }

        //TODO CHAPTER HEADER CHANGE 
        
        //check if the the ritual is completed
        if (dialogueCheck.text == ritualEndDialogue.dialogue)
        {
            fadeToBlack.SetActive(false);
            loungeButton.interactable = true;
            libraryButton.interactable = true;
        }

        //check if the password is given
        if (dialogueCheck.text == passwordDialogue.dialogue) //TODO check if dialogue or ALT dialogue
        {
            hubRoom.interactable = true;
        }
        
        //check for last line of dialogue
        if (dialogueCheck.text == lastLine.dialogue)
        {
            //TODO last line event
        }
        
    }
}
