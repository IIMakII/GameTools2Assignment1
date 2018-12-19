using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour {
   [SerializeField] AudioClip stepSound,jumpSound,landSound;
    private AudioSource fsource;

    // script used for events
	// Use this for initialization
	void Awake () {
        fsource = GetComponent<AudioSource>();
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

}
