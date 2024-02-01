using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class PathFinding : MonoBehaviour
{
    PathRequestManager requestManager;
    Grid grid;

    public Transform start;
    public Transform end;
    [ContextMenu("is there a path?")]
    void IsThereAPath() => StartFindPath(start.position,end.position);

    void Awake()
    {
        grid = GetComponent<Grid>();
        requestManager = GetComponent<PathRequestManager>();
    }



    public void StartFindPath(Vector3 start,Vector3 end)
    {
        StartCoroutine(FindPath(start,end));
    }
    IEnumerator FindPath(Vector3 startPos , Vector3 endPos)
    {
       

        Vector3[] waypoints =  new Vector3[0];
        bool pathSuccess = false;
        
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node endNode = grid.NodeFromWorldPoint(endPos);
        startNode.parent = startNode;

        if(startNode.walkable && endNode.walkable)
        {
            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedSet= new HashSet<Node>();
            openSet.Add(startNode);

            while(openSet.Count > 0)
            {
                Node currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);
                if(currentNode == endNode)
                {
                    pathSuccess = true;
                    
                    break;
                }

                foreach(Node neighbour in grid.GetNeighbours(currentNode))
                {
                    if(!neighbour.walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeightbour = currentNode.gCost + GetDistance(currentNode,neighbour);
                    if(newMovementCostToNeightbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeightbour;
                        neighbour.hCost = GetDistance(neighbour,endNode);
                        neighbour.parent = currentNode;


                        if(!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        } 
                        else
                        {
                            openSet.UpdateItem(neighbour);
                        }
                    }
                }
            }
        }
        yield return null;
        if(pathSuccess)
        {
            waypoints = RetracePath(startNode,endNode);
            
        }// add latest successful
        requestManager.FinishedProcessingPath(waypoints,pathSuccess);

        
    
    }

    Vector3[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
            
        }
        //Vector3[] waypoints = SimplfyPath(path);
        Vector3[] waypoints = MegaChadPath(path);
        Array.Reverse(waypoints);
        return waypoints;
        
    }
    Vector3[] MegaChadPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        foreach(Node node in path)
        {
            waypoints.Add(node.worldPosistion);
        }
        return waypoints.ToArray();
    }

    Vector3[] SimplfyPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 previousDirection = Vector2.zero;

        for(int i = 1 ; i< path.Count ;i ++)
        {
            Vector2 newDirection = new Vector2(path[i-1].gridX-path[i].gridX,path[i-1].gridY-path[i].gridY);
            if(newDirection != previousDirection)
            {
                waypoints.Add(path[i].worldPosistion);
            }
            previousDirection = newDirection;
        }
        return waypoints.ToArray();

    }

    int GetDistance(Node nodeA , Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if(dstX > dstY)
        {
            return 14*dstY + 10*(dstX -dstY); // pythagorthas therorm x 10 to change from float to int 
        }
        return 14*dstX + 10*(dstY -dstX);
    }
}
