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
    private bool isTalking = false;

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


    public Camera cam;
    
    public GameObject talkingPos;

    private GameObject advText;

    public GameObject heldItem;
    
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
        //
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

        playerMove = GameObject.FindWithTag("Movement");
        dialogueCanvas = GameObject.Find("DialogueCanvas");
        mcSpriteSpot = dialogueCanvas.transform.GetChild(4).gameObject;
        thisCharSpriteSpot = dialogueCanvas.transform.GetChild(5).gameObject;
        dialogueText = dialogueCanvas.transform.GetChild(6).GetChild(0).GetComponent<TextMeshProUGUI>();
        nameText = dialogueCanvas.transform.GetChild(7).GetChild(0).GetComponent<TextMeshProUGUI>(); 
        cam = Camera.main;
        heldItem = GameObject.Find("RegMenuHeldItem");
        CurrentBlock = startBlock;
    }

    private void Update()
    {
        PlayerDistance();
        
        if (isTalking && playerCloseEnough)
        {
            dialogueCanvas.SetActive(true);
            playerMove.SetActive(false);
        }

        if (isTalking == false)
        {
            dialogueCanvas.SetActive(false);
            playerMove.SetActive(true);
        }

        if (Input.GetMouseButtonDown(0))
        {
            //Ray2D ray = cam.ScreenPointToRay(Input.mousePosition);

            if (!isTalking)
            {
                int layerMask = 1 << 9;
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, layerMask);

                if (hit.collider != null)
                {
                    var newObj = hit.collider.gameObject;
                    //Debug.Log(newObj.name);
                
                    if (newObj.name == this.gameObject.name)
                    {
                        /*if (isTalking && playerCloseEnough)
                        {
                            dialogueUpdate();
                        } */
                        if (!isTalking)
                        {
                            dialogueText.text = CurrentBlock.dialogue;
                            nameText.text = currentBlock.character;
                            //WE DO NOT WANT TO UPDATE WHICH BLOCK WE ARE LOOKING AT because the player may not be close enough to see it
                            //this is to compensate if the player clicks to talk, but needs to walk closer first
                            SpriteHandler();
                            isTalking = true;
                        }
                    }
                }
            } else if (isTalking && playerCloseEnough)
            {
                //Debug.Log("testing when this triggers");
                DialogueUpdate();
            }
        }
    }

    void DialogueUpdate()
    {

        var evt = false;
        
        //for protection
        dialogueCanvas.SetActive(true);

        //if there is an event on the current scriptable object
        if(CurrentBlock.hasEvent == true)
        {//if the held item matches the event item, in name
            if (InventoryManager.instance.heldItem.heldItem == CurrentBlock.eventItem)
            {
                Debug.Log("EVT TRUE");
                //then the event is actually happening
                evt = true;
                //the next block will be updated to be the alt block, rather than the usual block of text
                ItemReplace();
                CurrentBlock = CurrentBlock.alternativeBlock;
                
            }
            else
            {
                Debug.Log("No event");
                //otherwise we continue with the regular dialogue
                CurrentBlock = CurrentBlock.nextLine;
            }
        }
        else
        {
            Debug.Log("No event");
            //otherwise we continue with the regular dialogue
            CurrentBlock = CurrentBlock.nextLine;
        }

        //if you click, and you're already on the very last block of this particular dialogue section
        if (CurrentBlock.isEnd == true)
        {
         IfDialogueEnded();
         //Debug.Log("End");
        }
        else //regular happening in the dialogue
        {

            if (CurrentBlock.hasEvent == true && InventoryManager.instance.heldItem.heldItem == CurrentBlock.eventItem) //if the event is true (item matching) then we display the alt dialogue
            {
                
                dialogueText.text = CurrentBlock.altDialogue;
                nameText.text = CurrentBlock.character;
                SpriteHandler();
            }
            else //otherwise show the regular dialogue
            {
                dialogueText.text = CurrentBlock.dialogue;
                nameText.text = CurrentBlock.character;
                SpriteHandler();
            }
            
            //example: if you need to be holding a book:
            //if you are holding a book, say "I am holding a book"
            //if not, say "I need to find that book"
            

        }

        //TODO add replies section
        
    }

    void ItemReplace()
    {
        InventoryManager.instance.inventory.Container.Remove(InventoryManager.instance.heldItem.heldItem);
        DisplayInventory.instance.UpdateDisplay();
        InventoryManager.instance.ResetHeldItemImages();
        InventoryManager.instance.inventory.Container.Add(CurrentBlock.givenObject);
        DisplayInventory.instance.UpdateDisplay();
    }

    //when the dialogue is over for that particular conversation
    void IfDialogueEnded()
    {
        isTalking = false;
        dialogueCanvas.SetActive((false));
        CurrentBlock = CurrentBlock.nextConvo;  

    }
    
    //this handles all sprites based on the sprite in the scriptable object
    //different blocks used for positioning
    void SpriteHandler()
    {
        if (CurrentBlock.character == "Aoife")
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
    
    /*private void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log(other.gameObject.name);
        
        var player = other.gameObject.tag;
        if (player == playerTag)
        {
            playerCloseEnough = true;
            dialogueCanvas.SetActive(true);
        }
    }
    */

    /*private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(other.name);
        
        var player = other.gameObject.tag;
        if (player == playerTag)
        {
            playerCloseEnough = false;
            dialogueCanvas.SetActive(true);
        }
    }*/

    private void PlayerDistance()
    {
        if (Vector2.Distance(gameObject.transform.position, InventoryManager.instance.player.transform.position) < 1)
        {
            playerCloseEnough = true;
            dialogueCanvas.SetActive(true);
        }else
        {
            playerCloseEnough = false;
            dialogueCanvas.SetActive(false);
        }
    }

    //this is just to set the player's desired position to IN FRONT of the character they have clicked on
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
