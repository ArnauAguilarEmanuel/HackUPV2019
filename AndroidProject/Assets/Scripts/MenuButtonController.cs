using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
        _GameController.instance.userName = userName.text;

        _GameController.instance.API.LogInUser(userName.text, flight.text);
        
    }
    public void LoadNextScene(int i)
    {
        SceneManager.LoadScene(i, LoadSceneMode.Single);
       
    }
    // Update is called once per frame
    bool loaded = false;
    void Update()
    {

        if (_GameController.instance.API.myUserAvaileable && !serverResponded)
        {
            serverResponded = true;
            GameObject.Find("Canvas").GetComponent<MenuController>().AnimationTimer = 0;
            GameObject.Find("Canvas").GetComponent<MenuController>().goToMenu = true;
            GameObject.Find("Canvas").GetComponent<MenuController>().LoggedIn = true;
            Debug.Log(_GameController.instance.currentAirport);
            _GameController.instance.API.RequesAllFlightsScores(_GameController.instance.currentAirport);
            _GameController.instance.API.RequestMyFlightScores(_GameController.instance.API.myUser.flight_id.ToString());
            _GameController.instance.API.RequestBestScores(_GameController.instance.API.myUser.user_id.ToString(), _GameController.instance.API.myUser.flight_id.ToString());
        }
    }
}
