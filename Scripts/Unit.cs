using UnityEngine;
using System.Collections;
using PathCreation;
using System.Collections.Generic;

public class Unit : MonoBehaviour {
	const float MinTimeForNewPath = 0.2f;
	const float MinimumMoveDst = 0.5f;

	public Hub targetHub;
	public float startSpeed = 1f;
	public float accerleration = 0.2f;
	public float turnDst = 4f;
	public float turnSpeed =8f;
	float currentSpeed;
	public Transform target;
	Path PussyPath;
	private float initialaztionTime;
	private float ditanceTravelled;
	List <PathCreator> pathCreators;
	void Start()
	{
		pathCreators = Camera.main.GetComponent<RoadManager>().pathCreators;
		initialaztionTime = Time.timeSinceLevelLoad;
		StartCoroutine(UpdatePath());
	}

	IEnumerator UpdatePath()
	{
		if(Time.timeSinceLevelLoad < 0.3f)
		{
			yield return new WaitForSeconds(0.3f);
		}
		PathRequestManager.RequestPath(transform.position,target.position,OnPathFound);

		float sqrMoveDist =  MinimumMoveDst * MinimumMoveDst;
		Vector3 oldTargetPos = target.position;
		while (true)
		{
			yield return new WaitForSeconds(MinTimeForNewPath);
			if((target.position-oldTargetPos).sqrMagnitude>sqrMoveDist)
			{
				PathRequestManager.RequestPath(transform.position,target.position,OnPathFound);
				oldTargetPos = target.position;
			}

		}
	}


    [ContextMenu("find path")]
	void Findpath() {
		PathRequestManager.RequestPath(transform.position,target.position, OnPathFound);
	}

	void OnPathFound(Vector3[] waypoints, bool pathSuccessful) {

		
		if (pathSuccessful) {
			PussyPath = new Path(waypoints,transform.position,turnDst);
			//path = NewPath(waypoints);
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
		else
		{
			Debug.Log("bing");
			Destroy(gameObject,0);
		}
	}

	Vector3[] NewPath(Vector3[] oldPath)
	{
		List<Vector3> newPath = new List<Vector3>(); 
		
		foreach(Vector3 point in oldPath)
		{
			Vector3 UpdatedPoint = Vector3.positiveInfinity;

			foreach(PathCreator pathCreator in pathCreators)
			{
				Vector3 P1 = pathCreator.path.GetClosestPointOnPath(point);

				Vector3 Differnce = P1 - point;
				if(Differnce.magnitude <  UpdatedPoint.magnitude)
				{
					UpdatedPoint = P1;
				}
			}
			newPath.Add(UpdatedPoint);
		}
		
		return newPath.ToArray();
	}
	
	IEnumerator FollowPath() {

		bool followingPath = true;
		int pathIndex = 0;
		transform.LookAt(PussyPath.lookPoints[0]);
		while (followingPath) {
			Vector2 pos2d = new Vector2 (transform.position.x,transform.position.y);
			while(PussyPath.turnBoundries[pathIndex].HasCrossedLine(pos2d))
			{
				if(pathIndex == PussyPath.finishLineIndex)
				{
					followingPath = false;
					break; 
				}
				else
				{
					pathIndex ++;
				}
			}
			if(followingPath)
			{
				float newSpeed = NewSpeed(startSpeed,Time.deltaTime);
				Quaternion targetRot = Quaternion.LookRotation(PussyPath.lookPoints[pathIndex]- transform.position);
				transform.rotation = Quaternion.Lerp(transform.rotation,targetRot,Time.deltaTime*turnSpeed);
				transform.Translate(Vector3.forward * newSpeed , Space.Self);
				transform.position = new Vector3(transform.position.x,transform.position.y,-0.1f);
				ditanceTravelled += (Mathf.Pow(newSpeed,2)-Mathf.Pow(startSpeed,2))/(2*accerleration);
			}
			else
			{
				targetHub.AddTargetSpeed(ditanceTravelled/(Time.timeSinceLevelLoad- initialaztionTime));
				Destroy(gameObject);
			}


			yield return null;

		}
	}

	private float NewSpeed(float currentSpeed ,float time )
	{
		return (currentSpeed + accerleration * time);
	}
}