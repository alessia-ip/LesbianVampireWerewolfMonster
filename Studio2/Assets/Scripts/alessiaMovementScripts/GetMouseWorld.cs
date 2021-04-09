using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class GetMouseWorld : MonoBehaviour
{
    public Vector3 worldPosition;

    public Camera cam;

    private void Start()
    {
        
    }

    private void Update()
    {
        worldPosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
    }
}
