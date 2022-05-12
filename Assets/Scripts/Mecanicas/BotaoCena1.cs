using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoCena1 : MonoBehaviour
{
    public Animator doorAnimator;
    public GameObject botaoVermelho;
    public GameObject botaoVerde;

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
            doorAnimator.SetBool("OpenDoor", true);
            botaoVerde.SetActive(false);
            botaoVermelho.SetActive(true);

            apertouBotao = false;
            limitador = false;
        }
    }

    


}
