using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    
    public Transform target;
    public float speed;
    
    public Vector3 offsetPosition;
    public Vector3 offsetRotation;


    Vector3 velocity = Vector3.zero;

    public static int posicaoJogador1;
    
    public RectTransform fundoPreto;
    /*
    posiçoes camera
    1- down (padrao)- Position : x = 0 | y = 10.7 | z = -7.5   &   Rotation x = 45 | y = 0 | z = 0
    2- left - Position : x = -7.5 | y = 10.7 | z = 0   &   Rotation x = 45 | y = 90 | z = 0
    3- up - Position : x = 0 | y = 10.7 | z = 7.5   &   Rotation x = 45 | y = 180 | z = 0
    4- right - Position : x = 7.5 | y = 10.7 | z = 0   &   Rotation x = 45 | y = -90 | z = 0
    */


    // Start is called before the first frame update
    void Start()
    {
        posicaoJogador1 = 1;

    }

    // Update is called once per frame
    void Update()
    {
        //Posiciona e rotaciona a camera na posição certa, faz ela seguir o jogador
        transform.position = Vector3.SmoothDamp(transform.position, target.position + offsetPosition, ref velocity, speed);
        transform.rotation = Quaternion.Euler(offsetRotation);


        //Checa qual a posição baseado na variavel posicaoJogador1 e muda o valor de offset
        if (posicaoJogador1 == 1)
        {
            offsetPosition = new Vector3(0f, 10.7f, -7.5f);
            offsetRotation = new Vector3(45f, 0f, 0f);
        }
        else if (posicaoJogador1 == 2)
        {
            offsetPosition = new Vector3(-7.5f, 10.7f, -0f);
            offsetRotation = new Vector3(45f, 90f, 0f);
        }
        else if (posicaoJogador1 == 3)
        {
            offsetPosition = new Vector3(0f, 10.7f, 7.5f);
            offsetRotation = new Vector3(45f, 180f, 0f);
        }
        else if (posicaoJogador1 == 4)
        {
            offsetPosition = new Vector3(7.5f, 10.7f, 0f);
            offsetRotation = new Vector3(45f, -90f, 0f);
        }
    }

    private void LateUpdate()
    {
        //transform.position = target.position + offset;

        //transform.position = Vector3.SmoothDamp(transform.position, target.position + offsetPosition, ref velocity, speed);
        
    }




    


}
