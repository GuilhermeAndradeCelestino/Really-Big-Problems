using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_1_Script : MonoBehaviour
{
    //travar o player
    public static bool travaPlayer = false;

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

    [Space]
    



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

    GameObject parede;
    bool terminouOsoco = false;
    bool umaVezSoco = true;

    


    //empurrar caixa
    bool isMovingBox = false;
    bool canMoveBox;
    bool estouPuxando;
    Transform tempTransform;

    public static bool vitoriaP1;

    //puzzle sequencia cor
    bool estouPertoDoBotao = false;
    bool isBlue = false;
    bool isRed = false;
    bool isYellow = false;
    bool isGreen = false;


    //puzle porta cena 3
    int respostaAtual = -1;
    Collider porta;

    Vector3 playerMovement;



    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        _gameObject = GetComponent<GameObject>();
        vitoriaP1 = false;
    }

    // Update is called once per frame
    void Update()
    {
        print(estouPertoDoBotao + " P1");
        //print(pa.CharacterControls);
        //Mudança da orientação do movimento baseado na posição da camera
        Orientacao_Inputs();
        //print(paredeQuebravel);
        //Gravidade
        Gravidade();

        if (!vitoriaP1 && !Mecanica_Troca_Script.trocaTroca && !Puzzle_porta.transicao )
        {
            //Movimento e rotação
            Movimentacao();
        }


        //animaçoes
        //Animacoes();

        //print(playerMovement.y);
        //print(interactP1);
        //print("Caixa: " + canMoveBox);

        //Checa se o jogador esta observando uma parede (so funciona se vc estiver perto de uma)
        ChecarParede();

       
        

        if (terminouOsoco)
        {
            parede.SetActive(false);
            umaVezSoco = true;
            terminouOsoco = false;
        }
        
    }

    private void FixedUpdate()
    {
        Animacoes();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Vitoria")
        {
            vitoriaP1 = true;
        }

        
    }

    private void OnTriggerStay(Collider other)
    {
        porta = other;
        if (other.gameObject.tag == "ParedeQuebrada")
        {
            other.gameObject.GetComponent<Parede_quebrada>().enabled = true;
            parede = other.gameObject;
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

        if(other.gameObject.tag == "LivroInteragivel")
        {
            other.gameObject.GetComponent<livro_interagivel>().mostrarMensagem = true;

            if (interactP1)
            {
                livro_interagivel.estouLendo = true;
            }
        }

        if(other.gameObject.tag == "PortalTrocaA")
        {
            print("foi1");
            Mecanica_Troca_Script.player1A_pronto = true;
        }
        else if (other.gameObject.tag == "PortalTrocaB")
        {
            print("foi2");
            Mecanica_Troca_Script.player1B_pronto = true;
        }


        if(other.gameObject.tag == "Botao_ReiniciarSequencia")
        {
            estouPertoDoBotao = true;
        }
        

        

        bool a = true;
        PuzzleSequenciaCor(other, a);

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ParedeQuebrada")
        {
            other.gameObject.GetComponent<Parede_quebrada>().enabled = false;
            //print("é uma parede quebravel");
            paredeQuebravel = false;

        }

        if (other.gameObject.tag == "CaixaInteragivel")
        {
            canMoveBox = false;
            //print("nCaixa");
        }

        if (other.gameObject.tag == "LivroInteragivel")
        {
            other.gameObject.GetComponent<livro_interagivel>().mostrarMensagem = false;
        }


        


        if (other.gameObject.tag == "Botao_ReiniciarSequencia")
        {
            estouPertoDoBotao = false;
        }

        bool a = false;
        PuzzleSequenciaCor(other, a);
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
        paredeQuebravel = false;

        yield return new WaitForSeconds(5f);
        terminouOsoco = true;
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
        
        if (vitoriaP1)
        {
            _animator.SetFloat("MoveSpeed", 0);
        }
        
        if (travaPlayer)
        {
            _animator.SetFloat("MoveSpeed", 0);
            playerMovement.x = 0;
            playerMovement.z = 0;
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


    void PuzzleSequenciaCor(Collider _collider, bool verdadeOUfalso)
    {
        if (_collider.gameObject.tag == "Piso_Azul")
        {
            isBlue = verdadeOUfalso;
        }
        else if (_collider.gameObject.tag == "Piso_Verde")
        {
            isGreen = verdadeOUfalso;
        }
        else if (_collider.gameObject.tag == "Piso_Vermelho")
        {
            isRed = verdadeOUfalso;
        }
        else if (_collider.gameObject.tag == "Piso_Amarelo")
        {
            isYellow = verdadeOUfalso;
        }
    }

    void PuzzleInformarBotao(bool input)
    {
        if (isRed && input)
        {
            puzzle_sequencia_com_cor.placaVermelho = true;
        }
        else if (isYellow && input)
        {
            puzzle_sequencia_com_cor.placaAmarelo = true;
        }
        else if (isGreen && input)
        {
            puzzle_sequencia_com_cor.placaVerde = true;
        }
        else if (isBlue && input)
        {
            puzzle_sequencia_com_cor.placaAzul = true;
        }
    }

    void PuzzleCena3Porta(Collider other, bool bora)
    {
        if (other.gameObject.tag == "Cena3_Sala1")
        {
            if (other.gameObject.name == "Porta0" && bora)
            {
                other.GetComponent<Salas>().MandaResposta(0);
            }
            else if (other.gameObject.name == "Porta1" && bora)
            {
                other.GetComponent<Salas>().MandaResposta(1);
            }
            else if (other.gameObject.name == "Porta2" && bora)
            {
                other.GetComponent<Salas>().MandaResposta(2);
            }
            else if (other.gameObject.name == "Porta3" && bora)
            {
                other.GetComponent<Salas>().MandaResposta(3);
            }
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
        if (cc.isGrounded && isPunching == false && isMovingBox == false && vitoriaP1 == false)
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

        if (podeQuebrarParece && paredeQuebravel && context.started && umaVezSoco)
        {
            StartCoroutine(Punch());
            umaVezSoco = false;
        }

        if (estouPertoDoBotao && context.started && cc.isGrounded)
        {
            puzzle_sequencia_com_cor.reiniciar = true;
            estouPertoDoBotao = false;
        }

        PuzzleInformarBotao(context.started);
        PuzzleCena3Porta(porta, context.started);
    }

    public void OnRotateLeft(InputAction.CallbackContext context)
    {
        //trava os controle caso o jogador esteja socando
        if (context.performed && isPunching == false && !travaPlayer)
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
        if (context.performed && isPunching == false && !travaPlayer)
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


    public void ChangeCharacter(InputAction.CallbackContext context)
    {
        if (context.performed && !isPunching && !travaPlayer)
        {
            ModoDeJogo.mudanca = true;
            ModoDeJogo.qualOjogador = 2;
            QualOplayer_Single.MudarCamera = true;
        }
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (pausa.podePausar == false && context.performed && !livro_interagivel.estouLendo && !travaPlayer)
        {
            pausa.podePausar = true; 
        }
        else if (pausa.podePausar == true && context.performed && !livro_interagivel.estouLendo && !travaPlayer)
        {
            pausa.podePausar = false;
        }
    }
}

