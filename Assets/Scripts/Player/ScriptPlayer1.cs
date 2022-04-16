using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScriptPlayer1 : MonoBehaviour
{
    Rigidbody rb;
    GameObject go;

    public Transform feet;
    public Transform olhandoDirecao;
    public Transform olhos;

    [Space]

    public LayerMask floorMask;
    public LayerMask wallLayer;

    [Space]

    public Animation _animation;

    [Space]

    public float speed;
    public int jumpForce;
    public int rotationSpeed;

    Vector2 movementInput;
    bool jumped = false;
    public static bool interactP1 = false;
    bool rotateLeft = false;
    bool rotateRight = false;
    float originalSpeed;

    bool isMoving;
    bool isJumping;
    bool isMovingBox;
    bool isPunching = false;


    
    Vector3 playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        go = GetComponent<GameObject>();
        originalSpeed = speed;

        _animation["Punch"].wrapMode = WrapMode.Once;
    }



    // Update is called once per frame
    void Update()
    {
        // muda a velocidade para o valor padrão caso o jogdor não esteja movendo a caixa 
        if (!isMovingBox)
        {
            speed = originalSpeed;
        }
        
        

        //animations
        Actions();

        if (!isMoving && !isJumping)
        {
            _animation.CrossFade("Breathing Idle");
        }
        else if (isMoving && !isJumping)
        {
            _animation.CrossFade("Running");
        }
        
        if (isPunching)
        {
            StartCoroutine(Punch());
        }
        
        if (Physics.CheckSphere(feet.position, 0.1f, floorMask) == false)
        {
            isJumping = true;
            _animation.CrossFade("Jump");
        }
        else
        {
            isJumping = false;
        }

        //rotaçiona a camera (sentido horario)
        if (rotateLeft == true)
            {
                if (CameraJogador1.posicaoJogador1 < 4)
                {
                    CameraJogador1.posicaoJogador1++;
                }
                else if (CameraJogador1.posicaoJogador1 == 4)
                {
                    CameraJogador1.posicaoJogador1 = 1;
                }

            }

            //(rotaçiona a camera (sentido anti-horario))
            if (rotateRight == true)
            {
                if (CameraJogador1.posicaoJogador1 > 1)
                {
                    CameraJogador1.posicaoJogador1--;
                }
                else if (CameraJogador1.posicaoJogador1 == 1)
                {
                    CameraJogador1.posicaoJogador1 = 4;
                }
            }
        
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        //checa se esta colidindo com uma caixa que pode ser movida
        if (collision.gameObject.tag == "CaixaInteragivel")
        {
            //diminui a velocidade do jogador e aplica velocidade na caixa 
            float speedDiminuida = 1f;
            isMovingBox = true;
            speed = speedDiminuida;
            var pushDir = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            collision.collider.attachedRigidbody.velocity = pushDir;
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "ParedeQuebrada")
        {
            if (interactP1 && Physics.Linecast(olhos.position, olhandoDirecao.position, wallLayer))
            {
                isPunching = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "CaixaInteragivel")
        {
            //indica que o jogdor paou de mover a caixa
            isMovingBox = false;
        }
    }

    private void FixedUpdate()
    {
        //movimento

        //Mudança da orientação do movimento basiado na posição da camera
        if(CameraJogador1.posicaoJogador1 == 1)
        {
            playerMovement = new Vector3(movementInput.x, 0.0f, movementInput.y) /* * speed */;
        }
        else if(CameraJogador1.posicaoJogador1 == 2)
        {
            playerMovement = new Vector3(movementInput.y , 0.0f, movementInput.x * -1) /* * speed */;
        }
        else if (CameraJogador1.posicaoJogador1 == 3)
        {
            playerMovement = new Vector3(movementInput.x * -1 , 0.0f, movementInput.y * -1)  /* * speed */;
        }
        else if (CameraJogador1.posicaoJogador1 == 4)
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


    IEnumerator Punch()
    {
        //Indica para o scrit da parede que ele deve ser começado
        Parede_quebrada.comecaQuebrar = true;
        //inicia a animação
        _animation.CrossFade("Punch");
        
        yield return new WaitForSeconds(1.3f);

        //indica que o personagem parou de bater
        isPunching = false;
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
        //trava os controle caso o jogador esteja socando
        if (isPunching == false)
        {
            movementInput = context.ReadValue<Vector2>();
        }
        

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        //trava os controle caso o jogador esteja socando
        if (isPunching == false)
        {
            jumped = context.action.triggered;
        }
        
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        interactP1 = context.action.triggered;
        
    }

    public void OnRotateLeft(InputAction.CallbackContext context)
    {
        //trava os controle caso o jogador esteja socando
        if (context.performed && isPunching == false)
        {
            if (CameraJogador1.posicaoJogador1 < 4)
            {
                CameraJogador1.posicaoJogador1++;
            }
            else if (CameraJogador1.posicaoJogador1 == 4)
            {
                CameraJogador1.posicaoJogador1 = 1;
            }
        }
        
        //rotateLeft = context.action.triggered;
        //rotateLeft = context.started;
    }

    public void OnRotateRight(InputAction.CallbackContext context)
    {
        //trava os controle caso o jogador esteja socando
        if (context.performed && isPunching == false)
        {
            if (CameraJogador1.posicaoJogador1 > 1)
            {
                CameraJogador1.posicaoJogador1--;
            }
            else if (CameraJogador1.posicaoJogador1 == 1)
            {
                CameraJogador1.posicaoJogador1 = 4;
            }
        }
   



        //rotateRight = context.action.triggered;
        //rotateRight = context.started;
    }
}
