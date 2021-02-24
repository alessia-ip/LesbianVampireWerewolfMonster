using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDialogue : MonoBehaviour
{
    
    public GameObject movementScript;


    public void CloseTheUI()
    {
        movementScript.SetActive(false);
        ContextCursorScript.instance.Walk();
        this.gameObject.SetActive(false);
    }
    
}
