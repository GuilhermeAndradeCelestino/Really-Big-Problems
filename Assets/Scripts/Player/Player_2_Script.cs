using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_2_Script : MonoBehaviour
{

    [Space]

    public LayerMask floorMask;
    public LayerMask wallLayer;

    [Space]

    public Animator _animator;


    [Space]

    CharacterController cc;
    GameObject _gameObject;

    public float speed;
    public float jumpForce;
    public float gravity;
    public float rotationSpeed;

    [Space]
    public GameObject orbeP2;

    float rotationVelocity;
    float moveCharacterY;

    Vector2 movementInput;

    public static bool interactP2 = false;

    float originalSpeed;

    bool isMoving;
    bool isJumping;



    public static bool vitoriaP2;


    Vector3 playerMovement;

    //puzzle sequencia colorido
    bool estouPertoDoBotao = false;
    bool isBlue = false;
    bool isRed = false;
    bool isYellow = false;
    bool isGreen = false;


    // Start is called before the first frame update
    void Start()
    {
        vitoriaP2 = false;
        cc = GetComponent<CharacterController>();
        _gameObject = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //print("vitoriaP2 é: " + vitoriaP2);
        //Mudança da orientação do movimento baseado na posição da camera
        Orientacao_Inputs();

        //Gravidade
       Gravidade();

        if (!vitoriaP2)
        {
            //Movimento e rotação
            Movimentacao();
        }

        //Indica qual o personagem o jogador esta usando no momento
        if (!ModoDeJogo.isMultiplayer)
        {
            IndicadorSinglePlayer();
        }

    }

    private void FixedUpdate()
    {
        Animacoes();

        /*
        Gravidade();

        if (!vitoriaP2)
        {
            Movimentacao();
        }
        */
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Vitoria")
        {
            vitoriaP2 = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Passagem")
        {
            if(other.gameObject.name == "Portal_A" )
            {
                PassagemPlayer(true);
            }

            if (other.gameObject.name == "Portal_B")
            {
                PassagemPlayer(false);
            }
        }

        if (other.gameObject.tag == "Botao")
        {
            ApertaBotao();
        }

        if (other.gameObject.tag == "LivroInteragivel")
        {
            other.gameObject.GetComponent<livro_interagivel>().mostrarMensagem = true;

            if (interactP2)
            {
                livro_interagivel.estouLendo = true;
            }
        }

        if (other.gameObject.tag == "PortalTrocaA" || other.gameObject.tag == "PortalTrocaB")
        {
            Portal_Troca.P2EstaPronto = true;
        }

        if (other.gameObject.tag == "Botao_ReiniciarSequencia")
        {
            estouPertoDoBotao = true;
        }

        bool a = true;
        PuzzleSequenciaCor(other, a);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "LivroInteragivel")
        {
            other.gameObject.GetComponent<livro_interagivel>().mostrarMensagem = false;
        }

        if (other.gameObject.tag == "PortalTrocaA" || other.gameObject.tag == "PortalTrocaB")
        {
            Portal_Troca.P2EstaPronto = false;
        }

        if (other.gameObject.tag == "Botao_ReiniciarSequencia")
        {
            estouPertoDoBotao = false;
        }

        bool a = false;
        PuzzleSequenciaCor(other, a);
    }

    void Orientacao_Inputs()
    {
        //Checa para ver qual é a posição da camenra no momento e ajusta o playerMovement conforme a camera.
        if (CameraJogador2.posicaoJogador2 == 1)
        {

                playerMovement = new Vector3(movementInput.x, 0, movementInput.y) /* * speed */;

        }
        else if (CameraJogador2.posicaoJogador2 == 2)
        {

                playerMovement = new Vector3(movementInput.y, 0, movementInput.x * -1) /* * speed */;

        }
        else if (CameraJogador2.posicaoJogador2 == 3)
        {

                playerMovement = new Vector3(movementInput.x * -1, 0, movementInput.y * -1)  /* * speed */;

        }
        else if (CameraJogador2.posicaoJogador2 == 4)
        {

                playerMovement = new Vector3(movementInput.y * -1, 0, movementInput.x)  /* * speed */;

        }

    }

    void Gravidade()
    {

        moveCharacterY -= gravity * Time.deltaTime;

        //moveCharacterY -= gravity * Time.fixedDeltaTime;
        //playerMovement.y -= gravity;
        playerMovement.y = moveCharacterY;

        if (moveCharacterY < -gravity)
        {
            moveCharacterY = -gravity;
        }
    }

    void Movimentacao()
    {

        //Movimento
        //cc.Move(playerMovement * speed * Time.deltaTime);
        cc.Move(new Vector3(playerMovement.x * speed, playerMovement.y, playerMovement.z * speed) * Time.deltaTime);

        //cc.Move(new Vector3(playerMovement.x * speed, playerMovement.y, playerMovement.z * speed) * Time.fixedDeltaTime);

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
        if (Mathf.Abs(playerMovement.x) > 0 || Mathf.Abs(playerMovement.z) > 0)
        {
            _animator.SetFloat("MoveSpeed", 1);
        }
        else if (Mathf.Abs(playerMovement.x) == 0 || Mathf.Abs(playerMovement.z) == 0)
        {
            _animator.SetFloat("MoveSpeed", 0);
        }
        
        if (vitoriaP2)
        {
            _animator.SetFloat("MoveSpeed", 0);
        }

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
            if ((isJumping && playerMovement.y < 7) || playerMovement.y < 0)
            {
                _animator.SetBool("IsFalling", true);
            }
        }

    }

    void PassagemPlayer(bool a_ou_b)
    {
        // a = true , b = false
        if (interactP2 && a_ou_b)
        {
            Passagem.comecarPassagem = true;
            Passagem.a_To_b = true;
        }
        else if (interactP2 && !a_ou_b)
        {
            Passagem.comecarPassagem = true;
            Passagem.b_To_A = true;
        }
    }

    void ApertaBotao()
    {
        if (interactP2)
        {
            BotaoCena1.apertouBotao = true;
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


    void IndicadorSinglePlayer()
    {
        if (ModoDeJogo.qualOjogador == 2)
        {
            orbeP2.SetActive(true);
        }
        else
        {
            orbeP2.SetActive(false);
        }
    }
    // inputs 
    public void OnMove(InputAction.CallbackContext context)
    {
        //movementInput = context.ReadValue<Vector2>();
        if(vitoriaP2 == false)
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
        if (cc.isGrounded && vitoriaP2 == false)
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
            interactP2 = true;
        }
        else if (context.performed == false)
        {
            interactP2 = false;
        }

        if (estouPertoDoBotao && context.started)
        {
            puzzle_sequencia_com_cor.reiniciar = true;
        }

        PuzzleInformarBotao(context.started);
    }

    public void OnRotateLeft(InputAction.CallbackContext context)
    {
        //trava os controle caso o jogador esteja socando
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

        //rotateLeft = context.action.triggered;
        //rotateLeft = context.started;
    }

    public void OnRotateRight(InputAction.CallbackContext context)
    {
        //trava os controle caso o jogador esteja socando
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
        //rotateRight = context.started;
    }

    public void ChangeCharacter(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            print("foi");
            ModoDeJogo.mudanca = true;
            ModoDeJogo.qualOjogador = 1;
        }
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (pausa.podePausar == false && context.performed && !livro_interagivel.estouLendo)
        {
            pausa.podePausar = true;
        }
        else if (pausa.podePausar == true && context.performed && !livro_interagivel.estouLendo)
        {
            pausa.podePausar = false;
        }
    }
}
