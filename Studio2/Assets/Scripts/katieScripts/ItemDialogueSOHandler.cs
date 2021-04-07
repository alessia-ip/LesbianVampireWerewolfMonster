using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemDialogueSOHandler : MonoBehaviour
{
    private bool isTalking = false;

    private bool playerCloseEnough = false;
    public GameObject playerMovement;
    private string playerTag = "Player";

    public ItemDialogueObject startBlock;
    public ItemDialogueObject currentBlock;
    
    public GameObject dialogueCanvas;
    public GameObject mcSpriteSpot;
    public GameObject itemSpriteSpot;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;

    private void Start()
    {
        currentBlock = startBlock;
    }

    private void Update()
    {
        if (isTalking == true && playerCloseEnough == true)
        {
            dialogueCanvas.SetActive(true);
            playerMovement.SetActive(false);
        }

        if (!isTalking)
        {
            int layerMask = 1 << 9;
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, layerMask);

            if (hit.collider != null)
            {
                var newObj = hit.collider.gameObject;
                if (newObj.name == this.gameObject.name)
                {
                    if (!isTalking)
                    {
                        dialogueText.text = currentBlock.dialogue;
                        nameText.text = currentBlock.character;
                        SpriteHandler();
                        isTalking = true;
                    }
                }
            }
        }else if (isTalking && playerCloseEnough)
        {
            
        }
    }

    void DialogueUpdate()
    {
        
        //this is where i stopped
    }

    void SpriteHandler()
    {
        itemSpriteSpot.SetActive(true);
        mcSpriteSpot.SetActive(true);

        mcSpriteSpot.GetComponent<Image>().sprite = currentBlock.mcSprite;
        itemSpriteSpot.GetComponent<Image>().sprite = currentBlock.itemSprite;

    }
}
