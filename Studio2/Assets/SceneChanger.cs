using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public string sceneName;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Feet")
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
