using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    
    public Transform target;
    public float speed;
    
    public Vector3 offsetPosition;
    public Vector3 offsetRotation;

    Vector3 velocity = Vector3.zero;



    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position + offsetPosition, ref velocity, speed);
        transform.rotation = Quaternion.Euler(offsetRotation);
    }

    private void LateUpdate()
    {
        //transform.position = target.position + offset;

        //transform.position = Vector3.SmoothDamp(transform.position, target.position + offsetPosition, ref velocity, speed);
        
    }




    


}
