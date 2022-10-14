using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class pausa : MonoBehaviour
{

    public GameObject hud_pausa;
    public GameObject botao;
    public static bool podePausar;
    bool umaVez;

    // Start is called before the first frame update
    void Start()
    {
        podePausar = false;
        umaVez = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (podePausar)
        {
            
            hud_pausa.SetActive(true);
            if (umaVez == true)
            {
                StartCoroutine(delay());
                print("sadasdasdsadsssssss");
                EventSystem.current.SetSelectedGameObject(botao);
                umaVez = false;
            }

            
        }
        else if (!podePausar)
        {
            hud_pausa.SetActive(false);
            umaVez = true;
            Time.timeScale = 1;
        }
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 0;
    }
}
