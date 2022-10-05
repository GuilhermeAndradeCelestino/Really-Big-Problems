using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Mecanica_Troca_Script : MonoBehaviour
{

    AudioSource _audioSource;

    public Transform pontoA;
    public Transform pontoB;


    [Space]
    [Space]

    // Posição 0: player 1 || Posição 1: player 2
    public GameObject[] playersSingle;
    public GameObject[] playersMulti;
    
    [Space]
    [Space]

    public Animator fade;
    public float esperar_o_fadein_acabar = 1;
    public float tempoComTelaEscura = 2;
    public float esperar_o_fadeOut_acabar = 1.5f;
    int indicador = 0;

    
    int contadorTroca = 1;

    public static bool player1A_pronto = false;
    public static bool player1B_pronto = false;

    //uma variavel que serve para parar o codigo de movimentação dos jogadores, evitando um bug que impede o teleporte de ocorrer
    public static bool trocaTroca = false;

    public static bool player2A_pronto = false;
    public static bool player2B_pronto = false;

    bool trocaFinalizada;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trocaFinalizada)
        {
            Reiniciar();
            trocaFinalizada = false;
        }
        
        IniciarTroca();
    }


    //resseta os indicador, o contador da troca e as variaves que mostram onde esta o player
    void Reiniciar()
    {
        indicador = 0;
        player1A_pronto = false;
        player1B_pronto = false;
        player2A_pronto = false;
        player2B_pronto = false;
        contadorTroca = 1;
    }

    //uma corrotina que pede por o indicador(baicamente indica se é multiplayer ou single e qual é a combinação do momento) + algumas variaves para controlar o fade in/out
    IEnumerator Troca(int indicadorTroca,float _esperar_o_fadein_acabar, float _tempoComTelaEscura, float _esperar_o_fadeOut_acabar)
    {
        fade.SetBool("FadeIn", true);
        yield return new WaitForSeconds(_esperar_o_fadein_acabar);
        trocaTroca = true;

        if (indicador == 1)
        {
            playersMulti[0].transform.position = pontoB.position;
            playersMulti[1].transform.position = pontoA.position;
        }
        else if (indicador == 2)
        {
            playersMulti[0].transform.position = pontoA.position;
            playersMulti[1].transform.position = pontoB.position;
        }
        else if (indicador == 3)
        {
            playersSingle[0].transform.position = pontoB.position;
            playersSingle[1].transform.position = pontoA.position;
        }
        else if (indicador == 4)
        {
            playersSingle[0].transform.position = pontoA.position;
            playersSingle[1].transform.position = pontoB.position;
        }
        yield return new WaitForSeconds(0.2f); 
        trocaTroca = false;
        _audioSource.Play();
        yield return new WaitForSeconds(_tempoComTelaEscura);
        
        fade.SetBool("FadeIn", false);
        fade.SetBool("FadeOut", true);
        yield return new WaitForSeconds(_esperar_o_fadeOut_acabar);
        fade.SetBool("FadeOut", false);
        fade.SetBool("voltaPadrao", true);
        yield return new WaitForSeconds(1);
        fade.SetBool("voltaPadrao", false);

        yield return new WaitForSeconds(2);
        trocaFinalizada = true;
    }

    



    void IniciarTroca()
    {
        //checa se o jogador esta no sinlge ou no multi
        if (ModoDeJogo.isMultiplayer)
        {
            //checa onde ta cada jogador, so existe duas combinações , o jogador 1 esta no ponto a, o jogador 2 esta no ponto b ou o jogador 2 esta no ponto a, o jogador 1 esta no ponto b
            if (player1A_pronto && player2B_pronto)
            {
                
                if (contadorTroca == 1)
                {
                    indicador = 1;
                    StartCoroutine(Troca(indicador, esperar_o_fadein_acabar, tempoComTelaEscura, esperar_o_fadeOut_acabar));
                    contadorTroca = 0;
                }
            }

            if (player2A_pronto && player1B_pronto)
            {
                
                if (contadorTroca == 1)
                {
                    indicador = 2;
                    StartCoroutine(Troca(indicador, esperar_o_fadein_acabar, tempoComTelaEscura, esperar_o_fadeOut_acabar));
                    contadorTroca = 0;
                }
            }
        }
        else if (!ModoDeJogo.isMultiplayer)
        {
            //checa onde ta cada jogador, so existe duas combinações , o jogador 1 esta no ponto a, o jogador 2 esta no ponto b ou o jogador 2 esta no ponto a, o jogador 1 esta no ponto b
            if (player1A_pronto && player2B_pronto)
            {
                //checa se ja não foi feito uma troca, uma forma de lidar com as multiplas execuções do update
                if (contadorTroca == 1)
                {
                    //indica qual a possibilidade
                    indicador = 3;
                    StartCoroutine(Troca(indicador, esperar_o_fadein_acabar, tempoComTelaEscura, esperar_o_fadeOut_acabar));
                    contadorTroca = 0;
                }
            }

            if (player2A_pronto && player1B_pronto)
            {
                
                if (contadorTroca == 1)
                {
                    indicador = 4;
                    StartCoroutine(Troca(indicador, esperar_o_fadein_acabar, tempoComTelaEscura, esperar_o_fadeOut_acabar));
                    contadorTroca = 0;
                }
            }
        }

    }


    

    

}

