using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject logInMenu;
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject PlaneViewPrefab ;
    [SerializeField] private float animationSpeed;

    private bool loggedIn;
    public bool LoggedIn { get => loggedIn; set => loggedIn = value; }
    public float AnimationTimer { get => animationTimer; set => animationTimer = value; }

    [SerializeField] private float animationTimer;
    private bool recivedResponse, instantiate;

    public bool goToMenu;
    public enum menu { Airport, User, Flight, LoggIn};
    public menu current = menu.LoggIn;

    [HideInInspector] public float[] highScores;/// <summary>
    /// implement
    /// </summary>

    public void SetMenuToUser() { current = menu.User; animationTimer = 0; }
    public void SetMenuToAirport() { current = menu.Airport; animationTimer = 0; }
    public void SetMenuToFlight() { current = menu.Flight; animationTimer = 0; }


    void setUpPlaneScrollView()
    {
        GameObject.Find("PlaneScrollView");
    }
    void Update()
    {
        if (loggedIn)
        {
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
                    if (recivedResponse && !instantiate)
                    {
                        GameObject target = GameObject.Find("PlaneScrollView").transform.GetChild(0).GetChild(0).gameObject;
                        target.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 220 * 200);
                        for (int i = 0; i < 200; i++)
                        {
                            GameObject aux = Instantiate(PlaneViewPrefab, GameObject.Find("PlaneScrollView").transform.GetChild(0).GetChild(0).transform);
                            aux.transform.position += new Vector3(0, target.GetComponent<RectTransform>().rect.height / 2 - 40 - i * 220, 0);
                            aux.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = i.ToString();
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
                    if (Mathf.Abs(gameMenu.transform.localPosition.x) < 5f) gameMenu.transform.localPosition = new Vector3(0, gameMenu.transform.localPosition.y, gameMenu.transform.localPosition.z);
                    animationTimer += Time.deltaTime;
                    Debug.Log(Mathf.Abs(gameMenu.transform.localPosition.x));
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
    }
}
