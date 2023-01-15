using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollowCam : MonoBehaviour
{
 //a reference to the object that we want to follow
    public GameObject _FollowObject;

 //the height and offset that we want the camera to maintain
    public Vector3 _Offset = new Vector3(-5.0f,3.0f,-5.0f);
 //a stiffness value that controls how quickly we follow the car
    public float Stiffness = 0.1f;
 // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
 //Force the camera to look directly at the GameObject
 //work out a vector pointing to the game object from the camera (this object)
    Vector3 RelativePosition = _FollowObject.transform.position- transform.position;
 //normalise the vector
    RelativePosition.Normalize();
 //set the rotation of this object (camera) to look in the direction we have calculated
    transform.rotation = Quaternion.LookRotation(RelativePosition, Vector3.up);
 //get the camera to move to the game object with
    Vector3 TargetLocation = _FollowObject.transform.position + _Offset;
 //Interpolate between the current position and the target position
 //use the stiffness value to decide how much to interpolate
    transform.position = Vector3.Lerp(transform.position, TargetLocation, Stiffness);
    }
}

