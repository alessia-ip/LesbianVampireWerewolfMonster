using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class testing : MonoBehaviour
{
    // Start is called before the first frame update
    static Pathfinding pathfinding;
    private GetMouseWorld mouse;
    public GameObject player;
    public List<PathNode> path;
    private int pathNum;
    public LayerMask layerMask;

    public Animator playerAnimator;


    public bool charHover = false;
    public Vector2 charXY;
    
    public float speed = 2.5f;
    
    public GameObject gridStart;
    public int gWidth;
    public int gHeight;
    public float cellSize;

    public bool canWalk = true;

    public GameObject pingPos;
    
    private void Start()
    {
        //playerAnimator = GameObject.FindWithTag("playerAnimator").GetComponent<Animator>();
        pathfinding = new Pathfinding(gWidth, gHeight, gridStart.transform.position, cellSize);
        var gridhelp = pathfinding.GetGrid();
        int x, y;
        gridhelp.GetXY(player.transform.position, out x, out y);
        player.transform.position = gridhelp.GetWorldPosition(x, y);
        pingPos.transform.position = player.transform.position;
        MapWalk();
    }

    
    
    //could regenerate grid on changing rooms <- 
    public void playerPosFix()
    {
        var gridhelp = pathfinding.GetGrid();
        int x, y;
        gridhelp.GetXY(player.transform.position, out x, out y);
        player.transform.position = gridhelp.GetWorldPosition(x, y);
    }

    public void MapWalk()
    {
        //pathfinding = new Pathfinding(gWidth, gHeight, gridStart.transform.position, cellSize); //this is where we make the new grid
        mouse = GetComponent<GetMouseWorld>();
        var grid = pathfinding.GetGrid();
        int testx;
        int testy;
        Debug.Log(grid.GetWorldPosition(7,11));
        grid.GetXY(grid.GetWorldPosition(7,11),  out testx, out testy);
        Debug.Log(testx +","+ testy);
        for (var y = 0; y < grid.GetHeight(); y++)
        {
            //Debug.Log(grid.GetHeight());
            for (var x = 0; x < grid.GetWidth(); x ++)
            {
                //var grid = pathfinding.GetGrid();
                Vector2 worldPosition = grid.GetWorldPosition(x, y);
                if (Physics2D.OverlapPoint(worldPosition, layerMask))
                {
                    //Debug.Log("Hit a collider - set to unwalkable: " + pathfinding.GetNode(x,y));
                    //GameObject trigger = new GameObject("check" + worldPosition.x + worldPosition.y);
                    //trigger.transform.localScale = new Vector3(.33f, .33f, .33f);
                    pathfinding.GetNode(x, y).SetIsWalkable(false);
                    //trigger.AddComponent<CircleCollider2D>();
                    //trigger.AddComponent<Rigidbody2D>();
                    //trigger.AddComponent<NodeCollision>();
                }
                else
                {
                    pathfinding.GetNode(x, y).SetIsWalkable(true);
                }
                
                //Destroy(trigger);
                //pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
            }
        }
        
    }

  

    private void Update()
    {
        var grid = pathfinding.GetGrid();

        if (Input.GetMouseButtonDown(0) && canWalk == true)
        {

            pathNum = 1;
            int x = 0;
            int y = 0;
            int px;
            int py;
            
            if (charHover == true)
            {
                grid.GetXY(charXY, out x, out y);
            }
            else
            {
                grid.GetXY(mouse.worldPosition, out x, out y);
            }
            
            grid.GetXY(player.transform.position, out px, out py);
            //Debug.Log(px + " ,"  + py); //playerPosition
            

            path = pathfinding.FindPath(px, py, x, y);
            if (path != null)
            {
                pingPos.SetActive(true);
                var lastNode = path [path.Count- 1];
                var lastNodepos = grid.GetWorldPosition(lastNode.x, lastNode.y);
                pingPos.transform.position = new Vector3(lastNodepos.x,lastNodepos.y, pingPos.transform.position.z);
                for (int i = 0; i < path.Count - 1; i++)
                {
                    //draw path
                    //Debug.DrawLine(new Vector3(path[i].x, path[i].y) * .33f + Vector3.one * .165f, new Vector3(path[i + 1].x, path[i + 1].y) * .33f + Vector3.one * .165f, Color.green);
                }
            }
        }



        if (canWalk == false)
        {
            //Debug.Log("Hit the canvas");
        }
        
        if (path != null && path.Count > 1)
        {
            
           // m_Animator.SetBool("Jump", false);
                playerAnimator.SetBool("Walking", true);


                var nextNode = path[pathNum];
                var nextNodepos = grid.GetWorldPosition(nextNode.x, nextNode.y);
                player.transform.position =
                    Vector3.MoveTowards(
                        player.transform.position,
                        nextNodepos,
                        1f * Time.deltaTime * speed);

            
  
                var xDif = player.transform.position.x - nextNodepos.x;
                var yDif = player.transform.position.y - nextNodepos.y;
                
                if (xDif < 0) // this is going right
                {
                    playerAnimator.SetBool("WalkingRight", true);
                }
                else if(xDif > 0)//this is going left
                {
                    playerAnimator.SetBool("WalkingRight", false);
                }

                if (yDif < 0) // this is going up
                {
                    playerAnimator.SetBool("WalkingUp", true);
                }
                else if (yDif >0)//this is going down
                {
                    playerAnimator.SetBool("WalkingUp", false);
                }

                if (player.transform.position == nextNodepos && pathNum != path.Count - 1)
                {
                    pathNum++;

                }else if (player.transform.position == nextNodepos && pathNum == path.Count - 1)
                {
                    playerAnimator.SetBool("Walking", false);
                    path.Clear();
                    pingPos.SetActive(false);
                }


        
        }

        canWalk = true;
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
