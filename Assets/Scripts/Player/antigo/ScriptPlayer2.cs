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

    private void FixedUpdate()
    {
        //movimento

        //Mudança da orientação do movimento basiado na posição da camera
        if (CameraJogador2.posicaoJogador2 == 1)
        {
            playerMovement = new Vector3(movementInput.x, 0.0f, movementInput.y) /* * speed */;
        }
        else if (CameraJogador2.posicaoJogador2 == 2)
        {
            playerMovement = new Vector3(movementInput.y, 0.0f, movementInput.x * -1) /* * speed */;
        }
        else if (CameraJogador2.posicaoJogador2 == 3)
        {
            playerMovement = new Vector3(movementInput.x * -1, 0.0f, movementInput.y * -1)  /* * speed */;
        }
        else if (CameraJogador2.posicaoJogador2 == 4)
        {
            playerMovement = new Vector3(movementInput.y * -1, 0.0f, movementInput.x)  /* * speed */;
        }


        //playerMovement = new Vector3(movementInput.x, 0.0f, movementInput.y).normalized /* * speed */;
        //rb.velocity = new Vector3(playerMovement.x, rb.velocity.y, playerMovement.z);
        rb.MovePosition(rb.position + playerMovement * speed * Time.fixedDeltaTime);


        //Pulo
        if (jumped)
        {
            if (Physics.CheckSphere(feet.position, 0.1f, floorMask))
            {
                print("pisou2");
                rb.velocity = Vector3.up * jumpForce;

            }
        }

        //rotação
        if (playerMovement != Vector3.zero)
        {
            //Quaternion toRotation = Quaternion.LookRotation(playerMovement, Vector3.up);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            Quaternion targetRotation = Quaternion.LookRotation(playerMovement);
            targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(targetRotation);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "CaixaInteragivel")
        {
            other.gameObject.GetComponentInParent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "CaixaInteragivel")
        {
            other.gameObject.GetComponentInParent<Rigidbody>().isKinematic = false;
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

    public void OnRotateLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (CameraJogador2.posicaoJogador2 < 4)
            {
                CameraJogador2.posicaoJogador2++;
            }
            else if (CameraJogador2.posicaoJogador2 == 4)
            {
                CameraJogador2.posicaoJogador2 = 1;
            }
        }
    }

    public void OnRotateRight(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            if (CameraJogador2.posicaoJogador2 > 1)
            {
                CameraJogador2.posicaoJogador2--;
            }
            else if (CameraJogador2.posicaoJogador2 == 1)
            {
                CameraJogador2.posicaoJogador2 = 4;
            }
        }

        //rotateRight = context.action.triggered;
    }
}

