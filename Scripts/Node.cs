using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node>
{
    public bool walkable;
    public Vector3 worldPosistion;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public Node parent;
    int heapIndex;


    public Node(bool _walkable, Vector3 _worldPos, int _gridx , int _gridy)
    {
        walkable = _walkable;
        worldPosistion = _worldPos;
        gridX = _gridx;
        gridY = _gridy;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
    
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
