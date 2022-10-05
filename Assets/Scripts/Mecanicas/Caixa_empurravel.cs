using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caixa_empurravel : MonoBehaviour
{
    public Transform indicador1, indicador2,indicador3,indicador4;

    public LayerMask wallLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (Player_1_Script.direcaoCaixa)
        {
            case 1:
                if (Physics.Linecast(transform.position, indicador1.position, wallLayer))
                {
                    Player_1_Script.pararDeMoverCaixa = true;
                }
                else
                {
                    Player_1_Script.pararDeMoverCaixa = false;
                }
                break;
            
            case 2:
                if (Physics.Linecast(transform.position, indicador2.position, wallLayer))
                {
                    Player_1_Script.pararDeMoverCaixa = true;
                }
                else
                {
                    Player_1_Script.pararDeMoverCaixa = false;
                }
                break;
            
            case 3:
                if (Physics.Linecast(transform.position, indicador3.position, wallLayer))
                {
                    Player_1_Script.pararDeMoverCaixa = true;
                }
                else
                {
                    Player_1_Script.pararDeMoverCaixa = false;
                }
                break;
            
            case 4:
                if (Physics.Linecast(transform.position, indicador4.position, wallLayer))
                {
                    Player_1_Script.pararDeMoverCaixa = true;
                }
                else
                {
                    Player_1_Script.pararDeMoverCaixa = false;
                }
                break;
            
            default:
                Player_1_Script.pararDeMoverCaixa = false;
                break;
                
        }

        
        
    }


}
