using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitcasesCollector : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colision");
        _GameController.instance.ProcessSuitcase(other.gameObject.tag);
        other.gameObject.SetActive(false);
    }

}
