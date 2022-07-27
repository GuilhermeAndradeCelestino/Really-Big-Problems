using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectionObject : MonoBehaviour
{
    public GameObject[] inspectionObjects;
    private int currIndex;

    public void TurnOnInspection(int Index)
    {
        currIndex = Index;
        inspectionObjects[Index].SetActive(true);
    }

    public void TurnOffInspection()
    {
        inspectionObjects[currIndex].SetActive(false);
    }
}
