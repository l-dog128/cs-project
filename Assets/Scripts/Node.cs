using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node>
{
    // Is the node walkable
    public bool walkable;
    // The posistion of the node
    public Vector3 worldPosistion;
    // Its x and y posistion in the grid 
    public int gridX;
    public int gridY;

    // Its Gcost,Hcost and parent node
    public int gCost;
    public int hCost;
    public Node parent;
    // The index in the heap
    int heapIndex;

    // Constructor
    public Node(bool _walkable, Vector3 _worldPos, int _gridx , int _gridy)
    {
        walkable = _walkable;
        worldPosistion = _worldPos;
        gridX = _gridx;
        gridY = _gridy;
    }
    // Getter for fcost
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
    // Getter for HeapIndex
    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }
    // Method which compares to nodes
    public int CompareTo(Node node1)
    {
        int compare = fCost.CompareTo(node1.fCost);
        if(compare == 0)
        {
            compare = hCost.CompareTo(node1.hCost);
        }
        return -compare;
    }
}
