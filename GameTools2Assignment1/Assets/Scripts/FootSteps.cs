﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour {
   [SerializeField] AudioClip stepSound,jumpSound,landSound;
    private AudioSource fsource;

	// Use this for initialization
	void Start () {
        fsource = GetComponent<AudioSource>();
	}
	 
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
        fsource.volume = 1;
        fsource.PlayOneShot(stepSound); // play footstep then reset edits
        fsource.pitch = 1;
        fsource.panStereo = 0;
    }

    private void JumpSound(float volume )
    {
        fsource.volume = volume;
        fsource.PlayOneShot(jumpSound);
    }

    private void LandSound(float volume = 1)
    {
        fsource.volume = volume;
        fsource.PlayOneShot(landSound);
        
    }
 
    private void HandSlap(float volume = 1)
    {
        
        fsource.volume = volume;
        fsource.PlayOneShot(stepSound);
        //fsource.SetScheduledEndTime(1);
    }

}
