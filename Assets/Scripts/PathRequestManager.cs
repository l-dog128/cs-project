using System;
using System.Collections.Generic;
using UnityEngine;

public class PathRequestManager : MonoBehaviour
{
    // Queue to hold all path requests 
    Queue<PathRequest> pathRequestsQueue = new Queue<PathRequest>();
    // Refrence to current path request
    PathRequest currentPathRequest;

    static PathRequestManager instance;
    PathFinding pathFinding;
    bool isProcessingPath;

    void Awake()
    {
        instance = this;
        pathFinding = GetComponent<PathFinding>();
    }
    
    // Method called by anything wanting to request a path  
    // requires a start an end anda call back function
    public static void RequestPath(Vector3 pathStart, Vector2 pathEnd, Action<Vector3[],bool> callback)
    {
        // Create a new request add it to queue and try to process next
        PathRequest newRequest = new PathRequest(pathStart,pathEnd,callback);
        instance.pathRequestsQueue.Enqueue(newRequest);
        instance.TryProcessNext();
    }
    // Method that tries to process an item in the path requests queue
    void TryProcessNext()
    {
        // If not currently processeing path and there is an item in the queue
        if(!isProcessingPath && pathRequestsQueue.Count > 0)
        {
            // Remove first item from queue , set is findingpath to true and find a path
            currentPathRequest = pathRequestsQueue.Dequeue();
            isProcessingPath = true;
            pathFinding.StartFindPath(currentPathRequest.pathStart,currentPathRequest.pathEnd);
        }
    }
    // Method called when path has been finished  
    public void FinishedProcessingPath(Vector3[] path , bool success)
    {
        // Goes to call back function, sets is processing to false and trys to process next
        currentPathRequest.callback(path,success);
        isProcessingPath = false;
        TryProcessNext();
    }

    // Struct for a path request
    // callback used to return if path has been found and if so the path its self 
    struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[],bool> callback;
        // Constructor
        public PathRequest(Vector3 _start, Vector3 _end , Action<Vector3[],bool> _callback)
        {
            pathStart =_start;
            pathEnd = _end;
            callback = _callback;
        }
    }
}
