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
    
    // Start is called before the first frame update
    void Start()
    {


        //if (!Application.isEditor)
        //{
            foreach (var scene in loadScenes)
            {
                SceneManager.LoadScene(scene, LoadSceneMode.Additive);
            }
        //}

        
        
        
    }
    
}
