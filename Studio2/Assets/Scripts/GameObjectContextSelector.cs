using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectContextSelector : MonoBehaviour
{
    public enum contextTypes
    {
        weaponCursor,
        talkCursor,
        interactCursor,
        xCursor
    }

    public contextTypes objectContextType;
    
    private void OnMouseEnter()
    {
        switch (objectContextType)
        {
            case contextTypes.interactCursor:
                ContextCursorScript.instance.Interact();
                break;
            case contextTypes.talkCursor:
                ContextCursorScript.instance.Talk();
                break;
            case contextTypes.weaponCursor:
                ContextCursorScript.instance.Weapon();
                break;
            case contextTypes.xCursor:
                ContextCursorScript.instance.X();
                break;
        }    
    }

    private void OnMouseExit()
    {
        ContextCursorScript.instance.Walk();
    }
    
}
