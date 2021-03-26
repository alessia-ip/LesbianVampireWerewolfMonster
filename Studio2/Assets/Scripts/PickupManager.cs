using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public GameObject itemCanvas;
    
    [HideInInspector]
    public string itemType = "Nothing";
    [HideInInspector]
    public GameObject destroyThisItem = null;
    

    public void Pickup()
    {

        switch (itemType)
        {
            case "Cigar":
                Inventory.instance.AddItem("Cigar");
                break;
            case "Book":
                Inventory.instance.AddItem("Book");
                break;
            case "Paper":
                Inventory.instance.AddItem("Paper");
                break;
        }
        itemCanvas.SetActive(false);
        Destroy(destroyThisItem);
        
    }
    
    
}
