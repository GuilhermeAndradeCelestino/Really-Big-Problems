using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Botoes_Script : MonoBehaviour
{
    

    GameObject ultimoSelecionado;
    public AudioClip[] audiosBotoes;
    AudioSource _audioSorce;

    // Start is called before the first frame update
    void Start()
    {
        _audioSorce = GetComponent<AudioSource>();
    }

    // Update is called once per frame


    

    void Update()
    {
        //print(EventSystem.current.currentSelectedGameObject.name);

        /*
        if (pausa.podePausar)
        {
            //Caso haja algum bot�o selecionado armazena ele em um variavel
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                //ultimoSelecionado.SetSelectedGameObject = EventSystem.current.currentSelectedGameObject;

                ultimoSelecionado = EventSystem.current.currentSelectedGameObject;

            }
            //se n�o ouver nenhum bot�o selecionado e o usuario aperta alguma tecla seleciona o ultimo bot�o 
            //que foi armazenado na variavel ultimoSelecionado � selecionado
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                //EventSystem.current.SetSelectedGameObject(ultimoSelecionado);

                if (Keyboard.current.anyKey.wasPressedThisFrame)
                {
                    EventSystem.current.SetSelectedGameObject(ultimoSelecionado);

                }

            }

        }
        */



        //print(EventSystem.current.currentSelectedGameObject.name);
    }



    
    public void Audio_ApertaBotao()
    {
        _audioSorce.clip = audiosBotoes[0];
        _audioSorce.Play();
    }

    public void Audio_SelecionarBotao()
    {

    }


    public void Menu_BotaoIniciar()
    {
        ControladorTelas.id = 2;
    }

    public void Menu_BotaoOpcoes()
    {
        ControladorTelas.id = 1;
    }

    public void Menu_BotaoSair()
    {
        Application.Quit();
    }

    public void Modo_Umjogador()
    {
        ControladorTelas.id = 4;
    }
    public void Modo_Doisjogadores()
    {
        ControladorTelas.id = 5;
    }

    IEnumerator voltaMenuDelay()
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MenuInicialScene");
    }

    public void VoltarMenuInicial()
    {
        ControladorTelas.id = 0;
        print(SceneManager.GetActiveScene().name);
        if(SceneManager.GetActiveScene().name == "cena 1" || SceneManager.GetActiveScene().name == "cena 2" || SceneManager.GetActiveScene().name == "CENA 3")
        {
            
            StartCoroutine(voltaMenuDelay());
            //SceneManager.LoadScene("MenuInicialScene");
        }
    }


    IEnumerator reniciarDelay()
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void Reiniciar()
    {
        StartCoroutine(reniciarDelay());
        print("reiniciei");
        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        
    }

    public void SairPausa()
    {
        pausa.podePausar = false;
    }
    
    public void SairLerLivro()
    {
        livro_interagivel.estouLendo = false;
    }

    public void TutorialSingle()
    {
        ModoDeJogo.isMultiplayer = false;
        SceneManager.LoadScene("Cena 1");
    }

    public void TutorialMulti()
    {
        ModoDeJogo.isMultiplayer = true;
        SceneManager.LoadScene("Cena 1");
    }


    public void IndoPara1_2()
    {
        SceneManager.LoadScene("Cena 2");
    }

    public void IndoPara1_3()
    {
        SceneManager.LoadScene("Cena 3");
    }
}
