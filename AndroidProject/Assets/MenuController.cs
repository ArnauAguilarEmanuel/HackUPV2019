using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject logInMenu;
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject PlaneViewPrefab;
    [SerializeField] private GameObject FlightViewPrefab;
    [SerializeField] private float animationSpeed;

    private bool loggedIn;
    public bool LoggedIn { get => loggedIn; set => loggedIn = value; }
    public float AnimationTimer { get => animationTimer; set => animationTimer = value; }

    [SerializeField] private float animationTimer;
    private bool recivedResponse, instantiate;

    public bool goToMenu;
    public enum menu { Airport, User, Flight, LoggIn};
    public menu current = menu.LoggIn;

    private bool UserScoresResponded = true;

    private bool OtherUsersScores = true;
    private Vector2 startMousePos, endMousePos;
    private float[] highScores = new float[5]{10000,1000,100,10,1 };/// <summary>
    /// implement
    /// </summary>

    public void SetMenuToUser() { current = menu.User; animationTimer = 0; }
    public void SetMenuToAirport() { current = menu.Airport; animationTimer = 0; }
    public void SetMenuToFlight() { current = menu.Flight; animationTimer = 0; }


    void setUpPlaneScrollView()
    {
        GameObject.Find("PlaneScrollView");
    }

    void ChangeMenu(Vector2 direction) {
        if (direction.x > 400)
        {
            if (current == menu.Flight)
            {
                current = menu.Airport;
                animationTimer = 0;
            }
            else if (current == menu.Airport)
            {
                current = menu.User;
                animationTimer = 0;
            }
        }
        else if (direction.x < -400) {
            if (current == menu.User)
            {
                current = menu.Airport;
                animationTimer = 0;
            }
            else if (current == menu.Airport)
            {
                current = menu.Flight;
                animationTimer = 0;
            }
        }
    }
    void Update()
    {
        if (loggedIn)
        {
            if (_GameController.instance.API.top5Availeable)
            {
                if(_GameController.instance.API.top5.top_5.Length > 0) {
                    GameObject.Find("Score").GetComponent<TextMeshProUGUI>().text = _GameController.instance.API.top5.top_5[0].score.ToString();
                    GameObject.Find("Score (1)").GetComponent<TextMeshProUGUI>().text = _GameController.instance.API.top5.top_5[1].score.ToString();
                    GameObject.Find("Score (2)").GetComponent<TextMeshProUGUI>().text = _GameController.instance.API.top5.top_5[2].score.ToString();
                    GameObject.Find("Score (3)").GetComponent<TextMeshProUGUI>().text = _GameController.instance.API.top5.top_5[3].score.ToString();
                    GameObject.Find("Score (4)").GetComponent<TextMeshProUGUI>().text = _GameController.instance.API.top5.top_5[4].score.ToString();
                    GameObject.Find("Name").GetComponent<TextMeshProUGUI>().text = _GameController.instance.userName;
                    GameObject.Find("FlightNumber").GetComponent<TextMeshProUGUI>().text = _GameController.instance.API.myUser.flight_name;
                    UserScoresResponded = false;
                }
            }
            if (_GameController.instance.API.flightScoresAvaileable) {
                GameObject target = GameObject.Find("FlightScrollView").transform.GetChild(0).GetChild(0).gameObject;
                target.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 220 * 200);
                for (int i = 0; i < _GameController.instance.API.flightScores.total_score.Length; i++)
                {
                    GameObject aux = Instantiate(FlightViewPrefab, GameObject.Find("FlightScrollView").transform.GetChild(0).GetChild(0).transform);
                    aux.transform.position += new Vector3(0, target.GetComponent<RectTransform>().sizeDelta.y / 2 - 40 - i * 220, 0);
                    aux.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "x"+_GameController.instance.API.flightScores.total_score[i].total_multiplier.ToString();
                    aux.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = _GameController.instance.API.flightScores.total_score[i].name;
                    aux.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = _GameController.instance.API.flightScores.total_score[i].total_score.ToString();
                }
                OtherUsersScores = false;
            }

            if (current == menu.LoggIn)
            {
                if (gameMenu.GetComponent<RectTransform>().localPosition.y < 0)
                {
                    if (animationTimer < 0.6f)
                    {
                        logInMenu.transform.position -= new Vector3(0, Time.deltaTime * animationSpeed * animationTimer.Remap(0, 0.6f, 1, 0), 0);
                        gameMenu.transform.position -= new Vector3(0, Time.deltaTime * animationSpeed * animationTimer.Remap(0, 0.6f, 1, 0), 0);
                    }
                    else
                    {
                        logInMenu.transform.position += new Vector3(0, Time.deltaTime * animationSpeed * 10 * animationTimer.Remap(0.6f, 5, 0, 10), 0);
                        gameMenu.transform.position += new Vector3(0, Time.deltaTime * animationSpeed * 10 * animationTimer.Remap(0.6f, 5, 0, 10), 0);
                    }

                    animationTimer += Time.deltaTime;
                    recivedResponse = true;
                    if (_GameController.instance.API.airportRankingAvaileable && !instantiate)
                    {
                        GameObject target = GameObject.Find("PlaneScrollView").transform.GetChild(0).GetChild(0).gameObject;
                        target.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 220 * _GameController.instance.API.airportRanking.airport_scores.Length);
                        for (int i = 0; i < _GameController.instance.API.airportRanking.airport_scores.Length; i++)
                        {
                            GameObject aux = Instantiate(PlaneViewPrefab, GameObject.Find("PlaneScrollView").transform.GetChild(0).GetChild(0).transform);
                            aux.transform.position += new Vector3(0, target.GetComponent<RectTransform>().sizeDelta.y / 2 - 40 - i * 220, 0);
                            aux.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();//set Position number
                            aux.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = _GameController.instance.API.airportRanking.airport_scores[i].number.ToString();///set plane name
                            aux.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = _GameController.instance.API.airportRanking.airport_scores[i].total_score.ToString();
                        }
                        instantiate = true;
                    }
                }
                else
                {
                    current = menu.Airport;
                    gameMenu.transform.localPosition = Vector3.zero;
                    GameObject.Find("TopBar").transform.parent = transform;
                }
            }
            else if(current == menu.Airport)
            {
                if (gameMenu.GetComponent<RectTransform>().localPosition.x != 0)
                {
                    gameMenu.transform.position += (gameMenu.GetComponent<RectTransform>().localPosition.x<0)?  new Vector3(Time.deltaTime * animationSpeed * 10 * animationTimer, 0, 0): -new Vector3(Time.deltaTime * animationSpeed * 10 * animationTimer, 0, 0);
                    if (Mathf.Abs(gameMenu.transform.localPosition.x) < 50f) gameMenu.transform.localPosition = new Vector3(0, gameMenu.transform.localPosition.y, gameMenu.transform.localPosition.z);
                    animationTimer += Time.deltaTime;
                }
                else
                {
                    
                }
            }
            else if(current == menu.User)
            {
                if(gameMenu.GetComponent<RectTransform>().localPosition.x < 1080)
                {
                    gameMenu.transform.position += new Vector3(Time.deltaTime * animationSpeed * 10 * animationTimer, 0, 0);
                    if (Mathf.Abs(gameMenu.transform.localPosition.x - 1080) < 0.5f) gameMenu.transform.localPosition = new Vector3(1080, gameMenu.transform.localPosition.y, gameMenu.transform.localPosition.z);
                    animationTimer += Time.deltaTime;
                }
            }
            else if(current == menu.Flight)
            {
                if (gameMenu.GetComponent<RectTransform>().localPosition.x > -1080)
                {
                    gameMenu.transform.position -= new Vector3(Time.deltaTime * animationSpeed * 10 * animationTimer, 0, 0);
                    if (Mathf.Abs(gameMenu.transform.localPosition.x - 1080) < 0.5f) gameMenu.transform.localPosition = new Vector3(-1080, gameMenu.transform.localPosition.y, gameMenu.transform.localPosition.z);
                    animationTimer += Time.deltaTime;
                }
            }
        }
        if (Input.GetMouseButtonDown(0)) startMousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        if (Input.GetMouseButtonUp(0))
        {
            endMousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            ChangeMenu(endMousePos - startMousePos);
        }

    }
}
