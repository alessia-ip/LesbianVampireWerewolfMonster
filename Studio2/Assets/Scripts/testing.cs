using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    // Start is called before the first frame update
    public Pathfinding pathfinding;
    private GetMouseWorld mouse;

    private void Start()
    {
        pathfinding = new Pathfinding(100, 100);
        mouse = GetComponent<GetMouseWorld>();
    }
    
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var grid = pathfinding.GetGrid();
            int x = 0;
            int y = 0;
            grid.GetXY(mouse.worldPosition, out x, out y);
            List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            var grid = pathfinding.GetGrid();
            int x = 0;
            int y = 0;
            grid.GetXY(mouse.worldPosition, out x, out y);
            pathfinding.GetNode(x,y).SetIsWalkable(!pathfinding.GetNode(x,y).isWalkable);
        }
    }
    
    
}
