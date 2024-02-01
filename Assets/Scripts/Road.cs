using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using PathCreation.Examples;
public class Road : MonoBehaviour

{
    // Refrence to RoadMaterial
    public Material material;
    // Refrence to PathCreator and PathPlacer
    public PathCreator pathCreator;
    public PathPlacer pathPlacer;
    // Refrence to MeshCreator
    public RoadMeshCreator roadMeshCreator;
    // Is the roadCloses
    bool isClosed = false;
    // How long user must wait before new point can be added
    float timeSinceCreation = 0.25f;
    // The road data attached to this road
    public RoadData roadData;

    [ContextMenu("GetNpoints")]
    void N()
    {
        Debug.Log(roadData.points.Count);
    }
    void Start()
    {
        // Get list of pathCreators
        List<PathCreator> pathCreators = Camera.main.GetComponent<RoadManager>().pathCreators;
        //Add Components
        roadMeshCreator = GetComponent<RoadMeshCreator>();
        pathCreator = GetComponent<PathCreator>();
        pathPlacer = GetComponent<PathPlacer>();

        // Set settings 
        pathCreators.Add(pathCreator);

        pathPlacer.spacing = 0.9f;
        pathPlacer.holder = new GameObject("RoadSegmentHolder");
        pathPlacer.holder.transform.SetParent(this.gameObject.transform);

        roadMeshCreator.roadWidth = 1f;
        roadMeshCreator.thickness = 0.5f;
        roadMeshCreator.textureTiling = 2f;
        roadMeshCreator.flattenSurface = true;
        roadMeshCreator.roadMaterial = material;
        roadMeshCreator.undersideMaterial = material;
        
        
        // Check if connected to a hub if so set first and second point at hub and makes sure there isnt already a first ponint
        if(roadData.ConnectedHub == true)
        {
            Vector2 firstPoint = roadData.FirstPoint;
            try
            {
                if(roadData.points[0] == firstPoint)
                {
                }
            }
            catch
            {
                roadData.points.Add(firstPoint);
                roadData.points.Add(new Vector2(firstPoint.x+0.1f,firstPoint.y+0.1f));
            }
            
        }
        
    }

    void Update()
    {
        // If the road is selcected And has 2 or more points create a new bezier path
        if(roadData.isSelected == true)
        {
            if(roadData.points.Count > 1)
            {
                pathCreator.bezierPath = bezierPath(roadData.points.ToArray(),isClosed);
            }

            // Check if new point has been added 
            StopCoroutine(NewRoadPoint());
            StartCoroutine(NewRoadPoint());
            // Gets the spaced points from bezier path
            roadData.spacedPoints = GetSpacedPoints();
        }
        
    }
    // Method that gets the spaced points 
    public List<Vector2> GetSpacedPoints()
    {
        return pathPlacer.GetSpacedPoints();
    }

    // Method that checks if left mouse button has been clicked 
    IEnumerator NewRoadPoint()
    {
        if(Input.GetMouseButtonUp(0))
        {
            Vector3 screenPos = Input.mousePosition;
            var pos = Camera.main.ScreenToWorldPoint(screenPos);
            // Convert Mouse pos to screenpos
            // check that posistion isnt on one of the buttons if not add a new point 
            if(!((pos.x < -47 && pos.y > 40) || (pos.x > 50 && pos.y < -41) ||(Camera.main.GetComponent<RoadManager>().HubActive && pos.x < -47)))
            {
                roadData.points.Add(new Vector2(pos.x,pos.y));
                yield return new WaitForSeconds(timeSinceCreation);
            }
            else
            {
                roadData.isSelected = false;
                yield return null;
            }
            
        }
        else
        {
            yield return null;
        }
    }
    // method that creates a new bezier path
    public BezierPath bezierPath(Vector2[] points, bool isClosed )
    {
        return new BezierPath(points,isClosed,PathSpace.xy);
    }
}
