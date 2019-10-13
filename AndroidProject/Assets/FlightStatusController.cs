using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlightStatusController : MonoBehaviour
{

    TextMeshProUGUI TMP;
    Color original;
    // Start is called before the first frame update
    void Start()
    {
        TMP = GetComponent<TextMeshProUGUI>();
        original = TMP.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (_GameController.instance.API.myUserAvaileable)
        {
            switch (_GameController.instance.API.myUser.status)
            {
                case "Unknown"://grey
                case null:
                    TMP.color = original;
                    break;
                case "Expected"://green
                    TMP.color = new Color32(0, 132, 2, 255);
                    break;
                case "Canceled"://Red
                case "Departed":
                    TMP.color = new Color32(132, 21, 0, 255);
                    break;
                default://orange
                    TMP.color = new Color32(198, 114, 0, 255);
                    break;

                    
            }
        }
    }
}
