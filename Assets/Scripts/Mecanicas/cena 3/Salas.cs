using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salas : MonoBehaviour
{
    public static bool umaVez;
    // Start is called before the first frame update
    void Start()
    {
        umaVez = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void MandaResposta(int qualAPorta)
    {
        if (umaVez)
        {
            Puzzle_porta.resposta = qualAPorta;
            Puzzle_porta.respondendo = true;
            umaVez = false;
        }
        
    }
}
