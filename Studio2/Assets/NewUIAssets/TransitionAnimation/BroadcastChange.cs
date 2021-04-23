using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadcastChange : MonoBehaviour
{

    public GameObject map;
    
    
    public void animationBroadcast()
    {
        map.BroadcastMessage("sceneTransition");
        Debug.Log("animation");
    }
}
