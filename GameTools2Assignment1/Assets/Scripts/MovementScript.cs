using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementScript : MonoBehaviour {
    private Animator anim;
    private float speed, turn;
    private int typejump = -1, Sptypejump, wallSide;
    public bool jumping,specialjump;
    
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
            anim.SetTrigger("Jump");
            anim.SetFloat("TypeJump", typejump);
            typejump = -typejump;  
        }

       
	}
    private void OnTriggerEnter(Collider other) //if player enters jumpable object it lets animator know of this and type of jump to do 
    {

        if(other.tag == "Wall")
        {
            anim.SetBool("SpecialJump", true);
            Sptypejump = 1;
            anim.SetFloat("SpTypeJump", Sptypejump);
        }

        if(other.tag == "WallRunLeft")
        {
            anim.SetBool("SpecialJump", true);
            Sptypejump = -1;
            wallSide = -1;
            anim.SetFloat("SpTypeJump", Sptypejump);
            anim.SetFloat("WallSide", wallSide);

        }

        if (other.tag == "WallRunRight")
        {
            anim.SetBool("SpecialJump", true);
            Sptypejump = -1;
            wallSide = 1;
            anim.SetFloat("SpTypeJump", Sptypejump);
            anim.SetFloat("WallSide", wallSide);

        }
    }

    private void OnTriggerExit(Collider other) // turns off specal jump identifier
    {
        anim.SetBool("SpecialJump", false);
    }
}
