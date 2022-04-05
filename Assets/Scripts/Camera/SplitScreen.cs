using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreen : MonoBehaviour
{
    public float distanciaMax;

    [Space]
    [Space]

    public Camera cameraPlayer1;
    public Camera cameraPlayer2;
  
    [Space]
    [Space]

    public Transform transformP1;
    public Transform transformP2;

   

    

    // Update is called once per frame
    void Update()
    {
        

        float distancia = Vector3.Distance(transformP1.position, transformP2.position);

        if(distancia > distanciaMax)
        {
            cameraPlayer2.enabled = true;
            cameraPlayer1.rect = new Rect(0, 0, 0.5f, 1);
            cameraPlayer2.rect = new Rect(0.5f, 0, 0.5f, 1);
        }
        else
        {
            cameraPlayer2.enabled = false;
            cameraPlayer1.rect = new Rect(0, 0, 1, 1);
        }
    }
}
