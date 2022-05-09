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
    public Transform maoSoco;

    [Space]

    public LayerMask floorMask;
    public LayerMask wallLayer;

    [Space]

    //public Animation _animation;
    public Animator _animator;
    public GameObject particulaSoco;
    public GameObject teste;
    [Space]

    public float speed;
    public int jumpForce;
    public int rotationSpeed;

    Vector2 movementInput;
    bool jumped = false;
    public static bool interactP1 = false;
    
    float originalSpeed;

    bool isMoving;
    bool isJumping;
    bool isMovingBox;
    bool isPunching = false;
    
    
    bool podeQuebrarParece = false;
    
    
    Vector3 playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        go = GetComponent<GameObject>();
        originalSpeed = speed;

        //_animation["Punch"].wrapMode = WrapMode.Once;
    }



    // Update is called once per frame
    void Update()
    {
        // muda a velocidade para o valor padr�o caso o jogdor n�o esteja movendo a caixa 
        if (!isMovingBox)
        {
            speed = originalSpeed;
        }
        
        
        
        //animations
        Actions();

        if (!isMoving && !isJumping && !isPunching)
        {
            //_animation.CrossFade("Breathing Idle");

        }
        else if (isMoving && !isJumping)
        {
           
        }

        _animator.SetBool("isRunning", isMoving);
        //teste.transform.position = feet.position;
        

        if (Physics.CheckSphere(feet.position, 0.1f, floorMask) == false)
        {
            isJumping = true;
            
        }
        else
        {
            isJumping = false;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        //checa se esta colidindo com uma caixa que pode ser movida
        if (collision.gameObject.tag == "CaixaInteragivel")
        {

                float speedDiminuida = 1f;
                isMovingBox = true;
                speed = speedDiminuida;
                var pushDir = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                //collision.collider.attachedRigidbody.velocity = pushDir;
                collision.gameObject.transform.Translate(pushDir);


        }
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "ParedeQuebrada")
        {
            if (Physics.Linecast(olhos.position, olhandoDirecao.position, wallLayer))
            {
                podeQuebrarParece = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "CaixaInteragivel")
        {
            //indica que o jogador paou de mover a caixa
            isMovingBox = false;
            
            //podeEmpurrarCaixa = false;
        }

        if(collision.gameObject.tag == "ParedeQuebrada")
        {
            podeQuebrarParece = false;
        }
    }

   


    private void FixedUpdate()
    {
        //movimento

        //Mudan�a da orienta��o do movimento basiado na posi��o da camera
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

        //rota��o
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
        isPunching = true;
        
        //Indica para o scrit da parede que ele deve ser come�ado
        Parede_quebrada.comecaQuebrar = true;
        //inicia a anima��o
        //_animation.CrossFade("Punch");
        
        yield return new WaitForSeconds(1.3f);
        /* int numero = 1;
        if(numero == 1)
        {
            Instantiate(particulaSoco, maoSoco.position, maoSoco.rotation);
            numero--;
        }
        */
        
        //indica que o personagem parou de bater
        isPunching = false;
        print("1");
        Instantiate(particulaSoco, maoSoco.position, maoSoco.rotation);
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

        isMoving = context.action.triggered;
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
        if (context.performed == true)
        {
            interactP1 = true;
        }
        else if(context.performed == false)
        {
            interactP1 = false;
        }
        
        if(podeQuebrarParece && context.started)
        {
            StartCoroutine(Punch());
        }
        
        
        
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
