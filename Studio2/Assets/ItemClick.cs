using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class ItemClick : MonoBehaviour
{
    //this is for clicking on items in the world

    private ItemAndWorldParser _itemAndWorldParser;

    public int currentLine;
        
    public int startLine;
    public int endLine;
    
    [FormerlySerializedAs("dialogueCanvas")] public GameObject itemCanvas;
    public TextMeshProUGUI itemText;
    
    private bool playerCloseEnough = true;
    private string playerTag = "Player";


    public GameObject pButton;
    
    void Start()
    {
        _itemAndWorldParser = GameObject.FindWithTag("Dialogue Manager").GetComponent<ItemAndWorldParser>();
        currentLine = startLine;
    }
    
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (!playerCloseEnough)
        {
            itemCanvas.SetActive(false);
            currentLine = startLine;
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

    private void OnMouseDown()
    {
        if (playerCloseEnough == true)
        {
            itemCanvas.SetActive(true);
            nextLine();
        }
    }


    void nextLine()
    {
        if (currentLine > endLine)
        {
            currentLine = startLine;
            itemCanvas.SetActive(false);
            GameObject.FindWithTag("Dialogue Manager").GetComponent<PickupManager>().itemType =
                (PickupManager.ItemType) Enum.Parse(typeof(PickupManager.ItemType), "Nothing");
        }
        else
        {
            itemText.text = _itemAndWorldParser._worldTextInstances[currentLine].text;
            if (_itemAndWorldParser._worldTextInstances[currentLine].isItem)
            {
                GameObject.FindWithTag("Dialogue Manager").GetComponent<PickupManager>().itemType =
                   (PickupManager.ItemType) Enum.Parse(typeof(PickupManager.ItemType), this.gameObject.name);
                pButton.SetActive(true);
            }
            else
            {
                pButton.SetActive(false);
            }

            currentLine++;
        }
 // gameobject has a name (string) -> take the name -> make name an enum -> set the enum in pickup
    }
}
