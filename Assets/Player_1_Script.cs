using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_1_Script : MonoBehaviour
{

    

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

    [Space]

    CharacterController cc;

    public float speed;
    public float jumpForce;
    public float gravity;
    public float rotationSpeed;
    
    float rotationVelocity;
    float moveCharacterY;

    Vector2 movementInput;
    
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
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //Mudança da orientação do movimento baseado na posição da camera
        Orientacao();

        //Gravidade
        Gravidade();

        //Movimento e rotação
        Movimentacao();

        //animaçoes
        //Animacoes();

        print(playerMovement.y);
    }

    private void FixedUpdate()
    {
        Animacoes();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "ParedeQuebrada")
        {
            if (Physics.Linecast(olhos.position, olhandoDirecao.position, wallLayer))
            {
                podeQuebrarParece = true;
                print("é uma parede quebravel");
            }
        }
    }

    

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "ParedeQuebrada")
        {
            if (Physics.Linecast(olhos.position, olhandoDirecao.position, wallLayer))
            {
                podeQuebrarParece = true;
                print("é uma parede quebravel");
            }
        }
    }

    IEnumerator Punch()
    {
        isPunching = true;

        //Indica para o scrit da parede que ele deve começar quebrar a parede
        Parede_quebrada.comecaQuebrar = true;
        //inicia a animação
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
        podeQuebrarParece = false;
        print("1");
        Instantiate(particulaSoco, maoSoco.position, maoSoco.rotation);
    }
    
    
    void Orientacao()
    {
        //Checa para ver qual é a posição da camenra no momento e ajusta o playerMovement conforme a camera.
        if (CameraJogador1.posicaoJogador1 == 1)
        {
            playerMovement = new Vector3(movementInput.x, 0, movementInput.y) /* * speed */;
        }
        else if (CameraJogador1.posicaoJogador1 == 2)
        {
            playerMovement = new Vector3(movementInput.y, 0, movementInput.x * -1) /* * speed */;
        }
        else if (CameraJogador1.posicaoJogador1 == 3)
        {
            playerMovement = new Vector3(movementInput.x * -1, 0, movementInput.y * -1)  /* * speed */;
        }
        else if (CameraJogador1.posicaoJogador1 == 4)
        {
            playerMovement = new Vector3(movementInput.y * -1, 0, movementInput.x)  /* * speed */;
        }

    }

    void Gravidade()
    {

        moveCharacterY -= gravity * Time.deltaTime;
        //playerMovement.y -= gravity;
        playerMovement.y = moveCharacterY;

        if(moveCharacterY < -gravity)
        {
            moveCharacterY = -gravity;
        }
    }

    void Movimentacao()
    {

        //Movimento
        //cc.Move(playerMovement * speed * Time.deltaTime);
        cc.Move(new Vector3(playerMovement.x * speed, playerMovement.y , playerMovement.z * speed) * Time.deltaTime);

        //Rotação
        if (playerMovement.x != 0 || playerMovement.z != 0)
        {
            float targetAngle = Mathf.Atan2(playerMovement.x, playerMovement.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationVelocity, rotationSpeed);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }

    void Animacoes()
    {
        //Indica para o animator qual animação deve ser tocada 

        //Animação de movimento
        _animator.SetBool("IsRunning", isMoving);

        //soco
        _animator.SetBool("IsPunching", isPunching);

        //Pulo

        //Controles da mina
        /*
        if (isJumping) 
        {
            _animator.SetBool("IsJumping", true);
        }


        if (cc.isGrounded)
        {
            _animator.SetBool("IsGrounded", true);
            
            _animator.SetBool("IsJumping", false);
            isJumping = false;

            _animator.SetBool("IsFalling", false);
        }
        else
        {
            _animator.SetBool("IsGrounded", false);

            if ((isJumping && playerMovement.y < 7) || playerMovement.y < 0) 
            {
                _animator.SetBool("IsFalling", true);
            }
        }
        */

        //minha verção

        //checa se estou pulando
        _animator.SetBool("IsGrounded", cc.isGrounded);

        if (isJumping)
        {
            _animator.SetBool("IsJumping", true);
        }

        //checa se estou pisando no chão
        if (cc.isGrounded)
        {
            //indica que não estou pulando e que não estou caindo 
            _animator.SetBool("IsJumping", false);
            isJumping = false;
            _animator.SetBool("IsFalling", false);
        }

        

        if (!cc.isGrounded)
        {
            if((isJumping && playerMovement.y < 7) || playerMovement.y < 0)
            {
                _animator.SetBool("IsFalling", true);
            }
        }

    }

   
    

    // inputs 
    public void OnMove(InputAction.CallbackContext context)
    {

        isMoving = context.action.triggered;
        //movementInput = context.ReadValue<Vector2>();
        //trava os controle caso o jogador esteja socando
        
        if (isPunching == false)
        {
            movementInput = context.ReadValue<Vector2>();
        }
        

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        //trava os controle caso o jogador esteja socando
       /* if (isPunching == false)
        {
            isJumping = context.action.triggered;
            print("Pulou");
        }
       */

        //Jump
        if (cc.isGrounded && isPunching == false)
        {

            isJumping = true;
            moveCharacterY = jumpForce;
            print("foi");
        }

    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed == true)
        {
            interactP1 = true;
        }
        else if (context.performed == false)
        {
            interactP1 = false;
        }

        if (podeQuebrarParece && context.started)
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

