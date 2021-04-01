using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Item/BasicItem")]
public class ItemObject : ScriptableObject
{
    public GameObject canvasImage;//image for inv canvas
    public Sprite itemSprite;//for future sprite
}
