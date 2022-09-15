using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualOplayer_Single : MonoBehaviour
{
    public Camera cameraP1;
    public Camera cameraP2;
    public GameObject MeioCamera;
    
    [Space]
    [Header("Tamanho das cameras")]

    [Tooltip("O maior valor que a camera ira ter, a soma do valor maior e menor deve ser igual a 1 ")]
    public float valorMaior = 0.7f;
    [Tooltip("O menor valor que a camera ira ter, a soma do valor maior e menor deve ser igual a 1 ")]
    public float valorMenor = 0.3f;
    public float speed = 1;

    public static bool MudarCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!ModoDeJogo.isMultiplayer)
        {
            if (ModoDeJogo.qualOjogador == 1 && MudarCamera)
            {
                Rect valorAntigoP1 = cameraP1.rect;
                Rect valorAntigoP2 = cameraP2.rect;

                
                cameraP1.rect = new Rect(0, 0, Mathf.Lerp(valorAntigoP1.width, valorMaior, speed *  Time.deltaTime), 1);
                cameraP2.rect = new Rect(Mathf.Lerp(valorAntigoP2.x, valorMaior, speed * Time.deltaTime), 0, Mathf.Lerp(valorAntigoP2.width, valorMenor, speed * Time.deltaTime), 1);
                //MudarCamera = false;
            }
            else if (ModoDeJogo.qualOjogador == 2 && MudarCamera)
            {
                Rect valorAntigoP1 = cameraP1.rect;
                Rect valorAntigoP2 = cameraP2.rect;

                cameraP1.rect = new Rect(0, 0, Mathf.Lerp(valorAntigoP1.width, valorMenor, speed * Time.deltaTime), 1);
                cameraP2.rect = new Rect(Mathf.Lerp(valorAntigoP2.x, valorMenor, speed * Time.deltaTime), 0, Mathf.Lerp(valorAntigoP2.width, valorMaior, speed * Time.deltaTime), 1);

                //MudarCamera = false;
            }
        }
    }



}
