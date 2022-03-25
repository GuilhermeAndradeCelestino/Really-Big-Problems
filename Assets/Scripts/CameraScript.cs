using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public Transform target;
    public float speed;
    public Vector3 offset;

    

   

    private void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
