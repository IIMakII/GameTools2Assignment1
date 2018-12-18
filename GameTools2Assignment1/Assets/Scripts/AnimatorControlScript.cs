using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimatorControlScript : MonoBehaviour {
    private Animator anim;
    private Rigidbody rb;
    private CapsuleCollider cap;
    private float speed, turn, time= 0, capHeight;
    Vector3  capCenter,newCapCenter; // stores acutal cap value and the change
    private int typejump = -1, Sptypejump, wallSide;
    private bool specialjump, toAdjust = false;
    
    
	void Start () {
        anim = GetComponent<Animator>();
        cap = GetComponent<CapsuleCollider>();
        rb = GetComponentInParent<Rigidbody>();
        capHeight = cap.height;
        capCenter = cap.center;
        newCapCenter = cap.center;
        newCapCenter.y = newCapCenter.y +60; //stores the value i want to change cap center to

        
	}
	

	void Update () {
        time = time + 1 * Time.deltaTime; // counts time
        speed = Input.GetAxis("Vertical"); // speed - controls wether stationary,walking or running, turn - controls left ,straight or right 
        turn = Input.GetAxis("Horizontal");

        anim.SetFloat("Turn", turn); // puts theses values into the animator (speed and turn)
        anim.SetFloat("Speed", speed);

        if (Input.GetButtonDown("Jump")) // causes player to jump and randomises the jump animation and allows player to jump over objects by controling rb gravity usage and capsule config 
           
        {
            toAdjust = true; // states player pressed the jump button
            time = 0;   // resets time
            rb.useGravity = false;

            anim.SetBool("Jump",true);
            GetComponent<AnimEvents>().enableIK = true; // allows ik to be used 
            anim.SetFloat("TypeJump", typejump);
            typejump = -typejump;
            
            
        }
        if (time >= .2 && toAdjust == true)  //reduces size and center of capsule collider to allow to go over objects while jumping 
        {
            toAdjust = false;
            cap.height = capHeight / 2;
            cap.center = newCapCenter;  
        }

        if (time > 1)
        {
            cap.height = capHeight;
            cap.center = capCenter;
            rb.useGravity = true;
           
        }

        if (Input.GetButtonUp("Jump"))
        {
            anim.SetBool("Jump", false);
        }
	}
    private void OnTriggerStay(Collider other) //if player enters jumpable object it lets animator know of this and type of jump to do 
    {
        GetComponent<AnimEvents>()._object = other.gameObject; // so ik will know the other object

        if(other.tag == "Wall" || other.tag == "WallRun")
        {
            anim.SetBool("SpecialJump", true);
        }

        if (other.tag == "Wall")
        {
            Sptypejump = 1;
            anim.SetFloat("SpTypeJump", Sptypejump);
        }

        if (other.tag == "WallRun") // player must turn into wall to wall run //blend tree was changed to use turn intstead  to fix bug but i still cant get it to work yet 
        { 
           
            Sptypejump = -1;
            anim.SetFloat("SpTypeJump", Sptypejump);
            if(turn >= 0.01f)
            {
               
                wallSide = -1;
                anim.SetFloat("WallSide",wallSide);
            }

            if (turn <= -0.01)
            {
                
                wallSide = 1;
                anim.SetFloat("WallSide",wallSide);
            }

            else
            {
                anim.SetBool("SpecialJump", false);
            }

        }
    }

    private void OnTriggerExit(Collider other) // turns off special jump identifier when  player leaves area 
    {
        GetComponent<AnimEvents>()._object = null; // so ik forgets object
        

        if (other.tag == "Wall" || other.tag == "WallRun")
        {
           anim.SetBool("SpecialJump", false);
        }
        
    }
  
  
}
