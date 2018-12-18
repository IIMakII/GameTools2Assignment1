using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvents : MonoBehaviour {
   [SerializeField] AudioClip stepSound,jumpSound,landSound;
    private AudioSource fsource;
    private Animator anim;
    public bool enableIK = false;
    public GameObject _object;
    private float IKweight;
    Vector3  _objectAxisPosition; //used to force ik to not move to center of object but move to axis i want
   

	// Use this for initialization
	void Start () {
        fsource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
	}
	 // anim events controls when this will be played 
    private void FootSound(int scale )
    {
        if (scale > 0 ) // sound edits for right foot
        {
            fsource.pitch = 1.2f;
            fsource.panStereo = 0.5f;
        }

       if (scale < 0 ) // sound edits for left foot 
        {
            fsource.pitch = 0.8f;
            fsource.panStereo = -0.5f;
        }
        fsource.volume = 1; // resets sound volume to 0 incase it was tampered by other methods
        fsource.PlayOneShot(stepSound); // play footstep then reset edits
        fsource.pitch = 1;
        fsource.panStereo = 0;
    }

    private void JumpSound(float volume ) 
    {
        fsource.volume = volume;
        fsource.PlayOneShot(jumpSound);
    }

    private void LandSound(float volume)
    {
        fsource.volume = volume;
        fsource.PlayOneShot(landSound);
        
    }
 
    private void HandSlap(float volume)
    {
        
        fsource.volume = volume;
        fsource.PlayOneShot(stepSound);
        
    }

    private void DisableIK()
    {
        enableIK = false;
    }
   
    private void OnAnimatorIK(int layerIndex)
    {
        //if(_object != null) // makes sure object is not empty
       // {
            _objectAxisPosition = anim.GetBoneTransform(HumanBodyBones.LeftHand).position;
            _objectAxisPosition.y = _object.transform.position.y * 5; //+ _object.transform.localScale.y/ 2;  // forces hand for forward jump to be on the y.axis low wall

            anim.SetIKPosition(AvatarIKGoal.LeftHand, _objectAxisPosition);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, IKweight);
            Debug.Log("oihsd");
            /*
            if (enableIK == true)
            {
                IKweight = anim.GetFloat("IK");
                if (anim.GetFloat("TypeJump") <= 0) // ik for forward jump
                {
                    _objectAxisPosition = anim.GetBoneTransform(HumanBodyBones.LeftHand).position;
                    _objectAxisPosition.y = _object.transform.position.y + _object.transform.localScale.y / 2;  // forces hand for forward jump to be on the y.axis low wall

                    anim.SetIKPosition(AvatarIKGoal.LeftHand, _objectAxisPosition);
                    anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, IKweight);
                    Debug.Log(enableIK);
                }
            }
            */

        //}
        
    }

}
