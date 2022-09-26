using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cena3_porta_script : MonoBehaviour
{
    public Animator porta, portao;
    Collider collider_;
    
    
    // Start is called before the first frame update
    void Start()
    {
        collider_ = GetComponent<Collider>();
    }

    public void FecharPorta()
    {
        porta.SetTrigger("Abrir");
        collider_.enabled = false;
        Puzzle_porta.portaTravada = true;
    }

    public void AbrirPortao()
    {
        portao.SetTrigger("Abrir");
        collider_.enabled = false;
    }
}
