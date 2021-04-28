using System;
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

    [System.Serializable]
    public class Location
    {
        public string locationName;
        public Camera cam;
        public GameObject pos;
        
        public Location(string _locationName, Camera _cam, GameObject _position)
        {
            locationName = _locationName;
            cam = _cam;
            pos = _position;
        }
    }

    public List<Location> locations = new List<Location>();

    
    //transition related things
    public Camera currentCam;
    public Camera newCam;
    public GameObject playerTeleportPos;
    private GetMouseWorld _getMouseWorld;
    public testing _testing;
    private GameObject playerObj;
    public Animator playAnim;
    
    
    private void Start()
    {
        _getMouseWorld = GameObject.FindWithTag("Movement").GetComponent<GetMouseWorld>();
        _testing = GameObject.FindWithTag("Movement").GetComponent<testing>();
        playerObj = GameObject.Find("PlayerFeet");
    }

    // Update is called once per frame
    void Update()
    {
        confirmText.GetComponent<Text>().text = "Go to the " + nextRoomName + "?";
        int layerMask = 1 << 10; //bit shift <- what was this for?
    }

    public void OpenMapUI()
    {
        mapCanv.SetActive(true);
    }

    public void CloseMap()
    {
        mapCanv.SetActive(false);
    }

    public void Accept()
    {
        Debug.Log("Accept");
        playAnim.Play("Base Layer.T", 0, 0);
        
        acceptBut.SetActive(false);
        denyBut.SetActive(false);
        confirmText.SetActive(false);
        mapCanv.SetActive(false);
        
        //SceneManager.LoadScene(nextSceneName);
    }

    public void Deny()
    {
        acceptBut.SetActive(false);
        denyBut.SetActive(false);
        confirmText.SetActive(false);
    }
    
    /*public void LocationSelect(string nextScene)
    {
        var breakStr = nextScene.Split(',');
        
        nextSceneName = breakStr[0];
        nextRoomName = breakStr[1];
        Debug.Log(nextRoomName);
        acceptBut.SetActive(true);
        denyBut.SetActive(true);
        confirmText.SetActive(true);
    } */
    
    public void LocationSelect(int nextScene)
    {
        nextRoomName = locations[nextScene].locationName;

        newCam = locations[nextScene].cam;
        playerTeleportPos = locations[nextScene].pos;
        
        Debug.Log(nextRoomName);
        acceptBut.SetActive(true);
        denyBut.SetActive(true);
        confirmText.SetActive(true);
    }


    void sceneTransition()
    {
        
        currentCam.gameObject.SetActive(false);
        newCam.gameObject.SetActive(true);
        currentCam = newCam;
        playerObj.transform.position = new Vector3(
            playerTeleportPos.transform.position.x, 
            playerTeleportPos.transform.position.y, 
            playerObj.transform.position.z);
        _getMouseWorld.cam = newCam;
        _testing.playerPosFix();
        _testing.playerAnimator.SetBool("Walking", false);
    }
    
}
