using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzle_sequencia_com_cor : MonoBehaviour
{
    //indicadores 
    //0-Azul
    //1-Amarelo
    //2-Verde
    //3-vermelho
    public Animator[] indicadores;
    [Space]
    public Animator[] feedbackVisual;
    [Space]
    public GameObject[] collidersInteragiveis;
    [Space]
    //1- camera ods players 2- camera cutscene
    public GameObject[] cameras;
    public Animator abrirPassagem;
    public Animator fade;
    
    //0- verde 1- vermelho 2-verde 3-vermelho || 4 e 5 collider
    public GameObject[] botoes;

    public static bool placaVerde;
    public static bool placaAzul;
    public static bool placaVermelho;
    public static bool placaAmarelo;
    int limitador = 1;
    
    //variavel que os players podem acessar para indicar que o batão de iniciar a sequenci foi apertado
    public static bool reiniciar;

    //travar o botão 
    bool travarBotao = false;
    
    //indicar a sequenci ja foi embaralhada
    bool terminouDeEmbaralha = false;
    
    //uma garantinha extra para ter certeza que o embaralhador não seja iniciado acidentalmente
    bool estouJogando = false;
    
    //Indica se houve acerto ou erro
    bool acertou = false;
    bool errou = false;

    //Indica se o jogador venceu o puzzle
    bool vitoria = false;


    [Space]
    [Space]
    //sequencia que deve ser seguida
    public int[] sequenciaCerta;

    //sequencia do jogador
    public int[] sequenciaPlayer;
    int contador = 0;

    [Space]
    [Space]
    //opçoes para teste
    public bool isModoTesteLigado;
    [Space]
    public int contadorTeste;
    [Space]
    public bool testeTravarBotao;
    public bool isBotaoTravado;
    [Space]
    public bool SorteioRapido;
    [Space]
    public bool testarVitoria;
    
    void Start()
    {
        //deliga o collider ods botoes coloridos para evitar erros
        collidersInteragiveis[0].GetComponent<Collider>().enabled = false;
        collidersInteragiveis[1].GetComponent<Collider>().enabled = false;
        collidersInteragiveis[2].GetComponent<Collider>().enabled = false;
        collidersInteragiveis[3].GetComponent<Collider>().enabled = false;
        
        //seta a sequencia do player para 5 em totos os slots pra evitar erro
        for (int i = 0; i < 10; i++)
        {
            sequenciaPlayer[i] = 5;
        }
    }

    
    void Update()
    {

        //liga o modo teste
        if (isModoTesteLigado)
        {
            ModoDeTeste();
        }

        if(!vitoria)
        {
            //inicia o sorteio da sequencia e trava o botão 
            if (reiniciar && !estouJogando)
            {
                StartCoroutine(Embaralhar());
                travarBotao = true;
                reiniciar = false;
            }

            //trava e destrava o botão de reiniciar/embaralhar
            botaoReiniciar();



            //define se acertou ou errou
            AcertouOuErrou();

            if (terminouDeEmbaralha)
            {
                PegarSequencia();
            }

            ChecarVitoria();
        }

        if (vitoria && limitador == 1)
        {
            StartCoroutine(Cutscene());
            limitador = 0;
        }

    }

    IEnumerator Cutscene()
    {
        fade.SetBool("FadeIn", true);
        yield return new WaitForSeconds(1);
        
        cameras[0].SetActive(false);
        cameras[1].SetActive(true);
        yield return new WaitForSeconds(0.2f);

        fade.SetBool("FadeOut", true);
        fade.SetBool("FadeIn", false);
        yield return new WaitForSeconds(2.5f);

        fade.SetBool("voltaPadrao", true);
        fade.SetBool("FadeOut", false);
        abrirPassagem.SetBool("Abre", true);
        yield return new WaitForSeconds(3f);

        fade.SetBool("FadeIn", true);
        yield return new WaitForSeconds(1);
        
        fade.SetBool("voltaPadrao", false);
        cameras[0].SetActive(true);
        cameras[1].SetActive(false);
        yield return new WaitForSeconds(1);
        
        fade.SetBool("FadeOut", true);
        fade.SetBool("FadeIn", false);
        yield return new WaitForSeconds(3f);
        
        fade.SetBool("voltaPadrao", true);

        fade.SetBool("FadeOut", false);
        

    }

    void AcertouOuErrou()
    {
        if (acertou)
        {
            StartCoroutine(animacaoAcertou());
            if(contador == 10)
            {
                print("Você ganhou, parabens");
                placaVerde = false;
                placaVermelho = false;
                placaAzul = false;
                placaAmarelo = false;
                acertou = false;
            }
            sequenciaPlayer[contador] = sequenciaCerta[contador];
            contador++;
            placaVerde = false;
            placaVermelho = false;
            placaAzul = false;
            placaAmarelo = false;
            acertou = false;
        }
        
        if (errou)
        {
            for(int i = 0; i < 10; i ++)
            {
                sequenciaPlayer[i] = 5;
            }
            StartCoroutine(animacaoErrou());
            contador = 0;
            placaVerde = false;
            placaVermelho = false;
            placaAzul = false;
            placaAmarelo = false;
            terminouDeEmbaralha = false;
            travarBotao = false;
            estouJogando = false;
            errou = false;
        }
    }

    IEnumerator animacaoAcertou()
    {
        feedbackVisual[0].SetBool("Acertou", true);
        feedbackVisual[1].SetBool("Acertou", true);

        yield return new WaitForSeconds(3f);

        feedbackVisual[0].SetBool("Acertou", false);
        feedbackVisual[1].SetBool("Acertou", false);
    }


    IEnumerator animacaoErrou()
    {
        feedbackVisual[0].SetBool("Errou", true);
        feedbackVisual[1].SetBool("Errou", true);

        yield return new WaitForSeconds(3f);

        feedbackVisual[0].SetBool("Errou", false);
        feedbackVisual[1].SetBool("Errou", false);
    }

    void PegarSequencia()
    {
        if(contador != 10)
        {
            if (placaVerde)
            {
                if (sequenciaCerta[contador] == 2 && !acertou && !errou)
                {
                    print("Acertou");
                    acertou = true;
                }
                else if (sequenciaCerta[contador] != 2 && !acertou && !errou)
                {
                    print("errou");
                    errou = true;
                }
            }
            else if (placaVermelho)
            {
                if (sequenciaCerta[contador] == 3 && !acertou && !errou)
                {
                    print("Acertou");
                    acertou = true;
                }
                else if (sequenciaCerta[contador] != 3 && !acertou && !errou)
                {
                    print("errou");
                    errou = true;
                }
            }
            else if (placaAzul)
            {
                if (sequenciaCerta[contador] == 0 && !acertou && !errou)
                {
                    print("Acertou");
                    acertou = true;
                }
                else if (sequenciaCerta[contador] != 0 && !acertou && !errou)
                {
                    print("errou");
                    errou = true;
                }
            }
            else if (placaAmarelo)
            {
                if (sequenciaCerta[contador] == 1 && !acertou && !errou)
                {
                    print("Acertou");
                    acertou = true;
                }
                else if (sequenciaCerta[contador] != 1 && !acertou && !errou)
                {
                    print("errou");
                    errou = true;
                }
            }
        }       
    }



    IEnumerator Embaralhar()
    {
        for (int i = 0; i < 10; i++)
        {
            sequenciaCerta[i] = Random.Range(0, 4);
            indicadores[sequenciaCerta[i]].SetBool("Tocar", true);
            yield return new WaitForSeconds(1f);
            indicadores[sequenciaCerta[i]].SetBool("Tocar", false);
            yield return new WaitForSeconds(1f);
            if(i == 9)
            {
                estouJogando = true;
                terminouDeEmbaralha = true;
                collidersInteragiveis[0].GetComponent<Collider>().enabled = true;
                collidersInteragiveis[1].GetComponent<Collider>().enabled = true;
                collidersInteragiveis[2].GetComponent<Collider>().enabled = true;
                collidersInteragiveis[3].GetComponent<Collider>().enabled = true;
            }
        }
    }


    void botaoReiniciar()
    {
        if(travarBotao == false)
        {
            botoes[0].SetActive(true);
            botoes[1].SetActive(false);
            botoes[2].SetActive(true);
            botoes[3].SetActive(false);
            botoes[4].GetComponent<Collider>().enabled = true;
            botoes[5].GetComponent<Collider>().enabled = true;
        }
        else if(travarBotao == true)
        {
            botoes[0].SetActive(false);
            botoes[1].SetActive(true);
            botoes[2].SetActive(false);
            botoes[3].SetActive(true);
            botoes[4].GetComponent<Collider>().enabled = false;
            botoes[5].GetComponent<Collider>().enabled = false;
        }
    }

    void ModoDeTeste()
    {
        //contador mostrando em parte da sequencia nos estamos
        contadorTeste = contador;

        //testar se o travamento dos botoes esta funcionando
        if (testeTravarBotao)
        {
            travarBotao = isBotaoTravado;
        }
        
        //sorteio rapido sem as animações 
        if (SorteioRapido)
        {
            for (int i = 0; i < 10; i++)
            {
                sequenciaCerta[i] = Random.Range(0, 4);
                travarBotao = true;
                terminouDeEmbaralha = true;
            }
            SorteioRapido = false;
        }
        vitoria = testarVitoria;
    }

    void ChecarVitoria()
    {
        for(int i = 0; i < 10; i++)
        {
            if(sequenciaPlayer[i] != sequenciaCerta[i])
            {
                return;
            }
            
            if(i == 9)
            {
                vitoria = true;
            }
        }
    }

}
