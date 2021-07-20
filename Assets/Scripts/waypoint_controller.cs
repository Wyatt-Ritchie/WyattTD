using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class waypoint_controller : MonoBehaviour
{

    public List<Transform> waypoints = new List<Transform>();
    private Transform targetWaypoint;
    public float rotationSpeed = 4.0f;
    
    
    private int targetWaypointIndex = 0;
    private float minDistance = 0.1f;
    private int lastWaypointIndex;
    public float movementSpeed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject path = GameObject.FindGameObjectWithTag("Path");
        Transform[] pathChildren = path.GetComponentsInChildren<Transform>();
        waypoints.AddRange(pathChildren);
        targetWaypoint = waypoints[targetWaypointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        float movementStep = movementSpeed * Time.deltaTime;
        if (!checkIfDead())
        {
            if (targetWaypointIndex < waypoints.Count)
            {
                float distance = Vector3.Distance(transform.position, targetWaypoint.position);

                CheckDistanceToWaypoint(distance);

                transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, movementStep);
                float rotationStep = rotationSpeed * Time.deltaTime;

                Vector3 direction = targetWaypoint.transform.position - transform.position;
                Quaternion rotationToTarget = Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, rotationStep);
            }
            else
            {
                Debug.Log("Path Finished");
                Destroy(gameObject);
            }
        }
        
        
    }
    void CheckDistanceToWaypoint(float distance)
    {
        if (distance <= minDistance)
        {
            targetWaypointIndex++;
            updateTargetWaypoint(targetWaypointIndex);
        }
    }
    void updateTargetWaypoint(int target)
    {
        if (target < waypoints.Count)
        {
            targetWaypoint = waypoints[target];
        }
        
    }

    bool checkIfDead()
    {
        Enemy_Script enSC = gameObject.GetComponent<Enemy_Script>();
        return enSC.isDead();
    }
}
