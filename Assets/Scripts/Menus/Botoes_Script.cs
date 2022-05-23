using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Botoes_Script : MonoBehaviour
{
    public Button referencia_MenuInicial;
    public Button referencia_MenuModo;


    public InputActionAsset acoes;

    GameObject ultimoSelecionado;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    void Update()
    {

         

        
        if(EventSystem.current.currentSelectedGameObject != null)
        {
            //ultimoSelecionado.SetSelectedGameObject = EventSystem.current.currentSelectedGameObject;

            ultimoSelecionado = EventSystem.current.currentSelectedGameObject;

        }

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            if (Keyboard.current.anyKey.wasPressedThisFrame)
            {
                EventSystem.current.SetSelectedGameObject(ultimoSelecionado);

            }
        }

        print(EventSystem.current.currentSelectedGameObject.name);
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
        ModoDeJogo.isMultiplayer = false;
        SceneManager.LoadScene("Cena 1");
    }
    public void Modo_Doisjogadores()
    {
        ModoDeJogo.isMultiplayer = true;
        SceneManager.LoadScene("Cena 1");
    }

    public void VoltarMenuInicial()
    {
        ControladorTelas.id = 0;
    }

    
}
