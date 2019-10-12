using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuButtonController : MonoBehaviour
{

    private bool serverResponded = false;
    private TMP_InputField userName;
    private TMP_InputField flight;

    
    // Start is called before the first frame update
    void Start()
    {
        userName = GameObject.Find("UserNameInput").GetComponent<TMP_InputField>();
        flight = GameObject.Find("FlighInput").GetComponent<TMP_InputField>();
    }

    public void LoadNextLevel()
    {
        serverResponded = true;
        GameObject.Find("Canvas").GetComponent<MenuController>().AnimationTimer = 0;
        GameObject.Find("Canvas").GetComponent<MenuController>().goToMenu = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (serverResponded)
        {
            //load next scene
            //GameObject.Find("Debug").GetComponent<TextMeshProUGUI>().text = userName.text + " " + flight.text;
            GameObject.Find("Canvas").GetComponent<MenuController>().LoggedIn = true;
        }
    }
}
