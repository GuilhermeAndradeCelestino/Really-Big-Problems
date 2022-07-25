using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Vitoria : MonoBehaviour
{

    /*
     0 = P1 single
     1 = p2 single
     2 = p1 multi
     3 = p2 multi
     
     */

    public Animator[] animacoes;
    public GameObject telaVitoria;
    public GameObject botao;
    public static bool ganhou;


    // Start is called before the first frame update
    void Start()
    {
        ganhou = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player_1_Script.vitoriaP1)
        {
            if (ModoDeJogo.isMultiplayer)
            {
                animacoes[2].SetBool("Victory", true);
            }
            else if(!ModoDeJogo.isMultiplayer)
            {
                
                ModoDeJogo.qualOjogador = 2;
                animacoes[0].SetBool("Victory", true);
            }
        }
        else if (Player_2_Script.vitoriaP2)
        {
            if (ModoDeJogo.isMultiplayer)
            {
                animacoes[3].SetBool("Victory", true);
            }
            else if(!ModoDeJogo.isMultiplayer)
            {
                
                ModoDeJogo.qualOjogador = 1;
                animacoes[1].SetBool("Victory", true);
            }
        }

        if(Player_1_Script.vitoriaP1 && Player_2_Script.vitoriaP2)
        {
            ganhou = true;
            telaVitoria.SetActive(true);
            EventSystem.current.SetSelectedGameObject(botao);
        }
    }

}
