using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorTelas : MonoBehaviour
{
    public static int id;

    public GameObject[] menus;

    int contador;
    public int t;
    public bool teste;

    /*
    id 0 = tela do menu
    id 1 = tela de op�oes
    id 2 = tela sele��o de modo(um jogador ou dois jogadores)
    id 3 = tela de configura��o de dispositivo(apenas para modo 2 jogadores)  
    id 4 = tutorial single player
    id 5 = tutorial multi player
    id 6 = tela loading;
     */



    // Start is called before the first frame update
    void Start()
    {
        id = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (teste)
        {
            id = t;
        }
        for (int i = 0; i < menus.Length; i++)
        {
            if (i == id)
            {
                menus[i].SetActive(true);
            }
            else
            {
                menus[i].SetActive(false);
            }
        }
    }
}
