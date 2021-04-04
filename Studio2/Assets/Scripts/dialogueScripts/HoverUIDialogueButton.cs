using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverUIDialogueButton : MonoBehaviour
{
    Animator m_Animator;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    public void hoverON()
    {
        m_Animator.SetBool("Hovered", true);
    }
    
    public void hoverOFF()
    {
        m_Animator.SetBool("Hovered", false);
    }
    
}
