using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemDialogueSOHandler : MonoBehaviour
{
    public ItemObject item;
    public ItemDialogueObject startBlock;
    private SpriteRenderer inGameSprite;
    private bool isTalking = false;

    private bool playerCloseEnough = false;
    private string playerTag = "Player";

    [HideInInspector]
    public Camera mainCam;

    
    [HideInInspector]
    public ItemDialogueObject currentBlock;

    private void Start()
    {
        mainCam = Camera.main;
        inGameSprite = this.gameObject.GetComponent<SpriteRenderer>();
        inGameSprite.sprite = item.itemSprite;
        currentBlock = startBlock;
        InventoryManager.instance.mcSpriteSpot.GetComponent<Image>().sprite = currentBlock.mcSprite;
        InventoryManager.instance.itemSpriteSpot.GetComponent<Image>().sprite = currentBlock.itemSprite;
    }

    private void Update()
    {
        PlayerDistance();

        if (playerCloseEnough)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!isTalking)
                {
                    int layerMask = 1 << 12;
                    RaycastHit2D hit = Physics2D.Raycast(mainCam.ScreenToWorldPoint(Input.mousePosition),
                        Vector2.zero, Mathf.Infinity, layerMask);
                    if (hit.collider != null)
                    {
                        var newObj = hit.collider.gameObject;
                        if (newObj.name == this.gameObject.name)
                        {
                            isTalking = true;
                            InventoryManager.instance.playerMovement.SetActive(false);
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

    void DialogueUpdate()
    {
        InventoryManager.instance.dialogueText.text = currentBlock.dialogue;
        InventoryManager.instance.nameText.text = currentBlock.itemName;

        if (currentBlock.hasPickup)
        {
            InventoryManager.instance.currentItem = item;
            InventoryManager.instance.currentItemGameObj = gameObject;
            InventoryManager.instance.pickupButton.SetActive(true);
        }
        else
        {
            InventoryManager.instance.pickupButton.SetActive(false);
        }
    }

    private void DialogueEnded()
    {
        if (!currentBlock.hasPickup)
        {
            isTalking = false;
            InventoryManager.instance.itemDialogueCanvas.SetActive(false);
            currentBlock = currentBlock.nextConvo;
            InventoryManager.instance.playerMovement.SetActive(true);
        }
    }

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
