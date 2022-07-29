using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverObject : MonoBehaviour
{
    public GameObject Inspection;
    public InspectionObject inspectionObject;
    public int index;

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.E) && other.tag == "Player")
        {
           

            Inspection.SetActive(true);
            inspectionObject.TurnOnInspection(index);
          

        }
        if ((Inspection == true) && (Input.GetKeyDown(KeyCode.Escape)))
        {
            Inspection.SetActive(false);
        }


    }
}
