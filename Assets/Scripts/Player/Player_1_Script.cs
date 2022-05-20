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
    public LayerMask boxLayer;

    [Space]

    //public Animation _animation;
    public Animator _animator;
    public GameObject particulaSoco;

    [Space]

    CharacterController cc;
    GameObject _gameObject;

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
   
    //soco
    bool isPunching = false;
    bool paredeQuebravel;
    bool podeQuebrarParece = false;

    //empurrar caixa
    bool isMovingBox = false;
    bool canMoveBox;
    bool estouPuxando;
    Transform tempTransform;

    PlayerInput pa;


    Vector3 playerMovement;



    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        _gameObject = GetComponent<GameObject>();
        pa = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        //print(pa.CharacterControls);
        //Mudança da orientação do movimento baseado na posição da camera
        Orientacao_Inputs();

        //Gravidade
        Gravidade();

        //Movimento e rotação
        Movimentacao();

        //animaçoes
        //Animacoes();

        //print(playerMovement.y);
        //print(interactP1);
        //print("Caixa: " + canMoveBox);

        //Checa se o jogador esta observando uma parede (so funciona se vc estiver perto de uma)
        ChecarParede();

        
    }

    private void FixedUpdate()
    {
        Animacoes();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "ParedeQuebrada")
        {
            //print("é uma parede quebravel");
            paredeQuebravel = true;
        }

        if(other.gameObject.tag == "CaixaInteragivel")
        {
            canMoveBox = true;
            if (!isMovingBox)
            {
                StartCoroutine(EmpurrandoCaixa(other));
                tempTransform = other.gameObject.transform.parent.parent;
                print("empurrando");
            }
            else if (isMovingBox)
            {
                StartCoroutine(ParandoDeEmpurrarCaixa(other, tempTransform));
                print("posso deixar de empurrar");
                CameraJogador1.posicaoJogador1 = int.Parse(other.gameObject.name);
            }
           //print("Caixa");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ParedeQuebrada")
        {
            //print("é uma parede quebravel");
            paredeQuebravel = false;
        }

        if (other.gameObject.tag == "CaixaInteragivel")
        {
            canMoveBox = false;
            //print("nCaixa");
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
    
    
    void Orientacao_Inputs()
    {
        //Checa para ver qual é a posição da camenra no momento e ajusta o playerMovement conforme a camera.
        if (CameraJogador1.posicaoJogador1 == 1)
        {
            if (isMovingBox)
            {
                playerMovement = new Vector3(0, 0, movementInput.y) /* * speed */;

                if(playerMovement.z < 0)
                {
                    estouPuxando = true;
                }
                else
                {
                    estouPuxando = false;
                }
                
            }
            else
            {
                playerMovement = new Vector3(movementInput.x, 0, movementInput.y) /* * speed */;
            }
        }
        else if (CameraJogador1.posicaoJogador1 == 2)
        {
            if (isMovingBox)
            {
                playerMovement = new Vector3(movementInput.y, 0, 0) /* * speed */;

                if (playerMovement.x < 0)
                {
                    estouPuxando = true;
                }
                else
                {
                    estouPuxando = false;
                }
            }
            else
            {
                playerMovement = new Vector3(movementInput.y, 0, movementInput.x * -1) /* * speed */;
            }
        }
        else if (CameraJogador1.posicaoJogador1 == 3)
        {
            if (isMovingBox)
            {
                playerMovement = new Vector3(0 , 0, movementInput.y * -1)  /* * speed */;

                if (playerMovement.z > 0)
                {
                    estouPuxando = true;
                }
                else
                {
                    estouPuxando = false;
                }
            }
            else
            {
                playerMovement = new Vector3(movementInput.x * -1, 0, movementInput.y * -1)  /* * speed */;
            }
        }
        else if (CameraJogador1.posicaoJogador1 == 4)
        {
            if (isMovingBox)
            {
                playerMovement = new Vector3(movementInput.y * -1, 0, 0)  /* * speed */;

                if (playerMovement.z > 0)
                {
                    estouPuxando = true;
                }
                else
                {
                    estouPuxando = false;
                }
            }
            else
            {
                playerMovement = new Vector3(movementInput.y * -1, 0, movementInput.x)  /* * speed */;
            }
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
        if (isMovingBox)
        {
            float halfSpeed = speed * 0.5f;
            cc.Move(new Vector3(playerMovement.x * halfSpeed, playerMovement.y, playerMovement.z * halfSpeed) * Time.deltaTime);
        }
        else
        {
            cc.Move(new Vector3(playerMovement.x * speed, playerMovement.y, playerMovement.z * speed) * Time.deltaTime);
        }


        //Rotação
        if ((playerMovement.x != 0 || playerMovement.z != 0) && !isMovingBox)
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
        if(Mathf.Abs(playerMovement.x) > 0 || Mathf.Abs(playerMovement.z) > 0)
        {
            _animator.SetFloat("MoveSpeed", 1);
        }
        else if(Mathf.Abs(playerMovement.x) == 0 || Mathf.Abs(playerMovement.z) == 0)
        {
            _animator.SetFloat("MoveSpeed", 0);
        }


        //soco
        _animator.SetBool("IsPunching", isPunching);

        //Movendo Caixa
        _animator.SetBool("IsPushing", isMovingBox);
        _animator.SetBool("EstouPuxando", estouPuxando);


        //Pulo
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

    IEnumerator EmpurrandoCaixa(Collider caixa)
    {
        yield return new WaitForSeconds(0.1f);
        if (Physics.Linecast(olhos.position, olhandoDirecao.position, boxLayer))
        {
            Transform tempTransform = caixa.gameObject.transform.parent.parent; 
            if (canMoveBox && interactP1 && !isMovingBox)
            {
                caixa.gameObject.transform.parent.parent = transform;
                isMovingBox = true;
            }   
        } 
    }
   
    IEnumerator ParandoDeEmpurrarCaixa(Collider caixa , Transform temp)
    {
        yield return new WaitForSeconds(1);
        if (interactP1)
        {
            print("parei de empurrar");
            print(caixa.gameObject.transform.parent.gameObject.name);
            caixa.gameObject.transform.parent.SetParent(null);
            isMovingBox = false;
        }
    }

    void ChecarParede()
    {
        if (Physics.Linecast(olhos.position, olhandoDirecao.position, wallLayer))
        {
            podeQuebrarParece = true;
            //print("é uma parede");
        }
        else
        {
            podeQuebrarParece = false;
            //print("Não é uma parede");
        }
    }

    // inputs 
    public void OnMove(InputAction.CallbackContext context)
    {

        
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
        if (cc.isGrounded && isPunching == false && isMovingBox == false)
        {

            isJumping = true;
            moveCharacterY = jumpForce;
            //print("foi");
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

        if (podeQuebrarParece && paredeQuebravel && context.started)
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

