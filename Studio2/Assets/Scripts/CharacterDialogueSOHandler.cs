using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDialogueSOHandler : MonoBehaviour
{
    
    //are they currently talking?
    private bool isTallking = false;

    //this is if the player is close enough to the other character  or not
    private bool playerCloseEnough = false;
    public GameObject playerMove;
    private string playerTag = "Player";
    
    //setup
    //TODO save the current dialogue block
    public DialogueScriptableObject startBlock;
    public DialogueScriptableObject currentBlock;

    //canvas setup
    public GameObject dialogueCanvas;
    public GameObject mcSpriteSpot;
    public GameObject thisCharSpriteSpot;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    
    public GameObject talkingPos;
    
    public DialogueScriptableObject CurrentBlock
    {
        get
        {
            return currentBlock;
        }
        set
        {
            currentBlock = value;
            /*string jsonDialogue = JsonUtility.ToJson(currentBlock, true);
            File.WriteAllText(FILE_PATH_JSON, jsonDialogue);*/
        }
    }

    private string FILE_PATH_JSON;
    
    // Start is called before the first frame update
    void Start()
    {
        /*FILE_PATH_JSON = Application.dataPath + "/" + name + ".json";
        if (File.Exists(FILE_PATH_JSON))
        {
            string jsonText = File.ReadAllText(FILE_PATH_JSON);
            DialogueScriptableObject startingBlock = JsonUtility.FromJson<DialogueScriptableObject>(jsonText);
            CurrentBlock = startingBlock;
        }
        else
        {
            CurrentBlock = startBlock;
        }*/
        
        CurrentBlock = startBlock;
    }

    private void Update()
    {
        if (isTallking == true && playerCloseEnough == true)
        {
            playerMove.SetActive(false);
        }

        if (isTallking == false)
        {
            playerMove.SetActive(true);
        }
    }

    private void OnMouseDown()
    {
        
        if (isTallking == false)
        {
            for (int i = 0; i < dialogueCanvas.transform.childCount - 1; i++)
            {
                dialogueCanvas.transform.GetChild(i).gameObject.SetActive(true);
            }
            dialogueText.text = CurrentBlock.dialogue;
            nameText.text = currentBlock.character;
            //WE DO NOT WANT TO UPDATE WHICH BLOCK WE ARE LOOKING AT because the player may not be close enough to see it
            //this is to compensate if the player clicks to talk, but needs to walk closer first
            SpriteHandler();
            isTallking = true;
        }

        if (isTallking == false && playerCloseEnough == true)
        {
            //We want to turn off the player's movement
            //then start the dialogue
            isTallking = true;
            dialogueUpdate();
        }

        if (isTallking == true && playerCloseEnough == true)
        {
            //We want to update the dialogue
            dialogueUpdate();
        }
        
    }

    void dialogueUpdate()
    {
        //if you click, and you're already on the very last block of this particular dialogue section
        if (CurrentBlock.isEnd == true)
        {
         IfDialogueEnded();
        } else if(CurrentBlock.hasEvent == true)
        {
            //handle the item event thing in here
        }
        else //regular happening
        {
            if (dialogueText.text != CurrentBlock.dialogue)
            {
                dialogueText.text = CurrentBlock.dialogue;
                nameText.text = CurrentBlock.character;
                SpriteHandler();
                CurrentBlock = CurrentBlock.nextLine; //advance for the next click
            }
            else
            {
                CurrentBlock = CurrentBlock.nextLine;
                if (CurrentBlock.isEnd == true)
                {
                    IfDialogueEnded();
                }
                else
                {
                    dialogueText.text = CurrentBlock.dialogue;
                    nameText.text = CurrentBlock.character;
                    SpriteHandler();
                }
            }
            

            
            //TODO add replies section
        }
    }

    void IfDialogueEnded()
    {
        for (int i = 0; i < dialogueCanvas.transform.childCount; i++)
        {
            dialogueCanvas.transform.GetChild(i).gameObject.SetActive(false);
        }
        CurrentBlock = CurrentBlock.nextConvo;  
        isTallking = false;
    }
    
    void SpriteHandler()
    {
        if (CurrentBlock.character == "Main Character")
        {
            thisCharSpriteSpot.SetActive(false);
            mcSpriteSpot.SetActive(true);
            mcSpriteSpot.GetComponent<Image>().sprite = CurrentBlock.characterPortrait;
        }
        else
        {
            thisCharSpriteSpot.SetActive(true);
            mcSpriteSpot.SetActive(false);
            thisCharSpriteSpot.GetComponent<Image>().sprite = CurrentBlock.characterPortrait;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.gameObject.tag;
        if (player == playerTag)
        {
            playerCloseEnough = true;
            dialogueCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(other.name);
        var player = other.gameObject.tag;
        if (player == playerTag)
        {
            playerCloseEnough = false;
            dialogueCanvas.SetActive(true);
            for (int i = 0; i < dialogueCanvas.transform.childCount; i++)
            {
                dialogueCanvas.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    private void OnMouseOver()
    {
        playerMove.GetComponent<testing>().charHover = true;
        playerMove.GetComponent<testing>().charXY = talkingPos.transform.position;

    }

    private void OnMouseExit()
    {
        playerMove.GetComponent<testing>().charHover = false;
    }
}
