using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField]private Transform _target;
    [SerializeField][Range(.01f,1)] private float _followSpeed;
    [SerializeField][Range(.01f,1)] private float _rotSpeed;
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,_target.position,_followSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation,_target.rotation,_rotSpeed);
    }
}
