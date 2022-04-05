using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passagem : MonoBehaviour
{

    public static bool estaPassando;
    public Transform pointA;
    public Transform pointB;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if(gameObject.name == "Portal_A" && ScriptPlayer2.interactP2)
        {
            
            StartCoroutine(Teleporta(pointB));
            
        }
        else if (gameObject.name == "Portal_B" && ScriptPlayer2.interactP2)
        {
            
            StartCoroutine(Teleporta(pointA));

        }
    }


    IEnumerator Teleporta (Transform paraOnde)
    {
        estaPassando = true;
        yield return new WaitForSeconds(1f);
        player.transform.position = paraOnde.transform.position;
    }
}
