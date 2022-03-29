using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraS : MonoBehaviour
{
    public Transform obstruction;
    public Transform target;
    public Vector3 offset;

    Animator _animator;

    
    
    float zoomSpeed = 2f;

   

    // Start is called before the first frame update
    void Start()
    {
        obstruction = target;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;
        ViewObstrucion();

        Fade();
        

        if (Input.GetKey(KeyCode.Space))
        {
            Passagem.estaPassando = true;
        }

       

        
    }


    void Fade ()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("ComFade"))
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                _animator.SetBool("PlayFade", false);
            }
        }
        

        if (Passagem.estaPassando)
        {
            _animator.SetBool("PlayFade", true);
            Passagem.estaPassando = false;
        }
    }


    void ViewObstrucion()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, target.position - transform.position, out hit, 4.5f))
        {
            if(hit.collider.gameObject.tag != "Player")
            {
                obstruction = hit.transform;
                obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = 
                    UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;

                if(Vector3.Distance(obstruction.position, transform.position) >= 3f && 
                    Vector3.Distance(transform.position, target.position)  >= 1.5f)
                {
                    transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
                }
            }
            else
            {
                obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode =
                    UnityEngine.Rendering.ShadowCastingMode.On;
                
                if (Vector3.Distance(transform.position ,target.position) < 4.5f)
                {
                    transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
                }
            }
        }
    }
}
