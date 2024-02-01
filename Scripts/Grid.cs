using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Grid : MonoBehaviour
{
    public bool displayGrid;
    public LayerMask walkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;

    float nodeDiameter;
    int gridSizeX;
    int gridSizeY;
    
    Vector2[] points;

    void Awake()
    {
        nodeDiameter = nodeRadius *2;
        gridSizeY = Mathf.RoundToInt(Camera.main.orthographicSize*2f/nodeDiameter);
        gridSizeX = Mathf.RoundToInt(gridSizeY*Camera.main.aspect/nodeDiameter);
        gridWorldSize = new Vector2(gridSizeX,gridSizeY);
        CreateGrid();
    }

    void Update()
    {
        CreateGrid();
    }

    public int MaxSize
    {
        get
        {
            return gridSizeX *gridSizeY;
        }
    }
    void CreateGrid()
    {
        grid = new Node[gridSizeX,gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x/2 - Vector3.up * gridWorldSize.y/2;;
        for( int x = 0 ; x<gridSizeX ; x++)
        {
            for(int y = 0 ; y< gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius  ) + Vector3.up * (y * nodeDiameter + nodeRadius);
                //Vector3 worldPoint = worldBottomLeft + Vector3.right * x  + Vector3.up * y;
                
                bool walkable = false;
                grid[x,y] = new Node(walkable,worldPoint,x,y);
            }
        }
        SetWalkable();
    }

    public void SetWalkable()
    {
        points = Camera.main.GetComponent<RoadManager>().points.ToArray();
            foreach(Vector2 point in points)
            {
                Node node = NodeFromWorldPoint(point);
                node.walkable = true;    
            }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for(int x = -1 ; x <= 1 ; x++)
        {
            for(int y = -1 ; y <= 1 ; y++)
            {
                if(x == 0 && y == 0)
                {
                    continue;
                }
                int checkX = node.gridX + x;
                int checky = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checky >= 0 && checky< gridSizeY )
                {
                    neighbours.Add(grid[checkX,checky]);
                }
                
            }
        }
        return neighbours;
    }

    public Node NodeFromWorldPoint(Vector3 WorldPosistion)
    {
        float percentX = (WorldPosistion.x + gridWorldSize.x/2)/gridWorldSize.x;
        float percentY = (WorldPosistion.y + gridWorldSize.y/2)/gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY-1) * percentY);

        return grid[x,y];
    }



    [ContextMenu("node count")]
    void NodeCount()
    {
        Debug.Log(grid.Length);
    }

    void OnDrawGizmos()
    {
        
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y,0.2f));


        if(grid != null&& displayGrid)
        {   
            
            foreach(Node n in grid)
            {
                Gizmos.color = (n.walkable)?Color.white:Color.red;
                Gizmos.DrawCube(n.worldPosistion,Vector3.one*(nodeDiameter-.1f));
            }
        }
    }
}
