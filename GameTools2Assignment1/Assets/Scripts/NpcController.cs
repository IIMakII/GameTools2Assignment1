using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    private int currentPoint = 0;
    private bool following = false; // used to check if npc is following player or not
    private NavMeshAgent navAgent;
    private Animator anim;
    [SerializeField] GameObject target;
    [SerializeField] List<GameObject> wayPoints;

    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("following is " + following);
        followPlayer();
        followWayPoint();


    }
    private void OnTriggerEnter(Collider other) // when player enters triggers tells npc to follow player
    {
        if (other.tag == "Player")
        {
            following = true;
            
        }   
       
    }
    private void OnTriggerExit(Collider other)  // when player enters triggers tells npc to follow player
    {
        if(other.tag == "Player")
        {
            following = false;
        }
    }

    private void followPlayer()
    {
        if(following == true) // if player within range
        {
            anim.SetFloat("Speed", 0.4f); //increases speed
            navAgent.SetDestination(target.transform.position); // sets target position to the player

            if (Vector3.Distance(target.transform.position, this.transform.position) <= navAgent.stoppingDistance) // player is within stopping distance
            {
                Vector3 direction = (target.transform.position - this.transform.position).normalized; //gets the direction
                direction.y = 0;

                Quaternion rotate = Quaternion.LookRotation(direction); 
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, rotate, Time.deltaTime * 2);
                anim.SetFloat("Speed", 0f);
            }
        }
    }

    private void followWayPoint()
    {
         if (following == false)
         {
            anim.SetFloat("Speed", 0.1f);
            navAgent.SetDestination(wayPoints[currentPoint].transform.position); //set postion to the waypoint
            Debug.Log("currentPoint is" + currentPoint);

            if (Vector3.Distance(wayPoints[currentPoint].transform.position, this.transform.position) <= navAgent.stoppingDistance) // if waypoint within stopping distance chamges to next waypoint
            {
                if (currentPoint >= wayPoints.Count)
                {
                    currentPoint = (currentPoint + 1) % wayPoints.Count; // makes sure it doesnt go over lenght of list
                    Debug.Log("currentPoint is" + currentPoint);
                }
                
            }
          
         }
    }
}
