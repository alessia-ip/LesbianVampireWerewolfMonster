using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class NodeCollision : MonoBehaviour
{
    private GameObject player;
    private testing tscript;
    private Vector3 pos;
    private void Awake()
    {
        player = GameObject.Find("Player Movement");
        tscript = player.GetComponent<testing>();
        StartCoroutine(Wait());

    }

    private void Start()
    {
        pos = transform.position;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(.25f);
        Destroy(this.gameObject);
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        //Debug.Log("Collision");
        var notWalk = other.gameObject.tag;
        if (notWalk == "Unwalkable")
        {
            tscript.SetUnwalkable(pos);
            Debug.Log("Matt Parker");
        }
    }
   
}
