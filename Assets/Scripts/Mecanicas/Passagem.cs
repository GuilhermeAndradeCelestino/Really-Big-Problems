using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passagem : MonoBehaviour
{

    public static bool estaPassando;
    public static bool comecarPassagem = false;
    public static bool a_To_b;
    public static bool b_To_A;
    public Transform pointA;
    public Transform pointB;
    public GameObject player2_single;
    public GameObject player2_multi;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (a_To_b && comecarPassagem)
        {

            StartCoroutine(Teleporta(pointB));
            comecarPassagem = false;
            a_To_b = false;
        }
        
        if (b_To_A && comecarPassagem)
        {

            StartCoroutine(Teleporta(pointA));
            comecarPassagem = false;
            b_To_A = false;
        }
        
    }

    


    IEnumerator Teleporta (Transform paraOnde)
    {
        estaPassando = true;
        yield return new WaitForSeconds(1f);

        if (ModoDeJogo.isMultiplayer)
        {
            player2_multi.transform.position = paraOnde.transform.position;
        }
        else if (!ModoDeJogo.isMultiplayer)
        {
            player2_single.transform.position = paraOnde.transform.position;
        }
        comecarPassagem = false;
    }
}
