using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemDialogueSOHandler : MonoBehaviour
{
    //this script goes on each interactable item in game
    //these can be pickup items/ combo items/ or just fluff
    
    //place the items scriptable object
    public ItemObject item;
    //place the first line of dialogue for this item
    public ItemDialogueObject startBlock;
    
    //sets sprite
    private SpriteRenderer inGameSprite;
    
    //used for disable click when talk
    private bool isTalking = false;

    //check player distance form item
    private bool playerCloseEnough = false;
    private string playerTag = "Player";

    [HideInInspector]
    public Camera mainCam;

    
    [HideInInspector]
    public ItemDialogueObject currentBlock;

    private bool canClickNext;
    
    //TODO add variables for item scale in world
    //TODO add variables for itemSpriteSpot transform.position.y

    private void Start()
    {
        //mainCam = Camera.main;
        inGameSprite = gameObject.GetComponent<SpriteRenderer>();
        inGameSprite.sprite = item.itemSprite;
        currentBlock = startBlock;
        
        canClickNext = true;
    }

    private void Update()
    {
        mainCam = Camera.main;
        PlayerDistance();

        if (playerCloseEnough)
        {
            if (Input.GetMouseButtonDown(0) && canClickNext)
            {
                if (!isTalking)
                {
                    int layerMask = 1 << 12;//check to see if correct item was hit
                    RaycastHit2D hit = Physics2D.Raycast(mainCam.ScreenToWorldPoint(Input.mousePosition),
                        Vector2.zero, Mathf.Infinity, layerMask);
                    
                    if (hit.collider != null)
                    {
                        Debug.Log("Test");
                        var newObj = hit.collider.gameObject;
                        if (newObj.name == this.gameObject.name)
                        {
                            isTalking = true;
                            InventoryManager.instance.playerMovement.GetComponent<testing>().enabled = false;
                            InventoryManager.instance.itemDialogueCanvas.SetActive(true);
                            DialogueUpdate();
                            
                        }
                    }
                }
                else //is talking is true
                {
                    if (currentBlock.isEnd)
                    {
                        DialogueEnded();
                    }
                    else
                    {
                        currentBlock = currentBlock.nextLine;
                        DialogueUpdate();
                    }
                }
            }
        }
    }

    public void DialogueUpdate()
    {
        InventoryManager.instance.mcSpriteSpot.GetComponent<Image>().sprite = currentBlock.mcSprite;
        InventoryManager.instance.itemSpriteSpot.GetComponent<Image>().sprite = currentBlock.itemSprite;
        InventoryManager.instance.dialogueText.text = currentBlock.dialogue;
        InventoryManager.instance.nameText.text = currentBlock.itemName;

        if (currentBlock.hasPickup)
        {
            canClickNext = false;
            InventoryManager.instance.combineButton.SetActive(false);
            InventoryManager.instance.pickupButton.SetActive(true);
            InventoryManager.instance.currentItem = item;
            InventoryManager.instance.currentItemGameObj = gameObject;
            //Debug.Log(gameObject);
        }
        else if (currentBlock.hasCombine)
        {
            canClickNext = false;
            InventoryManager.instance.pickupButton.SetActive(false);
            InventoryManager.instance.combineButton.SetActive(true);
            InventoryManager.instance.currentItem = item;
            InventoryManager.instance.currentItemDialogue = gameObject;
        }
        else
        {
            canClickNext = true;
            InventoryManager.instance.pickupButton.SetActive(false);
            InventoryManager.instance.combineButton.SetActive(false);
        }
    }

    //TODO maybe add over on pickup button so players can exit dialogue without picking item up (not high priority)
    private void DialogueEnded()
    {
        // if (!currentBlock.hasPickup)
        // {
            isTalking = false;
            InventoryManager.instance.itemDialogueCanvas.SetActive(false);
            currentBlock = currentBlock.nextConvo;
            InventoryManager.instance.playerMovement.GetComponent<testing>().enabled = true;
        //}
    }

    
    //TODO make 1 into variable that can change depending on object (different room sizes)
    void PlayerDistance()
    {
        if (Vector2.Distance(gameObject.transform.position, InventoryManager.instance.player.transform.position) < 1)
        {
            playerCloseEnough = true;
            //Debug.Log("close enough");
        }
        else
        {
            playerCloseEnough = false;
            //Debug.Log("no cigar");
        }
    }
}
