using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Botoes_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
