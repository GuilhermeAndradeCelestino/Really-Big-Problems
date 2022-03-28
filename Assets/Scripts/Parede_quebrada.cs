using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parede_quebrada : MonoBehaviour
{
    public GameObject parede;
    public GameObject paredeQuebrada;
    
    [Space]
    [Space]

    public float tempoParaQuebrar;
    public float tempoParaSumir;

    [Space]
    [Space]

    public Transform centroExplosao;
    public float forcaExplosao;
    public float explosaoRadio;
    


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
        if(collision.gameObject.tag == "Player_1" && ScriptPlayer1.interactP1 == true)
        {
            print("é ele");
            StartCoroutine(QuebrarParede());
        }
    }


    IEnumerator QuebrarParede()
    {
        yield return new WaitForSeconds(tempoParaQuebrar);
        parede.SetActive(false);
        paredeQuebrada.SetActive(true);

        for (int i = 0; i < paredeQuebrada.transform.childCount; i++)
        {
            GameObject child = paredeQuebrada.transform.GetChild(i).gameObject;

            child.GetComponent<Rigidbody>().AddExplosionForce(forcaExplosao, centroExplosao.position, explosaoRadio);

        }

        Destroy(paredeQuebrada, tempoParaSumir);

    }
}
