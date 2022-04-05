using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCamPlayer2 : MonoBehaviour
{

    
    public Transform target;

    Animator _animator;
    



    // Start is called before the first frame update
    void Start()
    {
        
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Fade();
    }


    void Fade()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("ComFade"))
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                _animator.SetBool("PlayFade", false);
            }
        }


        if (Passagem.estaPassando)
        {
            _animator.SetBool("PlayFade", true);
            Passagem.estaPassando = false;
        }
    }

    

}
