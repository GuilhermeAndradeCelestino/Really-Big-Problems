using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Audio : MonoBehaviour
{
    public GameObject livro;
    public GameObject quebrandoParede;
    public GameObject apertaBotao;


    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (livro_interagivel.estouLendo)
        {
            Instantiate(livro);
        }
    }
}
