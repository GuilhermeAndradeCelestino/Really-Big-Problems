using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class livro_interagivel : MonoBehaviour
{
    public static bool estouLendo;
    public bool mostrarMensagem;

    public GameObject tutorial;
    public GameObject Texto;
    public GameObject botao;


    bool limitador = true;

    // Start is called before the first frame update
    void Start()
    {
        tutorial.SetActive(false);
        Texto.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        MostrarMensagem();

        MostrarLivro();
    }


    void MostrarMensagem()
    {
        if (estouLendo)
        {
            Texto.SetActive(false);
        }
        else if (!mostrarMensagem)
        {
            Texto.SetActive(false);
        }
        else if (pausa.podePausar)
        {
            Texto.SetActive(false);
        }
        else if (mostrarMensagem && !estouLendo)
        {
            Texto.SetActive(true);
        }
    }

    void MostrarLivro()
    {
        if (estouLendo && mostrarMensagem)
        {
            tutorial.SetActive(true);
            EventSystem.current.SetSelectedGameObject(botao);
            if (limitador)
            {
                GetComponent<AudioSource>().Play();
                limitador = false;
            }
        }
        else if (!estouLendo && mostrarMensagem)
        {
            tutorial.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
            limitador = true;
        }
    }

    
}
