using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_porta : MonoBehaviour
{


    //da esquerda para direita com as portas viradas para sua visao, 0 = porta1 |1 = porta2 | 2 = porta3 | 3 = porta4 | 
    public int[] portasCertas;
    [Space]
    [Space]
    
    
    [Header("Objetos das portas")]
    public GameObject[] portasSala1;
    
    public GameObject[] portasSala2;
    
    public GameObject[] portasSala3;

    public GameObject portasColliders;

    public GameObject particulaInteracao;

    [Space]
    [Space]

    //0 = sala 1 | 1 = sala 2 | 2 = sala 3
    public GameObject[] miniaturasSalas;

    public Transform[] possiveisPosicoes;


    int salaAtual;
    public static int resposta;
    public static bool respondendo;

    [Space]
    //tp: 0  - Sala1 | 1 - Sala2 | 2 - Sala3  | 3 - Sala Finalização | 4 - sala princiapal laberinto
    public Transform[] pontosTP;

    [Space]
    // Posição 0: player 1 || Posição 1: player 2
    public GameObject[] playersSingle;
    public GameObject[] playersMulti;
    [Space]
    public Animator fade;

    public static bool transicao;
    public static bool portaTravada;


    [Space]
    AudioSource _audioSource;
    public AudioClip[] clips;

    [Header("Opçoes de teste")]
    public bool testeSorteoPortaCerta = false;
    public bool testeArrumarPortas = false;
    public bool testarSorteo_e_Arrumar = false;
    public Transform posicaoTeste;
    public bool testaPosicaoComAsala1 = false;
    public bool testarPosicionamento = false;

    // Start is called before the first frame update
    void Start()
    {
        portaTravada = false;
        salaAtual = 1;
        _audioSource = GetComponent<AudioSource>();
        preparacaoCompleta();
    }

    // Update is called once per frame
    void Update()
    {
        travar_ou_destravar_Portas(portaTravada);
        testes();

        if (respondendo)
        {
            print(resposta + " resposta");
            if (portasCertas[salaAtual - 1] == resposta)
            {
                StartCoroutine(teleporteAcertou());
            }
            else
            {
                StartCoroutine(teleporteErrou());
            }
            respondendo = false;
        }
    }


    //Executa todo o processo, trocando qual as portas certas , e onde esta cada miniatura.
    void preparacaoCompleta()
    {
        SorteaPortaCerta();
        CorrigirPortasMiniaturas();
        posicionaMiniaturasAleatoriamente();
    }

    void SomAcertouErrou(bool acertei)
    {
        if (acertei)
        {
            _audioSource.clip = clips[0];
            _audioSource.Play();
        }
        else if (!acertei)
        {
            _audioSource.clip = clips[1];
            _audioSource.Play();
        }
    }

    IEnumerator teleporteAcertou()
    {
        transicao = true;
        yield return new WaitForSeconds(1);
        transicao = false;

        

        if (ModoDeJogo.isMultiplayer)
        {
            if (salaAtual == 3)
            {
                transicao = true;

                fade.SetBool("FadeIn", true);
                yield return new WaitForSeconds(1);

                playersMulti[0].transform.position = pontosTP[3].position;
                yield return new WaitForSeconds(0.2f);
                transicao = false;
                SomAcertouErrou(true);
                fade.SetBool("FadeIn", false);
                fade.SetBool("FadeOut", true);
                yield return new WaitForSeconds(1);
                
                fade.SetBool("FadeOut", false);
                fade.SetBool("voltaPadrao", true);
                yield return new WaitForSeconds(1);

                fade.SetBool("voltaPadrao", false);

            }
            else if (salaAtual == 1)
            {
                transicao = true;

                fade.SetBool("FadeIn", true);
                yield return new WaitForSeconds(1);

                playersMulti[0].transform.position = pontosTP[1].position;
                yield return new WaitForSeconds(0.2f);
                transicao = false;
                SomAcertouErrou(true);
                fade.SetBool("FadeIn", false);
                fade.SetBool("FadeOut", true);
                yield return new WaitForSeconds(1);
                
                fade.SetBool("FadeOut", false);
                fade.SetBool("voltaPadrao", true);
                yield return new WaitForSeconds(1);
                fade.SetBool("voltaPadrao", false);
            }
            else if (salaAtual == 2)
            {

                transicao = true;

                fade.SetBool("FadeIn", true);
                yield return new WaitForSeconds(1);

                playersMulti[0].transform.position = pontosTP[2].position;
                yield return new WaitForSeconds(0.2f);
                transicao = false;
                SomAcertouErrou(true);
                fade.SetBool("FadeIn", false);
                fade.SetBool("FadeOut", true);
                yield return new WaitForSeconds(1);
               
                fade.SetBool("FadeOut", false);
                fade.SetBool("voltaPadrao", true);
                yield return new WaitForSeconds(1);
                fade.SetBool("voltaPadrao", false);
            }

            if (salaAtual != 3)
            {
                salaAtual++;
            }

        }
        else if (!ModoDeJogo.isMultiplayer)
        {
            if (salaAtual == 3)
            {
                transicao = true;

                fade.SetBool("FadeIn", true);
                yield return new WaitForSeconds(1);

                playersSingle[0].transform.position = pontosTP[3].position;
                yield return new WaitForSeconds(0.2f);
                transicao = false;
                SomAcertouErrou(true);
                fade.SetBool("FadeIn", false);
                fade.SetBool("FadeOut", true);
                yield return new WaitForSeconds(1);
                
                fade.SetBool("FadeOut", false);
                fade.SetBool("voltaPadrao", true);
                yield return new WaitForSeconds(1);
                fade.SetBool("voltaPadrao", false);

            }
            else if (salaAtual == 1)
            {
                transicao = true;

                fade.SetBool("FadeIn", true);
                yield return new WaitForSeconds(1);

                playersSingle[0].transform.position = pontosTP[1].position;
                yield return new WaitForSeconds(0.2f);
                transicao = false;
                SomAcertouErrou(true);
                fade.SetBool("FadeIn", false);
                fade.SetBool("FadeOut", true);
                yield return new WaitForSeconds(1);
                
                fade.SetBool("FadeOut", false);
                fade.SetBool("voltaPadrao", true);
                yield return new WaitForSeconds(1);
                fade.SetBool("voltaPadrao", false);
            }
            else if (salaAtual == 2)
            {
                
                transicao = true;

                fade.SetBool("FadeIn", true);
                yield return new WaitForSeconds(1);

                playersSingle[0].transform.position = pontosTP[2].position;
                yield return new WaitForSeconds(0.2f);
                transicao = false;
                SomAcertouErrou(true);
                fade.SetBool("FadeIn", false);
                fade.SetBool("FadeOut", true);
                yield return new WaitForSeconds(1);
                
                fade.SetBool("FadeOut", false);
                fade.SetBool("voltaPadrao", true);
                yield return new WaitForSeconds(1);
                fade.SetBool("voltaPadrao", false);
            }
            
            if(salaAtual != 3)
            {
                salaAtual++;
            }
            
        }

        Salas.umaVez = true;
    }

    IEnumerator teleporteErrou()
    {
        
        if (ModoDeJogo.isMultiplayer)
        {
            transicao = true;

            fade.SetBool("FadeIn", true);
            yield return new WaitForSeconds(1);

            playersMulti[0].transform.position = pontosTP[0].position;
            playersMulti[1].transform.position = pontosTP[4].position;
            yield return new WaitForSeconds(0.2f);
            transicao = false;
            SomAcertouErrou(false);
            fade.SetBool("FadeIn", false);
            fade.SetBool("FadeOut", true);
            yield return new WaitForSeconds(1);
            
            fade.SetBool("FadeOut", false);
            fade.SetBool("voltaPadrao", true);


            print("E R R O U");
            preparacaoCompleta();
            salaAtual = 1;

            yield return new WaitForSeconds(1);
            fade.SetBool("voltaPadrao", false);

        }
        else if (!ModoDeJogo.isMultiplayer)
        {

            transicao = true;

            fade.SetBool("FadeIn", true);
            yield return new WaitForSeconds(1);

            playersSingle[0].transform.position = pontosTP[0].position;
            playersSingle[1].transform.position = pontosTP[4].position;
            yield return new WaitForSeconds(0.2f);
            transicao = false;
            SomAcertouErrou(false);
            fade.SetBool("FadeIn", false);
            fade.SetBool("FadeOut", true);
            yield return new WaitForSeconds(1);
            
            fade.SetBool("FadeOut", false);
            fade.SetBool("voltaPadrao", true);


            print("E R R O U");
            preparacaoCompleta();
            salaAtual = 1;

            yield return new WaitForSeconds(1);
            fade.SetBool("voltaPadrao", false);
        }

        Salas.umaVez = true;
    }

    void posicionaMiniaturasAleatoriamente()
    {
        int[] lista = new int[3];
        for(int i = 0; i < lista.Length; i++)
        {
            lista[i] = Random.Range(0,3);
            if(i == 1)
            {
                while(lista[i] == lista[0])
                {
                    lista[i] = Random.Range(0, 3);
                }
            }
            
            if(i == 2)
            {
                while (lista[i] == lista[0] || lista[i] == lista[1])
                {
                    lista[i] = Random.Range(0, 3);
                }
            }
            

        }

        for (int i = 0; i < lista.Length; i++)
        {
            miniaturasSalas[i].transform.position = possiveisPosicoes[lista[i]].position;
            miniaturasSalas[i].transform.rotation = possiveisPosicoes[lista[i]].rotation;
        }


    }


    //Opçoes para testes 
    void testes()
    {
        if (testeSorteoPortaCerta)
        {
            SorteaPortaCerta();
            testeSorteoPortaCerta = false;
        }

        if (testeArrumarPortas)
        {
            CorrigirPortasMiniaturas();
            testeArrumarPortas = false;
        }

        if (testarSorteo_e_Arrumar)
        {
            SorteaPortaCerta();
            CorrigirPortasMiniaturas();
            testarSorteo_e_Arrumar = false;
        }

        if (testaPosicaoComAsala1)
        {
            miniaturasSalas[0].transform.position = posicaoTeste.position;
            miniaturasSalas[0].transform.rotation = posicaoTeste.rotation;
        }


        if (testarPosicionamento)
        {
            posicionaMiniaturasAleatoriamente();
            testarPosicionamento = false;
        }
    }
    
    //faz o serteo de qual vão ser as portas certa da salas 1 , 2 e 3
    void SorteaPortaCerta()
    {
        for (int i = 0; i < portasCertas.Length; i++)
        {
            portasCertas[i] = Random.Range(0, 4);
        }
    }

    //arruma a porta das miniaturas para mostrar apenas as portas certas das salas 1 , 2 e 3
    void CorrigirPortasMiniaturas()
    {
        //Sala 1
        for (int i =0; i < portasSala1.Length; i++)
        {
            if (i != portasCertas[0])
            {
                portasSala1[i].SetActive(false);
            }
            else
            {
                portasSala1[i].SetActive(true);
            }
        }

        //Sala 2
        for (int i = 0; i < portasSala2.Length; i++)
        {
            if (i != portasCertas[1])
            {
                portasSala2[i].SetActive(false);
            }
            else
            {
                portasSala2[i].SetActive(true);
            }
        }
       
        //Sala 3
        for (int i = 0; i < portasSala3.Length; i++)
        {
            if (i != portasCertas[2])
            {
                portasSala3[i].SetActive(false);
            }
            else
            {
                portasSala3[i].SetActive(true);
            }
        }
    }

    //trava ou destrava as portas , impedindo o player 1 de interagir com elas 
    void travar_ou_destravar_Portas(bool devoTravar)
    {
        portasColliders.SetActive(devoTravar);
        particulaInteracao.SetActive(devoTravar);
    }

}
