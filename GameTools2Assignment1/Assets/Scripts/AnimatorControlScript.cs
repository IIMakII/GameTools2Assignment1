using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimatorControlScript : MonoBehaviour {
    private Animator anim;
    private float speed, turn;
    private int typejump = -1, Sptypejump, wallSide;
    private bool jumping,specialjump;
    private CapsuleCollider cap; 

    
	void Start () {
        anim = GetComponent<Animator>();
        
	}
	

	void Update () {

        speed = Input.GetAxis("Vertical"); // speed controls wether stationary,walking or running, turn controls left ,straight or right 
        turn = Input.GetAxis("Horizontal");

        anim.SetFloat("Turn", turn);
        anim.SetFloat("Speed", speed);

        if (Input.GetButtonDown("Jump")) // causes player to jump and randomises the jump animation
        {
            anim.SetBool("Jump",true);
            anim.SetFloat("TypeJump", typejump);
            typejump = -typejump;  
        }

       if (Input.GetButtonUp("Jump"))
        {
            anim.SetBool("Jump", false);
        }
	}
    private void OnTriggerStay(Collider other) //if player enters jumpable object it lets animator know of this and type of jump to do 
    {
        if(other.tag == "Wall" || other.tag == "WallRun")
        {
            anim.SetBool("SpecialJump", true);
        }

        if (other.tag == "Wall")
        {
            Sptypejump = 1;
            anim.SetFloat("SpTypeJump", Sptypejump);
        }

        if (other.tag == "WallRun") // player must turn into wall to wall run
        {
            Debug.Log(turn);
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

    private void OnTriggerExit(Collider other) // turns off specal jump identifier
    {
        if (other.tag == "Wall" || other.tag == "WallRun")
        {
           anim.SetBool("SpecialJump", false);
        }
        
    }
}
