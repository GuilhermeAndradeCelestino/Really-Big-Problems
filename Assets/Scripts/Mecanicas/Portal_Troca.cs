using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_Troca : MonoBehaviour
{
    public static bool P1EstaPronto = false;
    public static bool P2EstaPronto = false;
    public static bool P1EstaNoA = false;
    public Animator fade;


    bool estouRecarregando = false;

    public bool troca;
    Collider a;
    float timer = 0;
    public int cargas = 1;
    bool terminouDeTrocar = false;

    //codernada
    public Transform pontoPortalA;
    public Transform pontoPortalB;

    [Space]
    [Space]

    //players
    public GameObject player1_single;
    public GameObject player2_single;

    [Space]

    public GameObject player1_multi;
    public GameObject player2_multi;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //checa se os dois jogadores estão na plataforma e se ha cargas
        if(P1EstaPronto && P2EstaPronto && cargas == 1)
        {
            fade.SetBool("voltaPadrao", false);
            StartCoroutine(Teleporta(pontoPortalA, pontoPortalB));
        }
        //uma que a troca tenha terminado e os dois jogadores tenham saido do tp reseta a carga do tp
        if(!P1EstaNoA && !P2EstaPronto && terminouDeTrocar)
        {
            fade.SetBool("voltaPadrao", true);
            fade.SetBool("FadeOut", false);
            fade.SetBool("FadeIn", false);
            cargas = 1;
            terminouDeTrocar = false;
            
        }

       // print("P1estaPronto: " + P1EstaPronto + " // " + "P2estaPronto: " + P2EstaPronto);

    }
   


    IEnumerator Teleporta(Transform pontoA, Transform pontoB)
    {
        fade.SetBool("FadeIn", true);
        yield return new WaitForSeconds(1f);
        //garante que a corrotina não fique executando sem para 
        if(cargas == 0)
        {
            StopAllCoroutines();
        }
        if (ModoDeJogo.isMultiplayer)
        {
            if (P1EstaNoA)
            {
                
                player2_multi.transform.position = pontoA.transform.position;
                player1_multi.transform.position = pontoB.transform.position;
                fade.SetBool("FadeOut", true);
                cargas = 0;
                terminouDeTrocar = true;
            }   
            else
            {
                
                player2_multi.transform.position = pontoB.transform.position;
                player1_multi.transform.position = pontoA.transform.position;
                fade.SetBool("FadeOut", true);
                cargas = 0;
                terminouDeTrocar = true;
            }

        }        
        else if (!ModoDeJogo.isMultiplayer)
        {
            if (P1EstaNoA)   
            {
                
                player2_single.transform.position = pontoA.transform.position;
                player1_single.transform.position = pontoB.transform.position;
                cargas = 0;
                terminouDeTrocar = true;
                fade.SetBool("FadeOut", true);

               
            }
            else
            {
                player2_single.transform.position = pontoB.transform.position;
                player1_single.transform.position = pontoA.transform.position;
                cargas = 0;
                terminouDeTrocar = true;
                fade.SetBool("FadeOut", true);
            }
        }
        
    } 
 }



