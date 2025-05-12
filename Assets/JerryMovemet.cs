using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class JerryMovemet : MonoBehaviour
{
    private NavMeshAgent agent; 
    private Rigidbody rb; // Reference to the Rigidbody component
    public GameObject[] waypoints;
    public float jumpForce = 500f; // Force applied when jumping
    public float speed = 3.5f; // Speed of the agent
    private int currentWaypointIndex = 0; // Index of the current waypoint
    public float landTime;
    private Animator animator; // Reference to the Animator component
    public AnimationClip jumpAnimation; // Reference to the jump animation clip
    private bool UseRbVeloc;
    private Vector3 OldVelocity;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = waypoints[0].transform.position;
        agent.speed = speed; // Set the speed of the agent
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
        animator = GetComponent<Animator>(); // Get the Animator component
        

    }

    public IEnumerator Jump()
    {
        Debug.Log("Jump");
        OldVelocity = agent.velocity;
        Debug.Log("Velocity =" + OldVelocity);
      
        agent.enabled = false; // Disable the NavMeshAgent to allow jumping
        rb.isKinematic = false; // Set Rigidbody to non-kinematic
        UseRbVeloc = true; // Set the flag to use Rigidbody velocity

        rb.AddForce(Vector3.up * jumpForce);

        yield return new WaitForSeconds(landTime); // Wait for 1 second before re-enabling the NavMeshAgent
        rb.velocity = Vector3.zero; // Reset the velocity to zero
        rb.isKinematic = true; // Set Rigidbody back to kinematic
        agent.enabled = true; // Re-enable the NavMeshAgent
        UseRbVeloc = false; // Reset the flag
        //agent.velocity = Velocity; // Restore the original velocity


    }

    
    public void JumpCoruteen()
    {
        StartCoroutine(Jump());
    }


    // Update is called once per frame
    void Update()
    {
        //agent.velocity = transform.forward * speed; // Set the initial velocity
        // Check if the agent has reached its destination

        if (UseRbVeloc)
        {
            rb.velocity = new Vector3(OldVelocity.x, rb.velocity.y, OldVelocity.z); // Preserve the x and z velocity
            Debug.Log("Rigidbody velocity =" + rb.velocity);

        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0); // Reset the x and z velocity
            if (agent.remainingDistance <= agent.stoppingDistance)
            {

                if (currentWaypointIndex <= waypoints.Length)
                {
                    currentWaypointIndex++;

                }
                else if (currentWaypointIndex >= waypoints.Length - 1)
                {
                    currentWaypointIndex = 0; // Loop back to the first waypoint
                                              //agent.destination = waypoints[currentWaypointIndex].transform.position;
                }
                agent.destination = waypoints[currentWaypointIndex].transform.position;


            }
        }

       
    }
}
