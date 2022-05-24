using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pausa : MonoBehaviour
{

    public GameObject hud_pausa;

    public static bool podePausar;

    // Start is called before the first frame update
    void Start()
    {
        podePausar = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (podePausar)
        {
            hud_pausa.SetActive(true);
            Time.timeScale = 0;
        }
        else if (!podePausar)
        {
            hud_pausa.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
