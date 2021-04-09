using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemDialogueSOHandler : MonoBehaviour
{
    public ItemObject item;
    private SpriteRenderer inGameSprite;
    private bool isTalking = false;

    private bool playerCloseEnough = false;
    public GameObject playerMovement;
    private string playerTag = "Player";

    public ItemDialogueObject startBlock;
    public ItemDialogueObject currentBlock;
    
    public GameObject dialogueCanvas;
    public GameObject mcSpriteSpot;
    public GameObject itemSpriteSpot;
    public GameObject pickupButton;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;

    public GameObject player;

    private void Start()
    {
        inGameSprite = this.gameObject.GetComponent<SpriteRenderer>();
        inGameSprite.sprite = item.itemSprite;
        currentBlock = startBlock;
        mcSpriteSpot.GetComponent<Image>().sprite = currentBlock.mcSprite;
        itemSpriteSpot.GetComponent<Image>().sprite = currentBlock.itemSprite;
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
                    int layerMask = 1 << 11;
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),
                        Vector2.zero, Mathf.Infinity, layerMask);
                    if (hit.collider != null)
                    {
                        var newObj = hit.collider.gameObject;
                        if (newObj.name == this.gameObject.name)
                        {
                            isTalking = true;
                            playerMovement.SetActive(false);
                            dialogueCanvas.SetActive(true);
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
        dialogueText.text = currentBlock.dialogue;
        nameText.text = currentBlock.itemName;

        if (currentBlock.hasPickup)
        {
            InventoryManager.instance.currentItem = item;
            InventoryManager.instance.currentItemGameObj = gameObject;
            pickupButton.SetActive(true);
        }
        else
        {
            pickupButton.SetActive(false);
        }
    }

    private void DialogueEnded()
    {
        if (!currentBlock.hasPickup)
        {
            isTalking = false;
            dialogueCanvas.SetActive(false);
            currentBlock = currentBlock.nextConvo;
            playerMovement.SetActive(true);
        }
    }

    void PlayerDistance()
    {
        if (Vector2.Distance(gameObject.transform.position, player.transform.position) < 1)
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
