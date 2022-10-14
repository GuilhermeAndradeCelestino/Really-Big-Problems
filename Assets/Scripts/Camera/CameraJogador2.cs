using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraJogador2 : MonoBehaviour
{
    public Transform target;
    
    [Space]

    public float speed;
    public float rotationSpeed;
    public float delayEntreTroca;

    [Space]

    public Vector3 offsetPosition;
    public Vector3 offsetRotation;

    [Space]
    
    public static int posicaoJogador2;
    public LayerMask wallLayer;
    public float distanciaCollisao;
    [Space]
    
    Quaternion offsetRotationQuar;
    Vector3 velocity = Vector3.zero;

    [Space]

    public RectTransform fundoPreto;
    

    /*
     posi�oes camera
     1- down (padrao)- Position : x = 0 | y = 3.6f | z = -2.52  &   Rotation x = 45 | y = 0 | z = 0
     2- left - Position : x = -2.52 | y = 3.6f | z = 0   &   Rotation x = 45 | y = 90 | z = 0
     3- up - Position : x = 0 | y = 3.6f | z = 2.52   &   Rotation x = 45 | y = 180 | z = 0
     4- right - Position : x = 2.52 | y = 3.6f | z = 0   &   Rotation x = 45 | y = -90 | z = 0
     */


    // Start is called before the first frame update
    void Start()
    {
        posicaoJogador2 = 1;

    }

    // Update is called once per frame
    void Update()
    {
        //Arruma o tamanho da tela conforme o jogador selecionado no momento
        TamanhoTelaPreta();


        
    }

    private void FixedUpdate()
    {
        //Posiciona e rotaciona a camera na posi��o certa, faz ela seguir o jogador

        transform.position = Vector3.SmoothDamp(transform.position, target.position + offsetPosition, ref velocity, speed);

        offsetRotationQuar = Quaternion.Euler(offsetRotation);

        transform.rotation = Quaternion.Lerp(transform.rotation, offsetRotationQuar, rotationSpeed);


        //Checa qual a posi��o baseado na variavel posicaoJogador1 e muda o valor de offset
        if (posicaoJogador2 == 1)
        {
            StartCoroutine(MudarPosicao(new Vector3(0f, 3.6f, -2.52f), new Vector3(45f, 0f, 0f)));
        }
        else if (posicaoJogador2 == 2)
        {
            StartCoroutine(MudarPosicao(new Vector3(-2.52f, 3.6f, 0f), new Vector3(45f, 90f, 0f)));
        }
        else if (posicaoJogador2 == 3)
        {
            StartCoroutine(MudarPosicao(new Vector3(0f, 3.6f, 2.52f), new Vector3(45f, 180f, 0f)));
        }
        else if (posicaoJogador2 == 4)
        {
            StartCoroutine(MudarPosicao(new Vector3(2.52f, 3.6f, 0f), new Vector3(45f, -90f, 0f)));
        }
    }


    private void LateUpdate()
    {
        CheckWall();
    }

    void CheckWall()
    {
        //.https://onedrive.live.com/view.aspx?resid=5E38BA84B19225F0!296&ithint=file%2cpptx&authkey=!AO232s3m-pnKz-4 link para explica��o do codio
        RaycastHit hit;

        Vector3 start = target.position;
        Vector3 dir = transform.position - target.position;
        float dist;

        if (posicaoJogador2 == 1)
        {
            dist = offsetPosition.z * -1;
            dist += distanciaCollisao;
            if (Physics.Raycast(target.position, dir, out hit, dist, wallLayer))
            {
                float hitDist = hit.distance;
                Vector3 sphereCastCenter = target.position + (dir.normalized * hitDist);
                transform.position = sphereCastCenter;
            }
        }
        else if (posicaoJogador2 == 2)
        {
            dist = offsetPosition.x * -1;
            dist += distanciaCollisao;
            if (Physics.Raycast(target.position, dir, out hit, dist, wallLayer))
            {
                float hitDist = hit.distance;
                Vector3 sphereCastCenter = target.position + (dir.normalized * hitDist);
                transform.position = sphereCastCenter;
            }
        }
        else if (posicaoJogador2 == 3)
        {
            dist = offsetPosition.z * 1;
            dist += distanciaCollisao;
            if (Physics.Raycast(target.position, dir, out hit, dist, wallLayer))
            {
                float hitDist = hit.distance;
                Vector3 sphereCastCenter = target.position + (dir.normalized * hitDist);
                transform.position = sphereCastCenter;
            }
        }
        else if (posicaoJogador2 == 4)
        {
            dist = offsetPosition.x * 1;
            dist += distanciaCollisao;
            if (Physics.Raycast(target.position, dir, out hit, dist, wallLayer))
            {
                float hitDist = hit.distance;
                Vector3 sphereCastCenter = target.position + (dir.normalized * hitDist);
                transform.position = sphereCastCenter;
            }
        }

        Debug.DrawRay(target.position, dir, Color.red);

    }


    IEnumerator MudarPosicao(Vector3 position, Vector3 rotation)
    {
        offsetPosition = position;
        yield return new WaitForSeconds(delayEntreTroca);
        offsetRotation = rotation;
    }



    void TamanhoTelaPreta()
    {
        if (!ModoDeJogo.isMultiplayer)
        {
            if(ModoDeJogo.qualOjogador == 1)
            {
                fundoPreto.localScale = new Vector3(1.007025f, 1.962f, 1);
                fundoPreto.anchoredPosition = new Vector3(675, fundoPreto.anchoredPosition.y,0);
                //texto.anchoredPosition = new Vector3(44, texto.anchoredPosition.y, 0);
            }
            else if(ModoDeJogo.qualOjogador == 2)
            {
                fundoPreto.localScale = new Vector3(1.435111f, 1.962f, 1);
                fundoPreto.anchoredPosition = new Vector3(497, fundoPreto.anchoredPosition.y, 0);
            }
        }
    }
}
