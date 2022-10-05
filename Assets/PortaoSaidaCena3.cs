using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaoSaidaCena3 : MonoBehaviour
{
    AudioSource m_AudioSource;
    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    void PortaoSaidaSom()
    {
        m_AudioSource.Play();
    }
}
