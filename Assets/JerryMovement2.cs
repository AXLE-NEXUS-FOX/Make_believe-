using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JerryMovement2 : MonoBehaviour
{
   public GameObject[] waypoints;
    public float speed = 3.5f; // Speed of the agent
    private int currentWaypointIndex = 0; // Index of the current waypoint
    private Rigidbody rb;
    private GameObject targetWaypoint;

    private void Start()
    {
        // Set the initial position to the first waypoint
        
        rb = GetComponent<Rigidbody>();
    }

  

   
}

