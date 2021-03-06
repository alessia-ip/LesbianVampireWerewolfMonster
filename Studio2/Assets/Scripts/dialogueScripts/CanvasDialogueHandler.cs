﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasDialogueHandler : MonoBehaviour
{
    [System.Serializable]
    public class LineNum
    {
        public int beginning;
        public int ending;
    }

    private int numSet = 0;
    
    public LineNum[] lineSet;
    
    //need this since this SHOULD be connected to individual characters
    private DialogueParser _dialogueParser;
    
   // private bool playerCloseEnough = false; //TODO put this in a scene with player
   private string playerTag = "Player";

    //TODO make these instances later. This was just easier for now lol
    public GameObject dialogueCanvas;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI charaName;
    public Button buttonOne;
    public Button buttonTwo;
    public Button buttonThree;
    
    //public GameObject movementScript;

    private bool isTallking = false;

    public int currentLine;
    
    public int startLine;
    public int endLine;

    public GameObject MC;
    public GameObject thisChar;

    private bool playerCloseEnough = false;
    public GameObject playerMove;


    
    // Start is called before the first frame update
    void Start()
    {
        _dialogueParser = GameObject.FindWithTag("Dialogue Manager").GetComponent<DialogueParser>();
        currentLine = startLine;
    }


    private void Update()
    {
        if (!playerCloseEnough)
        {
            dialogueCanvas.SetActive(false);
            currentLine = startLine;
        }
    }

    private void OnMouseDown()
    {
        /*if (playerCloseEnough == true)
        {
            movementScript.SetActive(false);
            dialogueCanvas.SetActive(true);
            ContextCursorScript.instance.Talk();

        }*/

        if (playerCloseEnough)
        {
            dialogueCanvas.SetActive(true);
            NextLine();
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.gameObject.tag;
        if (player == playerTag)
        {
            Debug.Log("COLLISION");
            playerCloseEnough = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(other.name);
        var player = other.gameObject.tag;
        if (player == playerTag)
        {
            playerCloseEnough = false;
            Debug.Log("Player close:" + playerCloseEnough);
        }
    }

    void NextLine()
    {
        if (currentLine > endLine)
        {

            dialogueCanvas.SetActive(false);
            numSet += 1;
            if (numSet < lineSet.Length)
            {
                startLine = lineSet[numSet].beginning;
                endLine = lineSet[numSet].ending;
            }
            currentLine = startLine;
            playerMove.SetActive(true);
        }
        else
        {
            playerMove.SetActive(false);

            dialogueText.text = _dialogueParser._dialogueInstances[currentLine].dialogue;
            charaName.text = _dialogueParser._dialogueInstances[currentLine].character;

            
            //REPLIES

            if (_dialogueParser._dialogueInstances[currentLine].replies != null)
            {

                int len = _dialogueParser._dialogueInstances[currentLine].replies.Length;
                              
                if (len >= 1)
                {
                    buttonOne.gameObject.SetActive(true);
                    buttonOne.GetComponentInChildren<TextMeshProUGUI>().text =
                        _dialogueParser._dialogueInstances[currentLine].replies[0];
                }
                else
                {
                    buttonOne.gameObject.SetActive(false);
                }
                
                if (len >= 2)
                {
                    buttonTwo.gameObject.SetActive(true);
                    buttonTwo.GetComponentInChildren<TextMeshProUGUI>().text =
                        _dialogueParser._dialogueInstances[currentLine].replies[1];
                }
                else
                {
                    buttonTwo.gameObject.SetActive(false);
                }
                
                if (len >= 3)
                {
                    buttonThree.gameObject.SetActive(true);
                    buttonThree.GetComponentInChildren<TextMeshProUGUI>().text =
                        _dialogueParser._dialogueInstances[currentLine].replies[2];
                }
                else
                {
                    buttonThree.gameObject.SetActive(false);
                }

            }
            else
            {
                buttonOne.gameObject.SetActive(false);
                buttonTwo.gameObject.SetActive(false);
                buttonThree.gameObject.SetActive(false);
                
            }

            if (_dialogueParser._dialogueInstances[currentLine].character == "MC")
            {
                MC.SetActive(true);
                thisChar.SetActive(false);
            }
            else
            {
                MC.SetActive(false);
                thisChar.SetActive(true);
            }
            
            //REPLIES

            currentLine++;
            
        }
    }


}
