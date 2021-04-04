using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;
    
    private Grid<PathNode> grid;
    private List<PathNode> openList;
    private List<PathNode> closedList;
    public Vector3 screenOffset;
    
    public Pathfinding(int width, int height)
    {
        screenOffset = new Vector3(10,-5,0);
        grid = new Grid<PathNode>(width, height, .5f, screenOffset,
            (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y));
    }

    public Grid<PathNode> GetGrid()
    {
        return grid;
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = grid.GetGridObject(startX, startY);
        PathNode endNode = grid.GetGridObject(endX, endY);
        
        openList = new List<PathNode>{ startNode };
        closedList = new List<PathNode>();
        
        for (int x = 0; x < grid.GetWidth(); x++) //get all nodes in the grid and calculate the cst
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                PathNode pathNode = grid.GetGridObject(x, y);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null; 
            }
        }

        //we added this so it fuckin' works B) 
        //TODO PRIORITIZE NODES CLOSER TO THE PLAYER?
        if (!endNode.isWalkable)
        {
            for (int i = 0; !endNode.isWalkable; i++)
            {
                foreach (PathNode neighbourNode in GetNeighbourList(endNode, i))
                {
                    if (closedList.Contains(neighbourNode)) continue;
                    if (neighbourNode.isWalkable)
                    {
                        endNode = neighbourNode;
                        break;
                    } 
                }
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {

            
            PathNode currentNode = GetLowestFCostNode(openList);
            if (currentNode == endNode)
            {
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighbourNode in GetNeighbourList(currentNode, 1))
            {

                   if (closedList.Contains(neighbourNode)) continue;
                   if (!neighbourNode.isWalkable)
                   {
                       closedList.Add(neighbourNode);
                       continue;
                   } //this determines if the node is not walkable - ao you can't walk through colliders

                   int tentGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                   if (tentGCost < neighbourNode.gCost)
                   {
                       neighbourNode.cameFromNode = currentNode;
                       neighbourNode.gCost = tentGCost;
                       neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                       neighbourNode.CalculateFCost();

                       if (!openList.Contains(neighbourNode))
                       {
                           openList.Add(neighbourNode);
                       }
                   }
            }
        }


        // Debug.Log("Completed");
        //out of nodes on the open list
        return null;
    }

    private List<PathNode> GetNeighbourList(PathNode currentNode, int nodeOffset)
    {
        List<PathNode> neighbourList = new List<PathNode>();
        
        if (currentNode.x - nodeOffset >= 0)
        {
            //Left
            neighbourList.Add(GetNode(currentNode.x - nodeOffset, currentNode.y));
            //Left Down
            if (currentNode.y - nodeOffset >= 0) neighbourList.Add(GetNode(currentNode.x - nodeOffset, currentNode.y - nodeOffset));
            //Left Up
            if (currentNode.y + nodeOffset < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x - nodeOffset, currentNode.y + nodeOffset));
        }

        if (currentNode.x + nodeOffset < grid.GetWidth())
        {
            //Right
            neighbourList.Add(GetNode(currentNode.x + nodeOffset, currentNode.y));
            //Right Down
            if (currentNode.y - nodeOffset >= 0) neighbourList.Add(GetNode(currentNode.x + nodeOffset, currentNode.y - nodeOffset));
            //Right UP
            if (currentNode.y + nodeOffset < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x + nodeOffset, currentNode.y + nodeOffset));
        }

        //Down
        if (currentNode.y - nodeOffset >= 0) neighbourList.Add(GetNode(currentNode.x, currentNode.y - nodeOffset));
        //Up
        if (currentNode.y + nodeOffset < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x, currentNode.y + nodeOffset));

        return neighbourList;
    }

    public PathNode GetNode(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }

   

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }

    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }

        return lowestFCostNode;
    }
}
