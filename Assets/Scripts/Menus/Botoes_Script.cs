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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    void Update()
    {

         

        //Caso haja algum botão selecionado armazena ele em um variavel
        if(EventSystem.current.currentSelectedGameObject != null)
        {
            //ultimoSelecionado.SetSelectedGameObject = EventSystem.current.currentSelectedGameObject;

            ultimoSelecionado = EventSystem.current.currentSelectedGameObject;

        }

        //se não ouver nenhum botão selecionado e o usuario aperta alguma tecla seleciona o ultimo botão 
        //que foi armazenado na variavel ultimoSelecionado é selecionado
        if (EventSystem.current.currentSelectedGameObject == null)
        {

            if (Keyboard.current.anyKey.wasPressedThisFrame)
            {
                EventSystem.current.SetSelectedGameObject(ultimoSelecionado);

            }
        }

        //print(EventSystem.current.currentSelectedGameObject.name);
    }

    public void Menu_BotaoIniciar()
    {
        ControladorTelas.id = 2;
    }

    public void Menu_BotaoOpcoes()
    {
        //ControladorTelas.id = 1;
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

    public void VoltarMenuInicial()
    {
        ControladorTelas.id = 0;
        print(SceneManager.GetActiveScene().name);
        if(SceneManager.GetActiveScene().name == "cena 1")
        {
            SceneManager.LoadScene("MenuInicialScene");
        }
    }

    public void Reiniciar()
    {
        
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        
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

}
