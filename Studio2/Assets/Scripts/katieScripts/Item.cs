using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class Item : MonoBehaviour
// {
//     public bool playerCloseEnough = false;
//     private string playerTag = "Player";
//     public enum ItemType
//     {
//         Cigar, Book, Paper, Nothing
//     }
//
//     public ItemType itemType = ItemType.Nothing;
//     public void SendToInv()
//     {
//         Inventory.instance.AddItem(gameObject.GetComponent<Item>());
//     }
//
//     private void OnMouseDown()
//     {
//         if (playerCloseEnough)
//         {
//             SendToInv();
//             Destroy(gameObject);
//         }
//     }
//
//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         var player = other.gameObject.tag;
//         if (player == playerTag)
//         {
//             Debug.Log("COLLISION");
//             playerCloseEnough = true;
//         }
//     }
//
//     private void OnTriggerExit2D(Collider2D other)
//     {
//         var player = other.gameObject.tag;
//         if (player == playerTag)
//         {
//             playerCloseEnough = false;
//         }
//     }
// }
