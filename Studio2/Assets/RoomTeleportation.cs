using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTeleportation : MonoBehaviour
{

    public Camera currentCam;
    public Camera newCam;
    public GameObject playerTeleportPos;
    private GetMouseWorld _getMouseWorld;
    private testing _testing;
    private GameObject playerObj;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _getMouseWorld = GameObject.FindWithTag("Movement").GetComponent<GetMouseWorld>();
        _testing = GameObject.FindWithTag("Movement").GetComponent<testing>();
        playerObj = GameObject.Find("PlayerFeet");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("Trigger");

        
        if (other.tag == "Player")
        {
            _testing.path.Clear();
            currentCam.gameObject.SetActive(false);
            newCam.gameObject.SetActive(true);
            playerObj.transform.position = new Vector3(
                playerTeleportPos.transform.position.x, 
                playerTeleportPos.transform.position.y, 
                playerObj.transform.position.z);
           _getMouseWorld.cam = newCam;
           _testing.playerPosFix();
           _testing.playerAnimator.SetBool("Walking", false);
        }

    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        
        Debug.Log("Collision");
        
        if (other.gameObject.tag == "Player")
        {
            currentCam.gameObject.SetActive(false);
            newCam.gameObject.SetActive(true);
            playerObj.transform.position = playerTeleportPos.transform.position;
            _getMouseWorld.cam = newCam;
        }    
    }
}
