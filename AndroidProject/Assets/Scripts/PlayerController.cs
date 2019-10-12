using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    GameObject pickedObj;
    // Update is called once per frame
    void Update()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(mouseRay, out hit, 50) && !pickedObj) {
            pickedObj = hit.collider.gameObject;
            pickedObj.GetComponent<SuitcaseBehaviour>().picked = true;
        }

       if (!Input.GetMouseButton(0) && pickedObj) {
            pickedObj.GetComponent<SuitcaseBehaviour>().picked = false;
            pickedObj = null;
       }
       

    }
}
