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
    private bool initGridReset = false;
    
    [Header("Remove Prologue Thing")] 
    public DialogueScriptableObject startSO;
    public GameObject prologueText;
    public GameObject mainMenu;
    
    [Header("THICC character lol")] 
    public GameObject thiccCharacter;
    public DialogueScriptableObject introScene;

    [Header("ItemsToTurnOn")] 
    public DialogueScriptableObject firstAkari;
    public GameObject mortar;
    public GameObject cauldron;
    public GameObject painting;
    public GameObject map;
    public GameObject window;
    
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
    private bool initGridReset2 = false;
    public DialogueScriptableObject ThiccNewStart;

    [Header("Ritual End Variables")] 
    public DialogueScriptableObject ritualEndDialogue;
    public Button libraryButton;
    public Button loungeButton;
    public DialogueScriptableObject newStartBlockTHICC;
    public GameObject ChapterOne;

    [Header("End Ch 1 Cutscene")] public DialogueScriptableObject endCH1;
    
    [Header("Password")] 
    public DialogueScriptableObject passwordDialogue;
    public Button hubRoom;

    [Header("HubEnd")] 
    public GameObject duchess;
    public DialogueScriptableObject duchessExit;
    public DialogueScriptableObject lastLine;
    public GameObject endOfCh1Text;


    [Header("ANGER")] public DialogueScriptableObject TOTHELIB;
    
    // Update is called once per frame
    void Update()
    {
        
        //check if the blank character has been spoken 
        //TODO - blank character that takes up the whole screen
        if (dialogueCheck.text == startSO.dialogue)
        {
            mainMenu.SetActive(true);
            prologueText.SetActive(false);
          
            mortar = GameObject.Find("Mortar&Pestle");
            painting = GameObject.Find("Painting");
            window = GameObject.Find("Stained glass");
            cauldron = GameObject.Find("Cauldron");
            map = GameObject.Find("Map");

            mortar.GetComponent<ItemDialogueSOHandler>().enabled = false;
            painting.GetComponent<ItemDialogueSOHandler>().enabled = false;
            window.GetComponent<ItemDialogueSOHandler>().enabled = false;
            cauldron.GetComponent<ItemDialogueSOHandler>().enabled = false;
            map.GetComponent<ItemDialogueSOHandler>().enabled = false;


        }
        
        
        //turn off dark character
        if (dialogueCheck.text == introScene.dialogue)
        {
            thiccCharacter.GetComponent<SpriteRenderer>().enabled = false;
            thiccCharacter.GetComponent<Collider2D>().enabled = false;
            if (initGridReset == false)
            {
                resetGrid.MapWalk();
                initGridReset = true; 
                //resetGrid.path.Clear();
            }
        }
        
        //turn items off until you talk to akari
        if (dialogueCheck.text == firstAkari.dialogue)
        {

            mortar.GetComponent<ItemDialogueSOHandler>().enabled = true;
            painting.GetComponent<ItemDialogueSOHandler>().enabled = true;
            window.GetComponent<ItemDialogueSOHandler>().enabled = true;
            cauldron.GetComponent<ItemDialogueSOHandler>().enabled = true;
            map.GetComponent<ItemDialogueSOHandler>().enabled = true;
        }
        
        
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
            akariCharacterInGame.GetComponent<BoxCollider2D>().enabled = false;

            mxPaws.GetComponent<CharacterDialogueSOHandler>().startBlock = mxPawsNewStart;
            gardener.GetComponent<CharacterDialogueSOHandler>().startBlock = gardenerNewStart;
            mxPaws.GetComponent<CharacterDialogueSOHandler>().currentBlock = mxPawsNewStart;
            gardener.GetComponent<CharacterDialogueSOHandler>().currentBlock = gardenerNewStart;
            thiccCharacter.GetComponent<CharacterDialogueSOHandler>().startBlock = ThiccNewStart;
            thiccCharacter.GetComponent<CharacterDialogueSOHandler>().currentBlock = ThiccNewStart;
            
            
            mortar.GetComponent<ItemDialogueSOHandler>().enabled = false;
            painting.GetComponent<ItemDialogueSOHandler>().enabled = false;
            window.GetComponent<ItemDialogueSOHandler>().enabled = false;
            cauldron.GetComponent<ItemDialogueSOHandler>().enabled = false;
         
            
            fadeToBlack.SetActive(true); //TODO ANIMATE THIS
            
            if (initGridReset2 == false)
            {
                resetGrid.MapWalk();
                initGridReset2 = true;
                resetGrid.path.Clear();
            }
        }
        
        
        //check if the the ritual is completed - turn on chapter one header
        if (dialogueCheck.text == ritualEndDialogue.dialogue)
        {
            fadeToBlack.SetActive(false);
            loungeButton.interactable = true;
            libraryButton.interactable = true;
            loungeButton.gameObject.SetActive(true);
            libraryButton.gameObject.SetActive(true);
            
            thiccCharacter.GetComponent<SpriteRenderer>().enabled = true;
            thiccCharacter.GetComponent<Collider2D>().enabled = true;
            thiccCharacter.GetComponent<CharacterDialogueSOHandler>().startBlock = newStartBlockTHICC;
            thiccCharacter.GetComponent<CharacterDialogueSOHandler>().CurrentBlock = newStartBlockTHICC;
            ChapterOne.SetActive(true);
        }
        
        //when you start talking to thicc, turn off the chapter one header
        if (dialogueCheck.text == newStartBlockTHICC.dialogue)
        {
            ChapterOne.SetActive(false);
        }
                
        if (dialogueCheck.text == endCH1.dialogue)
        {
            
          
            
            thiccCharacter.GetComponent<SpriteRenderer>().enabled = false;
            thiccCharacter.GetComponent<Collider2D>().enabled = false;
            fadeToBlack.SetActive(false);
        }


        if (dialogueCheck.text == TOTHELIB.dialogue)
        {
            mortar.GetComponent<ItemDialogueSOHandler>().enabled = true;
            painting.GetComponent<ItemDialogueSOHandler>().enabled = true;
            window.GetComponent<ItemDialogueSOHandler>().enabled = true;
            cauldron.GetComponent<ItemDialogueSOHandler>().enabled = true;
        }
        
        //check if the password is given
        if (dialogueCheck.text == passwordDialogue.dialogue) //TODO check if dialogue or ALT dialogue
        {
            hubRoom.interactable = true;
            hubRoom.gameObject.SetActive(true);

        }


        if (dialogueCheck.text == duchessExit.dialogue)
        {
            thiccCharacter.SetActive(true);
            duchess.SetActive(false);
            thiccCharacter.SetActive(false);
            
        }
        
        //check for last line of dialogue
        if (dialogueCheck.text == lastLine.dialogue)
        {
            Invoke("lastLineEnd", 1);
        }
        
    }

    public void lastLineEnd()
    {
        fadeToBlack.SetActive(true);
        endOfCh1Text.SetActive(true);
        mainMenu.SetActive(false);
    }
    
}
