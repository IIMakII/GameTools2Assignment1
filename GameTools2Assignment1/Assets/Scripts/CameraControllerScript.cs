using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerScript : MonoBehaviour {
    private float xAxis, yAxis, aSpeed,baseFOF;
    private Camera cam;
    private Animator anim;
    private Transform trans;
    private Quaternion tAngle, bAngle;
    [SerializeField] private int xRange, yRange; // how many degrees the camera is allowed to move 

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
        anim = GetComponentInParent<Animator>(); 
        trans = GetComponentInChildren<Transform>();
        bAngle.x = 0;
        bAngle.y = 0;
        bAngle.z = 0;
        baseFOF = cam.fieldOfView;
    }
    // Update is called once per frame
    void Update ()
    {
        aSpeed = anim.GetFloat("Speed");
        xAxis = Input.GetAxis("CameraX"); //gets input
        yAxis = Input.GetAxis("CameraY");
        // xAxis = 1;  to test 
        tAngle.x = Mathf.Lerp(0, xRange,Mathf.Abs( xAxis)); // stores lerp value for cameramovement and add it straight to rotation;
        tAngle.y = Mathf.Lerp(0, yRange,Mathf.Abs( yAxis));
        tAngle.z = 0;

        if (xAxis <= 0) // if input was minus it make angle a minus 
        {
            tAngle.x = -tAngle.x;
        }
        
        if (yAxis <= 0)
        {
            tAngle.y = -tAngle.y;

        }
        trans.rotation = bAngle; // resets angle 
        trans.Rotate (tAngle.x, tAngle.y, tAngle.z); // does the rotation

        if (aSpeed >= 0.8f) // increase field of view while running fast 
        {
           cam.fieldOfView =  Mathf.Lerp(baseFOF, baseFOF + 10, (aSpeed - 0.8f) * 5);
        }
    }
}
