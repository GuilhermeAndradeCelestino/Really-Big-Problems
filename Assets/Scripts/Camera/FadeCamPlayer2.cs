using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCamPlayer2 : MonoBehaviour
{

    
    

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
        if (Passagem_Script.comecar_fade)
        {
            StartCoroutine(animacao());
            Passagem_Script.comecar_fade = false;
        }
    }

    IEnumerator animacao()
    {

        _animator.SetBool("PlayFade", true);
        yield return new WaitForSeconds(1.3f);
        _animator.SetBool("PlayFade", false);
        _animator.SetBool("StopFade", true);
        yield return new WaitForSeconds(0.5f);
        _animator.SetBool("StopFade", false);
        _animator.SetBool("normalState", true);
        yield return new WaitForSeconds(0.2f);
        _animator.SetBool("normalState", false);
    }

}
