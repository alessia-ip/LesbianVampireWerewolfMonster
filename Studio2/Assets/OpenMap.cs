using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpenMap : MonoBehaviour
{

    public GameObject mapCanv;

    private string nextSceneName;
    private string nextRoomName;

    public GameObject confirmText;
    public GameObject acceptBut;
    public GameObject denyBut;

    // Update is called once per frame
    void Update()
    {
        confirmText.GetComponent<Text>().text = "Go to the " + nextRoomName + "?";
        
        int layerMask = 1 << 10; //bit shift
        
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(
                Camera.main.ScreenToWorldPoint(Input.mousePosition), 
                Vector2.zero, 
                Mathf.Infinity, 
                layerMask);

            if (hit != null)
            {
                OpenMapUI();
            }
        }
    }

    void OpenMapUI()
    {
        mapCanv.SetActive(true);
    }

    public void CloseMap()
    {
        mapCanv.SetActive(false);
    }

    public void Accept()
    {        
        acceptBut.SetActive(false);
        denyBut.SetActive(false);
        confirmText.SetActive(false);
        mapCanv.SetActive(false);
        SceneManager.LoadScene(nextSceneName);
    }

    public void Deny()
    {
        acceptBut.SetActive(false);
        denyBut.SetActive(false);
        confirmText.SetActive(false);
    }
    
    public void LocationSelect(string nextScene)
    {
        var breakStr = nextScene.Split(',');
        
        nextSceneName = breakStr[0];
        nextRoomName = breakStr[1];
        Debug.Log(nextRoomName);
        acceptBut.SetActive(true);
        denyBut.SetActive(true);
        confirmText.SetActive(true);
    }
    
}
