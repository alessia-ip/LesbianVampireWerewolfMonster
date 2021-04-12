using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveSceneLoader : MonoBehaviour
{

    public string[] loadScenes;

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
