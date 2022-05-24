using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ModoDeJogo : MonoBehaviour
{
    public static bool isMultiplayer;
    public static int qualOjogador = 1;

    


    public PlayerInput player1_single_Input;
    public PlayerInput player2_single_Input;
    public PlayerInput player1_multi_Input;
    public PlayerInput player2_multi_Input;

    [Space]

    public GameObject personagemsSingleplayer;
    public GameObject personagemsMultiplayer;

    [Space]

    public GameObject cameraSingle;
    public GameObject cameraMulti;

    public static bool mudanca;

    public bool a;
    public bool teste;

    // Start is called before the first frame update
    void Start()
    {



        //player1.GetComponent<PlayerInput>().enabled = false;
        //player2.GetComponent<PlayerInput>().enabled = false;
        if (teste)
        {
            isMultiplayer = a;
        }
        
        // MudancaActionMap();

        personagemsMultiplayer.SetActive(false);
        personagemsSingleplayer.SetActive(false);
        cameraSingle.SetActive(false);
        cameraMulti.SetActive(false);



        if (isMultiplayer)
        {
            personagemsMultiplayer.SetActive(true);
            cameraMulti.SetActive(true);
        }
        else if(!isMultiplayer)
        {
            personagemsSingleplayer.SetActive(true);
            cameraSingle.SetActive(true);
        }

        qualOjogador = 1;
    }

    // Update is called once per frame
    void Update()
    {
        print(player1_single_Input.currentActionMap + "j1");
        print(player2_single_Input.currentActionMap + "j2");


        


        if (!isMultiplayer)
        {
            UmJogador();
        }

        print(qualOjogador);
        print("é mulijogador: " + isMultiplayer);

        //print(player1_single_Input.currentControlScheme + " Jogador 1");
        if (pausa.podePausar && !livro_interagivel.estouLendo)
        {
            Pausa();
        }
        else if (!pausa.podePausar)
        {
            Despausa();
        }

    }

    void Pausa()
    {
        if (isMultiplayer)
        {
            player1_multi_Input.enabled = false;
            player2_multi_Input.enabled = false;
        }
        else if (!isMultiplayer)
        {
            player1_single_Input.enabled = false;
            player2_single_Input.enabled = false;
        }
    }

    void Despausa()
    {
        if (isMultiplayer)
        {
            player1_multi_Input.enabled = true;
            player2_multi_Input.enabled = true;
        }
    }

    

    /*
    void MudancaActionMap()
    {
            
        if (isMultiplayer)   
        {
            player1.GetComponent<PlayerInput>().enabled = true;
            player2.GetComponent<PlayerInput>().enabled = true;

            player1Input.SwitchCurrentActionMap("TwoPlayers");
            player1Input.SwitchCurrentControlScheme("Keyboard");

            player2Input.SwitchCurrentActionMap("TwoPlayers");
            player2Input.SwitchCurrentControlScheme("Gamepad");

        } 
        else if (!isMultiplayer)
        {
            player1.GetComponent<PlayerInput>().enabled = true;
            player2.GetComponent<PlayerInput>().enabled = true;
            player1Input.SwitchCurrentActionMap("OnePlayer");
            player2Input.SwitchCurrentActionMap("OnePlayer");

            player2Input.SwitchCurrentControlScheme("Keyboard");
            UmJogador();
            qualOjogador = 1;
        }

    }
    */
    void UmJogador()
    {
        if (livro_interagivel.estouLendo)
        {
            player2_single_Input.enabled = false;
            player1_single_Input.enabled = false;
        }

        if (qualOjogador == 1 && !livro_interagivel.estouLendo)
        {
            
            player2_single_Input.enabled = false;
            player1_single_Input.enabled = true;
        }
        else if(qualOjogador == 2 && !livro_interagivel.estouLendo)
        {
            player1_single_Input.enabled = false;
            player2_single_Input.enabled = true;
        }
    }

    
}
