using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerScript : MonoBehaviour {
    private float xAxis, yAxis;
    private Transform trans;
    private Quaternion tAngle, bAngle;
    [SerializeField] private int xRange, yRange; // how many degrees the camera is allowed to move 

    private void Start()
    {
        trans = GetComponentInChildren<Transform>();
        bAngle.x = 0;
        bAngle.y = 0;
        bAngle.z = 0;
    }
    // Update is called once per frame
    void Update () {

        xAxis = Input.GetAxis("CameraX"); //gets input
        yAxis = Input.GetAxis("CameraY");
        //xAxis = 1;  to test 
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
        trans.rotation = tAngle; // does the rotation
    }
}
