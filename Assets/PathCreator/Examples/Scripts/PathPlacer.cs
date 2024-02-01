using PathCreation;
using UnityEngine;
using System.Collections.Generic;

namespace PathCreation.Examples {

    [ExecuteInEditMode]
    public class PathPlacer : PathSceneTool {

        public GameObject holder;
        public float spacing = 3;
        public List<Vector2> points2d = new List<Vector2>();

        const float minSpacing = .1f;

        void Generate () {
            if (pathCreator != null  && holder != null) {
                DestroyObjects ();
                points2d.Clear();

                VertexPath path = pathCreator.path;

                spacing = Mathf.Max(minSpacing, spacing);
                float dst = 0;


                while (dst < path.length) {
                    Vector3 point = path.GetPointAtDistance (dst);
                    Vector3 nextPoint = path.GetPointAtDistance(dst + spacing);
                    Quaternion rot = Quaternion.Euler(0,0,Vector3.Angle(point,nextPoint));
                    points2d.Add(new Vector2(point.x,point.y));

                    
                    //Instantiate (prefab, new Vector3(point.x,point.y,0), rot, holder.transform);
                    dst += spacing;
                }
            }
        }

        public List<Vector2> GetSpacedPoints()
        {
            return points2d;
        } 

        void DestroyObjects () {
            int numChildren = holder.transform.childCount;
            for (int i = numChildren - 1; i >= 0; i--) {
                DestroyImmediate (holder.transform.GetChild (i).gameObject, false);
            }
        }

        protected override void PathUpdated () {
            if (pathCreator != null) {
                Generate ();
            }
        }
    }
}