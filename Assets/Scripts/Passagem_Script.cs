using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passagem_Script : MonoBehaviour
{
    public GameObject posicaoChegada;
    public static bool comecar_fade = false;
    int contador = 1;

    public void Passar(Transform posicaoPlayer)
    {
        if (contador == 1)
        {
            StartCoroutine(Fade(posicaoPlayer));
            contador = 0;
        }
    }

    IEnumerator Fade(Transform a)
    {
        comecar_fade = true;
        yield return new WaitForSeconds(0.5f);
        a.position = posicaoChegada.transform.position;
        yield return new WaitForSeconds(0.5f);
        contador = 1;
    }
}
