using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AUDIO : MonoBehaviour
{

    public GameObject gardenCamera;
    private bool inGarden = false;

    public bool InGarden
    {
        get
        {
            return inGarden;
        }
        
        set
        {
            Debug.Log("fix garden");
            inGarden = value;
            changeAudio();
        }
    }

    private void Update()
    {
        if (gardenCamera.activeInHierarchy && InGarden == false)
        {
            InGarden = true;
        }
        else if (!gardenCamera.activeInHierarchy && InGarden == true)
        {
            InGarden = false;
        }
    }

    void changeAudio()
    {
        Debug.Log("run the audio changer");
        if (InGarden == true)
        {
            Debug.Log("GARDEN MFERS");
            this.gameObject.GetComponent<AudioSource>().enabled = false;
        }
        else
        {
            this.gameObject.GetComponent<AudioSource>().enabled = true;
        }
    }
}
