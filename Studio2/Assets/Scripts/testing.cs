using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    // Start is called before the first frame update
    static Pathfinding pathfinding;
    private GetMouseWorld mouse;
    public GameObject player;
    private List<PathNode> path;
    private int pathNum;
    public LayerMask layerMask;
    
    //TODO if the player clicks on a not walkable space, select the closest walkable node instead


    public void MapWalk()
    {
        pathfinding = new Pathfinding(80, 60);
        mouse = GetComponent<GetMouseWorld>();
        var grid = pathfinding.GetGrid();
        for (var y = 0; y < grid.GetHeight(); y++)
        {
            //Debug.Log(grid.GetHeight());
            for (var x = 0; x < grid.GetWidth(); x ++)
            {
                //var grid = pathfinding.GetGrid();
                Vector2 worldPosition = grid.GetWorldPosition(x, y);
                if (Physics2D.OverlapPoint(worldPosition, layerMask))
                {
                    //GameObject trigger = new GameObject("check" + worldPosition.x + worldPosition.y);
                    //trigger.transform.localScale = new Vector3(.33f, .33f, .33f);
                    pathfinding.GetNode(x, y).SetIsWalkable(false);
                    //trigger.AddComponent<CircleCollider2D>();
                    //trigger.AddComponent<Rigidbody2D>();
                    //trigger.AddComponent<NodeCollision>();
                }
                
                //Destroy(trigger);
                //pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
            }
        }
        
    }

    private void Start()
    {
        MapWalk();
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
                    
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * .33f + Vector3.one * .165f, new Vector3(path[i + 1].x, path[i + 1].y) * .33f + Vector3.one * .165f, Color.green);
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

   

    public void SetUnwalkable(Vector3 nodeObj)
    {
        int x;
        int y;
        pathfinding.GetGrid().GetXY(nodeObj, out x, out y);
        if(x< 0 || x >= pathfinding.GetGrid().GetWidth()){return;}
        if(y < 0 || y >= pathfinding.GetGrid().GetHeight()){return;}
        //Debug.Log(x + "," + y);
        pathfinding.GetNode(x, y).SetIsWalkable(false);
        //!pathfinding.Getnode(x,y).IsWalkable
        //Destroy(nodeObj);
    }
}
