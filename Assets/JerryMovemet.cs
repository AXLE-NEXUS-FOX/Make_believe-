using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JerryMovemet : MonoBehaviour
{
    private NavMeshAgent agent; 
    public GameObject[] waypoints;
    public float speed = 3.5f; // Speed of the agent
    private int currentWaypointIndex = 0; // Index of the current waypoint
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = waypoints[0].transform.position;
        agent.speed = speed; // Set the speed of the agent
       
    }

    public void Jump()
    {
        Debug.Log("Jump");

    }

    // Update is called once per frame
    void Update()
    {
        //agent.velocity = transform.forward * speed; // Set the initial velocity
        // Check if the agent has reached its destination
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
           
            if(currentWaypointIndex <= waypoints.Length)
            {
                currentWaypointIndex++;

            }
            else if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0; // Loop back to the first waypoint
                //agent.destination = waypoints[currentWaypointIndex].transform.position;
            }
            agent.destination = waypoints[currentWaypointIndex].transform.position;

          
        }
    }
}
