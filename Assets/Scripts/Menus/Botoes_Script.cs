using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;



public class Botoes_Script : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Slider LoadingBarfill;

    

    GameObject ultimoSelecionado;
    public AudioClip[] audiosBotoes;
    AudioSource _audioSorce;
    bool possoReinicar = true;
    // Start is called before the first frame update
    void Start()
    {
        _audioSorce = GetComponent<AudioSource>();
        possoReinicar = true;
    }

    // Update is called once per frame


    

    void Update()
    {
        //print(EventSystem.current.currentSelectedGameObject.name);

        /*
        if (pausa.podePausar)
        {
            //Caso haja algum botão selecionado armazena ele em um variavel
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                //ultimoSelecionado.SetSelectedGameObject = EventSystem.current.currentSelectedGameObject;

                ultimoSelecionado = EventSystem.current.currentSelectedGameObject;

            }
            //se não ouver nenhum botão selecionado e o usuario aperta alguma tecla seleciona o ultimo botão 
            //que foi armazenado na variavel ultimoSelecionado é selecionado
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

    IEnumerator voltaMenuDelay(int sceneID)
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(1);
        StartCoroutine(LoadSceneAsyncFase(sceneID));
    }

    public void VoltarMenuInicial(int sceneId)
    {
        ControladorTelas.id = 0;
        print(SceneManager.GetActiveScene().name);
        if(SceneManager.GetActiveScene().name == "cena 1" || SceneManager.GetActiveScene().name == "cena 2" || SceneManager.GetActiveScene().name == "CENA 3")
        {
            pausa.podePausar = false;
            StartCoroutine(LoadSceneAsyncFase(sceneId));
            //StartCoroutine(voltaMenuDelay(sceneId));
            //SceneManager.LoadScene("MenuInicialScene");
        }
    }


    IEnumerator reniciarDelay()
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(1);
        if(possoReinicar == true)
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
            possoReinicar = false;
        }
        
    }

    public void Reiniciar()
    {
        
        StartCoroutine(reniciarDelay());
        
        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        
    }

    public void SalvaConfigSomEfeito()
    {
        VolumeAtual.volumeEfeitoSonoro = GameObject.Find("Slider_EfeitosSonoros").GetComponent<Slider>().value;
        ControleVolume.podeAtualizar = true;
    }

    public void SalvaConfigSomMusica()
    {
        VolumeAtual.volumeMusica = GameObject.Find("Slider_Musica").GetComponent<Slider>().value;
        ControleVolume.podeAtualizar = true;
    }

    public void SairPausa()
    {
        pausa.podePausar = false;
    }
    
    public void SairLerLivro()
    {
        livro_interagivel.vouLer = false;
    }

    public void TutorialSingle(int sceneID)
    {
       
         ModoDeJogo.isMultiplayer = false;
         //SceneManager.LoadScene("Cena 1");
         StartCoroutine(LoadSceneAsyncMenu(sceneID));
        
        
    }

    public void TutorialMulti(int sceneID)
    {
        ModoDeJogo.isMultiplayer = true;
        StartCoroutine(LoadSceneAsyncMenu(sceneID));
    }


    public void IndoPara1_2(int sceneID)
    {
        //SceneManager.LoadScene("Cena 2");
        StartCoroutine(LoadSceneAsyncFase(sceneID));
    }

    public void IndoPara1_3(int sceneID)
    {
        //SceneManager.LoadScene("Cena 3");
        StartCoroutine(LoadSceneAsyncFase(sceneID));
    }

    IEnumerator LoadSceneAsyncMenu(int sceneID)
    {
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

       // operation.allowSceneActivation = false;

        ControladorTelas.id = 6;

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log(operation.progress);
            

            LoadingBarfill.value = progressValue;

            yield return null;
            /*
            if(progressValue == 1)
            {
                yield return new WaitForSeconds(1);
                operation.allowSceneActivation = true;
            }
            */
        }
    }

    IEnumerator LoadSceneAsyncFase(int sceneID)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        //operation.allowSceneActivation = false;

        Vitoria.tirarTela = true;
        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log(operation.progress);


            LoadingBarfill.value = progressValue;

            yield return null;
            /*
            if (progressValue == 1)
            {
                yield return new WaitForSeconds(1);
                operation.allowSceneActivation = true;
            }
            */
        }
    }
}
