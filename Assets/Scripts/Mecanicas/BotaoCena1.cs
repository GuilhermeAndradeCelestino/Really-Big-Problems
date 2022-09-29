using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoCena1 : MonoBehaviour
{
    public Animator doorAnimator;
    public Animator doorAnimator2;
    public GameObject botaoVermelho;
    public GameObject botaoVerde;

    public Animator fade;
    public GameObject cameraCutscene;
    public GameObject cameraJogador;

    public static bool apertouBotao = false;
    bool limitador = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(limitador && apertouBotao) 
        {
            StartCoroutine(cutscene());

            apertouBotao = false;
            limitador = false;
        }
    }

    IEnumerator cutscene()
    {
        Player_1_Script.travaPlayer = true;
        Player_2_Script.travaPlayer = true;
        fade.SetBool("FadeIn", true);
        yield return new WaitForSeconds(0.5f);
        cameraJogador.SetActive(false);
        cameraCutscene.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        fade.SetBool("FadeIn", false);
        fade.SetBool("FadeOut", true);
        yield return new WaitForSeconds(1f);

        doorAnimator.SetBool("OpenDoor", true);
        doorAnimator2.SetBool("Abrir", true);
        botaoVerde.SetActive(false);
        botaoVermelho.SetActive(true);
        fade.SetBool("FadeOut", false);
        fade.SetBool("voltaPadrao", true);
        yield return new WaitForSeconds(1.5f);


        fade.SetBool("FadeIn", true);
        fade.SetBool("voltaPadrao", false);
        yield return new WaitForSeconds(0.5f);
        cameraJogador.SetActive(true);
        cameraCutscene.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        fade.SetBool("FadeIn", false);
        fade.SetBool("FadeOut", true);
        yield return new WaitForSeconds(1f);

        fade.SetBool("FadeOut", false);
        fade.SetBool("voltaPadrao", true);
        fade.SetBool("voltaPadrao", false);

        Player_1_Script.travaPlayer = false;
        Player_2_Script.travaPlayer = false;

    }


}
