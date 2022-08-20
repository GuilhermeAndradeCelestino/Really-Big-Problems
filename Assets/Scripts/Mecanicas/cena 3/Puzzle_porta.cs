using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_porta : MonoBehaviour
{


    //da esquerda para direita, 0 = porta1 |1 = porta2 | 2 = porta3 | 3 = porta4 | 
    public int[] portasCertas;
    [Space]
    [Space]
    
    
    [Header("Obejetos das portas")]
    public GameObject[] portasSala1;
    
    public GameObject[] portasSala2;
    
    public GameObject[] portasSala3;

    [Space]
    [Space]

    //0 = sala 1 | 1 = sala 2 | 2 = sala 3
    public GameObject[] miniaturasSalas;

    public Transform[] possiveisPosicoes;


    int salaAtual;
    public static int resposta;
    public static bool respondendo;

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
        salaAtual = 1;
        
        preparacaoCompleta();
    }

    // Update is called once per frame
    void Update()
    {
        testes();

        if (respondendo)
        {
            if (portasCertas[salaAtual - 1] == resposta)
            {
                Acertou();
            }
            else
            {
                errou();
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



    void Acertou()
    {
        print("A C E R T O U");
        if(salaAtual == 3)
        {
            print("T E R M I N O U");
        }
        else
        {
            salaAtual++;
        }
    }

    void errou()
    {
        print("E R R O U");
        preparacaoCompleta();
        salaAtual = 1;
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


}
