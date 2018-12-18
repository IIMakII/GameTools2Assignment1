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
            Debug.Log("IK started");

                if (anim.GetFloat("TypeJump") <= 0 && anim.GetBool("SpecialJump") == false)// IK for forward run jump
                {
                    _objectAxis = anim.GetIKPosition(AvatarIKGoal.LeftHand); //takes positon of hand without IK taking affect.
                    _objectAxis.y = _object.transform.position.y + (_object.transform.lossyScale.y / 2); // changes y value to match with top of object. doing this stops hand from aiming to center


                    anim.SetIKPosition(AvatarIKGoal.LeftHand, _objectAxis);
                    anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, anim.GetFloat("IK"));
                    Debug.Log("run forward done");

                }


            if(anim.GetBool("SpecialJump") == true && anim.GetFloat("SpTypeJump") <= 0) // IK for wall run
            {
                /* IK for left foot to hit surface */
                {
                    _objectAxis = anim.GetIKPosition(AvatarIKGoal.LeftFoot);

                    if (_object.transform.position.x > anim.GetIKPosition(AvatarIKGoal.LeftFoot).x)
                    {
                        _objectAxis.x = _object.transform.position.x - (_object.transform.lossyScale.x / 3);
                    }

                    else
                    {
                        _objectAxis.x = _object.transform.position.x + (_object.transform.lossyScale.x / 3);
                    }


                    anim.SetIKPosition(AvatarIKGoal.LeftFoot, _objectAxis);
                    anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, anim.GetFloat("IK"));
                    Debug.Log("Left Foot done");
                }

                /* IK for right foot */
                {
                    _objectAxis = anim.GetIKPosition(AvatarIKGoal.RightFoot);

                    if (_object.transform.position.x > anim.GetIKPosition(AvatarIKGoal.RightFoot).x)
                    {
                        _objectAxis.x = _object.transform.position.x - (_object.transform.lossyScale.x / 3);
                    }

                    else
                    {
                        _objectAxis.x = _object.transform.position.x + (_object.transform.lossyScale.x / 3);
                    }


                    anim.SetIKPosition(AvatarIKGoal.RightFoot, _objectAxis);
                    anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, anim.GetFloat("RightFoot"));
                    Debug.Log("Right Foot done");
                }

            }

            if(anim.GetBool("SpecialJump") == true && anim.GetFloat("SpTypeJump") >= 0)
            {
                /* IK for left foot to hit surface */
                {
                    _objectAxis = anim.GetIKPosition(AvatarIKGoal.LeftFoot);
                    if (_object.transform.position.z >= anim.GetIKPosition(AvatarIKGoal.LeftFoot).z)
                    {
                        _objectAxis.z = _object.transform.position.z - (_object.transform.lossyScale.z / 2);
                    }
                    else
                    {
                        _objectAxis.z = _object.transform.position.z + (_object.transform.lossyScale.z / 2);
                    }
                    anim.SetIKPosition(AvatarIKGoal.LeftFoot, _objectAxis);
                    anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, anim.GetFloat("IK"));
                }

                /* IK for right foot */
                {
                    _objectAxis = anim.GetIKPosition(AvatarIKGoal.RightFoot);
                    if (_object.transform.position.z >= anim.GetIKPosition(AvatarIKGoal.RightFoot).z)
                    {
                        _objectAxis.z = _object.transform.position.z - (_object.transform.lossyScale.z / 2);
                    }
                    else
                    {
                        _objectAxis.z = _object.transform.position.z + (_object.transform.lossyScale.z / 2);
                    }
                    anim.SetIKPosition(AvatarIKGoal.RightFoot, _objectAxis);
                    anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, anim.GetFloat("RightFoot"));
                }
            }


        }
    }
}
