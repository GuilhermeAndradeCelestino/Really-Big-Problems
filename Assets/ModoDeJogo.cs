using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ModoDeJogo : MonoBehaviour
{
    public static bool isMultiplayer;


    public GameObject player1;
    public GameObject player2;

    PlayerInput player1Input;
    PlayerInput player2Input;

    PlayerInputManager aber;

    public InputActionMap one;

    // Start is called before the first frame update
    void Start()
    {
        player1Input = player1.GetComponent<PlayerInput>();
        player2Input = player2.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }

    private void OnEnable()
    {

    }
}
