using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_play : MonoBehaviour
{

    AudioSource a;

    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<AudioSource>();
        a.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(a.isPlaying == false)
        {
            Destroy(gameObject, 0.2f);
        }
    }
}
