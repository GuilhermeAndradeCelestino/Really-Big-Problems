using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class livro_interagivel : MonoBehaviour
{
    public static bool estouLendo;
    public bool mostrarMensagem;
    public static bool vouLer;

    public GameObject tutorial;
    public GameObject Texto;
    public GameObject botao;

    public static bool p1EstaPerto;
    public static bool p2EstaPerto;

    Collider co;

    bool limitador = true;

    // Start is called before the first frame update
    void Start()
    {
        estouLendo = false;
        tutorial.SetActive(false);
        Texto.SetActive(false);
        co = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        MostrarMensagem();

        //MostrarLivro();
        teste();
    }


    void MostrarMensagem()
    {
        if (vouLer)
        {
            Texto.SetActive(false);
        }
        
        if (!mostrarMensagem)
        {
            Texto.SetActive(false);
        }
        
        if (pausa.podePausar)
        {
            Texto.SetActive(false);
        }
        
        if (mostrarMensagem && !vouLer)
        {
            Texto.SetActive(true);
        }
    }



    void MostrarLivro()
    {
        if (p1EstaPerto || p2EstaPerto)
        {
            if (vouLer && mostrarMensagem)
            {
                estouLendo = true;
                tutorial.SetActive(true);
                co.enabled = false;
                EventSystem.current.SetSelectedGameObject(botao);
                if (limitador)
                {
                    GetComponent<AudioSource>().Play();
                    limitador = false;
                }
            }
            else if (!vouLer && mostrarMensagem)
            {

                tutorial.SetActive(false);
                EventSystem.current.SetSelectedGameObject(null);
                limitador = true;
                estouLendo = false;
                StartCoroutine(habilitar());
            }
        }
    }

    void teste()
    {
        if(p1EstaPerto || p2EstaPerto)
        {
            if (vouLer && mostrarMensagem)
            {
                estouLendo = true;
                tutorial.SetActive(true);
                EventSystem.current.SetSelectedGameObject(botao);
                if (limitador)
                {
                    GetComponent<AudioSource>().Play();
                    limitador = false;
                }
            }

            if (!vouLer && mostrarMensagem && estouLendo) 
            {
                tutorial.SetActive(false);
                EventSystem.current.SetSelectedGameObject(null);
                limitador = true;
                StartCoroutine(habilitar());
            }
        }
    }


    IEnumerator habilitar()
    {
        yield return new WaitForSeconds(0.2f);
        
        //co.enabled = true;
        estouLendo = false;
    }
}
