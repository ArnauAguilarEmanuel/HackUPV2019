using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitcasesCollector : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<SuitcaseBehaviour>()) other.GetComponent<SuitcaseBehaviour>().left = true;

        
    }

}
