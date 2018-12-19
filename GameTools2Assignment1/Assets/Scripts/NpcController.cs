using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    private int currentPoint = 0;
    private bool following = false;
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
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            following = true;
            
        }   
       
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            following = false;
        }
    }

    private void followPlayer()
    {
        if(following == true)
        {
            anim.SetFloat("Speed", 0.4f);
            navAgent.SetDestination(target.transform.position);

            if (Vector3.Distance(target.transform.position, this.transform.position) <= navAgent.stoppingDistance)
            {
                Vector3 direction = (target.transform.position - this.transform.position).normalized;
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
            navAgent.SetDestination(wayPoints[currentPoint].transform.position);
            Debug.Log("currentPoint is" + currentPoint);

            if (Vector3.Distance(wayPoints[currentPoint].transform.position, this.transform.position) <= navAgent.stoppingDistance)
            {
                currentPoint = (currentPoint + 1) % wayPoints.Count;
                Debug.Log("currentPoint is" + currentPoint);
            }
          
         }
    }
}
