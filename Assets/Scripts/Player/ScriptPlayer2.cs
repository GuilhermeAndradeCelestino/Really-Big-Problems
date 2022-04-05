using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ScriptPlayer2 : MonoBehaviour
{
    Rigidbody rb;
    GameObject go;
    public Transform feet;
    public LayerMask floorMask;
    public Animation _animation;
    
    
    public int speed;
    public int jumpForce;
    public int rotationSpeed;

    Vector2 movementInput;
    bool jumped = false;
    public static bool interactP2 = false;


    bool isMoving;
    bool isJumping;
    


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

        //rotação
        if (playerMovement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(playerMovement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        if (Physics.CheckSphere(feet.position, 0.1f, floorMask))
        {
            print("pisou2");

        }

        //Pulo
        if (jumped)
        {
            if (Physics.CheckSphere(feet.position, 0.1f, floorMask))
            {
                print("pisou2");
                rb.velocity = Vector3.up * jumpForce;

            }
        }


        //animations

        Actions();

        if (!isMoving)
        {
            _animation.CrossFade("Offensive Idle");
        }
        else if(isMoving)
        {
            _animation.CrossFade("Running");
        }
    }


    
    void Actions()
    {
        if (playerMovement == new Vector3(0f, 0f, 0f))
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }

        isJumping = jumped;

    }


    // inputs 
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumped = context.action.triggered;
        print("apertou2");
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        interactP2 = context.action.triggered;
    }
}
