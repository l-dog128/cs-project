using UnityEngine;
using UnityEditor;
using System.Collections;
using PathCreation;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{
	//Refrence to target hub
	public Hub targetHub;
	// Distance from point unit begins to turn
	public float turnDst = 0.4f;
	// Speed at which unit turns 
	public float turnSpeed =1000f;
	// Acceleration of hub
	public float accerleration;
	// MaxSpeed of hub
	public float maxSpeed;
	// Current speed of unit
	public float currentSpeed;
	// The points the unit will follow
	public Vector3[] waypoints;
	// The path it will follow
	Path path;
	// Start speed of hub
	private float startSpeed = 2f;
	private List<Unit> units;
	
	// Sets units and starts OnPathFound
	void Start()
	{
		units = targetHub.units;
		OnPathFound(waypoints);

	}
	// Method that Creates a new path and starts Follow path
	void OnPathFound(Vector3[] waypoints) 
	{
		// Create a new path
		path = new Path(waypoints,transform.position,turnDst);
		//path = NewPath(waypoints);

		StopCoroutine("FollowPath");
		StartCoroutine("FollowPath");
	}
	IEnumerator FollowPath() {

		bool followingPath = true;
		int pathIndex = 0;
		//look at first point in path 
		transform.LookAt(path.lookPoints[0]);
		while (followingPath) 
		{
			// Create new posistion of unit
			Vector2 pos2d = new Vector2 (transform.position.x,transform.position.y);
			// While the unit has crossed the line between the current and next points in the path 
			while(path.turnBoundries[pathIndex].HasCrossedLine(pos2d))
			{
				// If next index is last index then break else incrament path index
				if(pathIndex == path.finishLineIndex)
				{
					followingPath = false;
					break; 
				}
				else
				{
					pathIndex ++;
				}
			}
			// if following the path 
			if(followingPath)
			{
				// calculate new speed and if it is greater than max speed set it to max Speed
				currentSpeed = NewSpeed(startSpeed,Time.deltaTime);
				if(currentSpeed > maxSpeed)
				{
					currentSpeed = maxSpeed;
				}
				// calculate the rotation of the unit , then roate it to look where it needs to go
				Quaternion targetRot = Quaternion.LookRotation(path.lookPoints[pathIndex]- transform.position);
				transform.rotation = Quaternion.Lerp(transform.rotation,targetRot,Time.deltaTime*turnSpeed);
				//move forward to its next points
				transform.Translate(Vector3.forward * currentSpeed , Space.Self);
				
				// reset its z coordinate
				transform.position = new Vector3(transform.position.x,transform.position.y,-0.1f);
			}
			// else has reached end 
			else
			{
				//remove from list and destory the game object
				units.Remove(this);
				Destroy(gameObject);
			}


			yield return null;

		}
	}

	// Method which returns the current speed 
	public float GetCurrentSpeed()
	{
		return currentSpeed;
	} 

	// Used in inspector to see the path the unit will take 
	void OnDrawGizmosSelected()
	{
		for (int i = 1; i < path.lookPoints.Length; i++)
		{
			Gizmos.DrawLine(path.lookPoints[i-1],path.lookPoints[i]);
		}
	}
	// Method to calculate new speed using SUVAT formaula v = u + at 
	private float NewSpeed(float currentSpeed ,float time )
	{
		return (currentSpeed + accerleration * time);
	}
}