using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PathCreation;

public class PathFinding : MonoBehaviour
{
    // Refrence to the request mangager 
    PathRequestManager requestManager;
    // Refrence to the Grid that it searches 
    Grid grid;
    // Refrence to all of the pathcreators
    List<PathCreator> pathCreators;

    // Gets all required components 
    void Awake()
    {
        grid = GetComponent<Grid>();
        requestManager = GetComponent<PathRequestManager>();
        pathCreators = Camera.main.GetComponent<RoadManager>().pathCreators;
    }


    // Method that starts and finds a path given two points 
    public void StartFindPath(Vector3 start,Vector3 end)
    {
        StartCoroutine(FindPath(start,end));
    }
    
    // Method to find a path
    IEnumerator FindPath(Vector3 startPos , Vector3 endPos)
    {
       
        // Array to store path and flag if path found 
        Vector3[] waypoints =  new Vector3[0];
        bool pathSuccess = false;
    
        // Get start node and end node from grid
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node endNode = grid.NodeFromWorldPoint(endPos);
        startNode.parent = startNode;

        // if both the startnode and endnode are walkable
        if(startNode.walkable && endNode.walkable)
        {
            // Create a new heap for the open set nodes to be evaluted 
            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            // Create new closed set nodes that have already been evaluated
            HashSet<Node> closedSet= new HashSet<Node>();
            openSet.Add(startNode);
            while(openSet.Count > 0)
            {
                // remove the current node from the open set and at to closed set
                Node currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);
                // nodes are same reached end of path
                if(currentNode == endNode)
                {
                    // path found break out of loop
                    pathSuccess = true;
                    
                    break;
                }
                // loop through each of the nodes neighbours 
                foreach(Node neighbour in grid.GetNeighbours(currentNode))
                {
                    // if the node is not walkable or is contained in the closed set can continue
                    // as already has been evaluated 
                    if(!neighbour.walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }
                    // Calculate the movement cost to the neightbour 
                    int newMovementCostToNeightbour = currentNode.gCost + GetDistance(currentNode,neighbour);
                    // if the movement cost is less than the neighbours gCost or the neighbour is not in the openset
                    if(newMovementCostToNeightbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        // set new gCost , hcost and parent
                        neighbour.gCost = newMovementCostToNeightbour;
                        neighbour.hCost = GetDistance(neighbour,endNode);
                        neighbour.parent = currentNode;

                        // neighbour is not in the openset
                        if(!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        } 
                        // found a shorter path to this neighbour
                        else
                        {
                            openSet.UpdateItem(neighbour);
                        }
                    }
                }
            }
        }
        yield return null;
        // if the path is successfull return a new path
        if(pathSuccess)
        {
            waypoints = RetracePath(startNode,endNode);
            
        }// add latest successful
        requestManager.FinishedProcessingPath(waypoints,pathSuccess);

        
    
    }
    // Method that retraces node path
    Vector3[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        //loop throough every node adding parent 
        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
            
        }
        // Method that converts nodes to vector3 path
        Vector3[] waypoints = PathtoRoad(path);
        Array.Reverse(waypoints);
        return waypoints;
        
    }
    // Method that converts nodes to vector3 path
    Vector3[] PathtoRoad(List<Node> path)
    {
        // find out how much to incrament by be length of path for performance
        int increment  = (int)MathF.Log(path.Count);
        List<Vector3> waypoints = new List<Vector3>(); 
        // loop path list 
        for(int i  = 0; i<path.Count - increment - 1; i+= increment)
        {
            // Get a world point from the Node
            Vector3 point = path[i].worldPosistion;
            //compare which road point is closest 
            Vector3 closestPoint = Vector3.positiveInfinity;
            float differnce =12800f; //randomly chosen large number
            //Compare the distance between the point and closest point on the road
            //which ever is closest is the actual point  
            foreach(PathCreator pathCreator in pathCreators)
            {
                Vector3 trialPoint = pathCreator.path.GetClosestPointOnPath(point);

                float differnceMag =(trialPoint-point).magnitude;
                if(differnceMag < differnce)
                {
                    closestPoint = trialPoint;
                    differnce = differnceMag;

                }
            }
            waypoints.Add(closestPoint);
        }
        return(waypoints.ToArray());

    }
    // Method that calculated distance between nodes useing pythatgoras Theroem
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
