using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleVolume : MonoBehaviour
{
    public AudioSource[] somMusica;
    public AudioSource[] efeitosSonorosPrefab;
    public AudioSource[] efeitosSonorosFase;

    public static bool podeAtualizar = true;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (podeAtualizar == true)
        {
            for (int i = 0; i < somMusica.Length; i++)
            {
                somMusica[i].volume = VolumeAtual.volumeMusica;
            }

            for (int x = 0; x < efeitosSonorosPrefab.Length; x++)
            {
                efeitosSonorosPrefab[x].volume = VolumeAtual.volumeEfeitoSonoro;
            }

            for (int x = 0; x < efeitosSonorosFase.Length; x++)
            {
                efeitosSonorosFase[x].volume = VolumeAtual.volumeEfeitoSonoro;
            }
            podeAtualizar = false;
        }
        
    }
}
