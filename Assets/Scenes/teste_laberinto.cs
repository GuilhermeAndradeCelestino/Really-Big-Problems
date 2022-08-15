using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teste_laberinto : MonoBehaviour
{
    public int x;
    public int y;

    public int[] coordenadax;
    public int[] coordenaday;

    public int[,] array = new int[10,10];

    // Start is called before the first frame update
    void Start()
    {
        coordenadax = new int[x];
        coordenaday = new int[y];
            
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
