using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKScript : MonoBehaviour
{
    private Animator anim;
    public GameObject _object;
    public bool enableIK = false;
    private Vector3 _objectAxis;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
 
    private void DisableIK()
    {
        enableIK = false;
        Debug.Log(enableIK);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if(enableIK == true)
        {
            if (anim.GetFloat("TypeJump") <= 0) // IK for forward run
            {
                _objectAxis = anim.GetIKPosition(AvatarIKGoal.LeftHand); //takes positon of hand without IK taking affect.
                _objectAxis.y = _object.transform.position.y + (_object.transform.localScale.y/2); // changes y value to match with top of object. doing this stops hand from aiming to center


                anim.SetIKPosition(AvatarIKGoal.LeftHand, _objectAxis);
                anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, anim.GetFloat("IK"));

            }
            Debug.Log("IK started");
        }
    }
}
