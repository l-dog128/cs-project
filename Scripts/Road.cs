using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using PathCreation.Examples;
public class Road : MonoBehaviour

{
    public Material material;
    public PathCreator pathCreator;
    public PathPlacer pathPlacer;
    public MeshCollider meshCollider;
    public RoadMeshCreator roadMeshCreator;
    public bool connectedHub;
    bool isClosed = false;
    float timeSinceCreation = 0.25f;
     
    public RoadData roadData;
    public List<Vector2> points;
    void Start()
    {
        List<PathCreator> pathCreators = Camera.main.GetComponent<RoadManager>().pathCreators;

        meshCollider = GetComponent<MeshCollider>(); 
        roadMeshCreator = GetComponent<RoadMeshCreator>();
        pathCreator = GetComponent<PathCreator>();
        pathPlacer = GetComponent<PathPlacer>();

        
        

        //set settings 


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
        
        
        //check if connected to a hub if so set first and second point at hub and makes sure there isnt already a first ponint
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
        if(roadData.isSelected == true)
        {
            if(roadData.points.Count > 1)
            {
                pathCreator.bezierPath = bezierPath(roadData.points.ToArray(),isClosed);
                UpdateMeshCollider();
            }


            StopCoroutine(NewRoadPoint());
            StartCoroutine(NewRoadPoint());
            //change points to points 2d if doesnt work 
            points = roadData.points;
            roadData.spacedPoints = GetSpacedPoints();
        }
        
    }
    public List<Vector2> GetSpacedPoints()
    {
        return pathPlacer.GetSpacedPoints();
    }

    IEnumerator NewRoadPoint()
    {
        if(Input.GetMouseButtonUp(0))
        {
            Vector3 screenPos = Input.mousePosition;
            var pos = Camera.main.ScreenToWorldPoint(screenPos);
            if(!(pos.x < -62f || (pos.x > 55f && pos.y < -42f)))
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

    public BezierPath bezierPath(Vector2[] points, bool isClosed )
    {
        return new BezierPath(points,isClosed,PathSpace.xy);
    }

    void UpdateMeshCollider()
    {
        meshCollider.sharedMesh = roadMeshCreator.mesh;

    }

}
