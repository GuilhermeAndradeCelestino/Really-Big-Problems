using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caixa_empurravel : MonoBehaviour
{
    public Transform indicador1, indicador2,indicador3,indicador4;

    public LayerMask wallLayer;

    bool pararIndicador = false;


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
                
                
                if (Physics.CheckBox(indicador1.position, indicador1.localScale, indicador1.rotation,wallLayer))
                {
                    Player_1_Script.pararDeMoverCaixa = true;
                }
                else
                {
                    Player_1_Script.pararDeMoverCaixa = false;
                }
                
                break;
                
            case 2:

                if (Physics.CheckBox(indicador2.position, indicador2.localScale, indicador2.rotation, wallLayer))
                {
                    Player_1_Script.pararDeMoverCaixa = true;
                }
                else
                {
                    Player_1_Script.pararDeMoverCaixa = false;
                }
                break;
                
            case 3:

                if (Physics.CheckBox(indicador3.position, indicador3.localScale, indicador3.rotation, wallLayer))
                {
                    Player_1_Script.pararDeMoverCaixa = true;
                }
                else
                {
                    Player_1_Script.pararDeMoverCaixa = false;
                }
                break;
                
            case 4:

                if (Physics.CheckBox(indicador4.position, indicador4.localScale, indicador4.rotation, wallLayer))
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

    void checarParede()
    {
       
    }
}
