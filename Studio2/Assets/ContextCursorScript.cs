using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextCursorScript : MonoBehaviour
{
    
    public Texture2D walkCursor;
    public Texture2D talkCursor;
    public Texture2D interactCursor;
    public Texture2D xCursor;
    public Texture2D weaponCursor;

    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    private void Start()
    {
        Cursor.SetCursor(walkCursor, hotSpot, cursorMode);
    }

    public void Walk()
    {
        Cursor.SetCursor(walkCursor, hotSpot, cursorMode);
    }

    public void Talk()
    {
        Cursor.SetCursor(talkCursor, hotSpot, cursorMode);
    }
        
    public void Interact()
    {
        Debug.Log("interact");
        Cursor.SetCursor(interactCursor, hotSpot, cursorMode);
    }
        
    public void X()
    {
        Cursor.SetCursor(xCursor, hotSpot, cursorMode);
    }
        
    public void Weapon()
    {
        Cursor.SetCursor(weaponCursor, hotSpot, cursorMode);
    }
    
    
    
}
