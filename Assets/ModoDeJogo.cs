using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ModoDeJogo : MonoBehaviour
{
    public static bool isMultiplayer;
    public static int qualOjogador = 1;

    public GameObject player1;
    public GameObject player2;

    PlayerInput player1Input;
    PlayerInput player2Input;

    public static bool mudanca;

    public bool a;

    // Start is called before the first frame update
    void Start()
    {
        player1Input = player1.GetComponent<PlayerInput>();
        player2Input = player2.GetComponent<PlayerInput>();


        isMultiplayer = a;
        MudancaActionMap();

    }

    // Update is called once per frame
    void Update()
    {
        print(player1Input.currentActionMap + "j1");
        print(player2Input.currentActionMap + "j2");


        if (!isMultiplayer)
        {
            UmJogador();
        }

        print(qualOjogador);
    }

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
            UmJogador();
        }

    }

    void UmJogador()
    {
        if(qualOjogador == 1)
        {
            
            player2.GetComponent<PlayerInput>().enabled = false;
            player1.GetComponent<PlayerInput>().enabled = true;

            if (mudanca)
            {
                player1Input.SwitchCurrentActionMap("OnePlayer");
                mudanca = false;
            }


        }
        else if(qualOjogador == 2)
        {
            player1.GetComponent<PlayerInput>().enabled = false;
            player2.GetComponent<PlayerInput>().enabled = true;

            if (mudanca)
            {
                player2Input.SwitchCurrentActionMap("OnePlayer");
                mudanca = false;
            }


  
        }
    }
}
