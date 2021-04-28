using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineHover : MonoBehaviour
{
   public Material outline;


   private void Start()
   {
       
   }

   private void OnMouseOver()
   {
    outline.SetFloat("_Thickness", 2.5f);
       
   }

   private void OnMouseExit()
   {
       outline.SetFloat("_Thickness", 0f);
   }
}
