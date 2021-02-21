using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvOpenClose : MonoBehaviour
{
    public GameObject invCanvas;
    public GameObject playCanvas;
    
    public void OpenInv()
    {
        Time.timeScale = 0;
        invCanvas.SetActive(true);
        playCanvas.SetActive(false);
    }

    public void CloseInv()
    {
        Time.timeScale = 0;
        invCanvas.SetActive(false);
        playCanvas.SetActive(true);
    }
}
