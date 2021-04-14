using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveSceneLoader : MonoBehaviour
{
    [Header("To Load")]
    public string[] loadScenes;
    public Camera[] camerasInScene;
    
    [Header("In Game Scene Management")] 
    public string currentScene;
    public Camera currentCamera;


    private bool gridGen = false;

    public testing _gridMaker;
    
    // Start is called before the first frame update
    void Awake()
    {
      
        //if (!Application.isEditor)
        //{
            foreach (var scene in loadScenes)
            {
                SceneManager.LoadScene(scene, LoadSceneMode.Additive);
            }
        //}

    }

    /*private void Start()
    {
        Debug.Log(SceneManager.sceneCount - 1);
        Debug.Log(loadScenes.Length);
        if (SceneManager.sceneCount - 1 == loadScenes.Length && gridGen == false)
        {
            Debug.Log("Grid weeeeeeeee");
            _gridMaker.setGrid();
            gridGen = true;
        }
    }*/
}
