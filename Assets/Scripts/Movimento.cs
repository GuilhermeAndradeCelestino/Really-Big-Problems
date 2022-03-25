using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movimento : MonoBehaviour
{
    Rigidbody rb;
    GameObject go;
    public Transform feet;
    public LayerMask floorMask;

    public int speed;
    public int jumpForce;
    public int rotationSpeed;

    Vector2 movementInput;
    bool jumped = false;

    Vector3 playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        go = GetComponent<GameObject>();
    }

    

    // Update is called once per frame
    void Update()
    {
        
        //movimento
        playerMovement = new Vector3(movementInput.x, 0.0f, movementInput.y) * speed;
        rb.velocity = new Vector3(playerMovement.x, rb.velocity.y, playerMovement.z);

        //rota��o
        if(playerMovement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(playerMovement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        

        //Pulo
        if (jumped) 
        {
            if(Physics.CheckSphere(feet.position, 0.1f, floorMask))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumped = context.action.triggered;
    }

    

}

