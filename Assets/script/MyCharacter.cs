using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacter : MonoBehaviour
{
    //a variable to hold the current destination of the character
    Vector3 _Destination;
    private UnityEngine.AI.NavMeshPath _path;
    List<Vector3> _simplePath = new List<Vector3>();
    public BoxCollider _Collider;
    // Start is called before the first frame update
    void Start()
    {
        //when we start, set the destination to whatever the current position is
        _Destination = transform.position;
        _path = new UnityEngine.AI.NavMeshPath();
    }
    //a function to set the target desitnation of the character
    public void SetTarget(Vector3 TargetPos)
    {
        _Destination = TargetPos;
        //find a path to the destination from our current position
        bool foundPath = UnityEngine.AI.NavMesh.CalculatePath(transform.position, TargetPos, UnityEngine.AI.NavMesh.AllAreas, _path);
        _simplePath.Clear();

        for (int i = 0; i < _path.corners.Length; i++)
        {
            Debug.Log("I: " + i + "num corners: "+_path.corners.Length);
            _simplePath.Add(_path.corners[i]);
        }

        Debug.Log("set target");
    }
    // Update is called once per frame
    void Update()
    {
        //when updating, work out the direction we need to move in

        Vector3 MoveDirection = Vector3.zero;
        if (_simplePath.Count > 0)
        {
            //remove any parts of our path that we are really close to
            Vector3 Relpos = (transform.position - _simplePath[0]);
            Relpos.y = 0;
            //remove any parts of our path that we are really close to
            while (_simplePath.Count > 0 && Relpos.magnitude < 0.5f)
            {

                _simplePath.RemoveAt(0);
                if (_simplePath.Count > 0)
                {
                    Relpos = (transform.position - _simplePath[0]);
                }
            }
        }
        while (_simplePath.Count > 0 && (transform.position - _simplePath[0]).magnitude < 0.5f)
        {
            _simplePath.RemoveAt(0);
        }
        //if there is still path to travel, calculate the direction
        if (_simplePath.Count > 0)
        {
            MoveDirection = _simplePath[0] - transform.position;
        }

        if (MoveDirection.magnitude > 0.5f)
        {
            MoveDirection.Normalize();
            //ensure the character always points upwards.
            //we do this be removing any upward components from the move direction
            //this is called "projecting"
            Vector3 ProjectedMoveDirection = MoveDirection - (Vector3.up * Vector3.Dot(Vector3.up, MoveDirection));
            transform.rotation = Quaternion.LookRotation(ProjectedMoveDirection, Vector3.up);

            //set a variable in the animation controller 
            GetComponent<Animator>().SetFloat("WalkSpeed", 3.0f);
        }
        else
        {

            //set a variable in the animation controller
            GetComponent<Animator>().SetFloat("WalkSpeed", 0.0f);

        }
        //move the character down a bit (sort of simple gravity)
        transform.position = transform.position + new Vector3(0.0f, -0.01f, 0.0f);

        //character controller collsion (dont use physics directly, only collision tests)
        //we do this because we only want character to depenatrate vertically when colliding with the floor
        //this type of collision is usually called a "character controller" 
        //great trick to manually calculate object collisions!
        Collider[] coliders = Physics.OverlapBox(transform.position, _Collider.bounds.extents);
        for (int i = 0; i < coliders.Length; i++)
        {
            if (coliders[i] != _Collider)
            {
                Vector3 hitDirection;
                float hitDistance;
                if (Physics.ComputePenetration(coliders[i], coliders[i].transform.position, coliders[i].transform.rotation, _Collider,
     _Collider.transform.position, _Collider.transform.rotation, out hitDirection, out hitDistance))
                {
                    //make the hit direction relative to the character
                    hitDirection *= -1.0f;
                    //we only want to depenatrate in the vertically direction if its a floor
                    float MinFloorDot = 0.7f;
                    if (Vector3.Dot(hitDirection, Vector3.up) > MinFloorDot)
                    {
                        Vector3 depenatrationDir = Vector3.up;
                        //increase the penatration depth accordingly
                        float denominator = Mathf.Abs(Vector3.Dot(depenatrationDir, hitDirection));
                        if (denominator > 0.0f)
                        {
                            transform.position += depenatrationDir * (hitDistance / denominator);
                        }
                    }
                    else
                    {
                        //its not the floor, depenatrate in the natural direction
                        transform.position += hitDirection * hitDistance;
                    }
                }
            }
        }
    }
}
