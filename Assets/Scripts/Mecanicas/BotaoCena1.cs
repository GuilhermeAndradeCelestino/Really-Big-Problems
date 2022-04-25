using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoCena1 : MonoBehaviour
{
    public Animator doorAnimator;
    public GameObject botaoVermelho;
    public GameObject botaoVerde;

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
        if(collision.gameObject.tag == "Player_2"  && ScriptPlayer2.interactP2)
        {
            doorAnimator.SetBool("OpenDoor", true);
            botaoVerde.SetActive(false);
            botaoVermelho.SetActive(true);

        }
    }
}
