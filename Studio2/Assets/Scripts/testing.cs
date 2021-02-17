﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    // Start is called before the first frame update
    public Pathfinding pathfinding;
    private GetMouseWorld mouse;
    public GameObject player;
    private List<PathNode> path;
    private int pathNum;

    private void Start()
    {
        pathfinding = new Pathfinding(80, 60);
        mouse = GetComponent<GetMouseWorld>();
        var grid = pathfinding.GetGrid();
        for (var h = 0; h < grid.GetHeight(); h++)
        {
            for (var w = 0; w < grid.GetWidth(); w++)
            {
                //var grid = pathfinding.GetGrid();
                int x = 0;
                int y = 0;
                grid.GetXY(new Vector3(w, h), out x, out y);
                var nodeCheck = pathfinding.GetNode(x, y);
                Vector3 positionCheck = grid.GetWorldPosition(nodeCheck.x, nodeCheck.y);
                GameObject trigger = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                trigger.transform.position = positionCheck;
                trigger.AddComponent<CircleCollider2D>();
                Collider2D[] results;
                results =  new Collider2D[1000];
                Physics2D.OverlapCollider(trigger.GetComponent<CircleCollider2D>(),default, results);
                Debug.Log(results);
                //pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
            }
        }
        
    }
    
    private void Update()
    {
        var grid = pathfinding.GetGrid();
        if (Input.GetMouseButton(0))
        {
            pathNum = 1;
            int x = 0;
            int y = 0;
            int px;
            int py;
            grid.GetXY(mouse.worldPosition, out x, out y);
            grid.GetXY(player.transform.position, out px, out py);
            path = pathfinding.FindPath(px, py, x, y);
            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * .25f + Vector3.one * .125f, new Vector3(path[i + 1].x, path[i + 1].y) * .25f + Vector3.one * .125f, Color.green);
                }
            }
        }
        if (path != null)
        {
                //var lastNode = path[path.Count - 1];
                //var lastNodepos = grid.GetWorldPosition(lastNode.x, lastNode.y);
                var nextNode = path[pathNum];
                var nextNodepos = grid.GetWorldPosition(nextNode.x, nextNode.y);
                player.transform.position =
                    Vector3.MoveTowards(
                        player.transform.position,
                        nextNodepos,
                        1f * Time.deltaTime);
                if (player.transform.position == nextNodepos && pathNum != path.Count - 1)
                {
                    pathNum++;
                }
        }

       
    }
    
    
}
