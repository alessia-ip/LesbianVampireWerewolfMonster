using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ClickToMove : MonoBehaviour
{
    public GameObject hitcheck;
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, -Vector2.up);

           
            if (hit.collider != null)
            {
                hitcheck.transform.position = hit.point;
            }
            
        }

        player.transform.position =
            Vector2.Lerp(
                player.transform.position, 
                hitcheck.transform.position, 
                0.5f * Time.deltaTime);

    }
}
