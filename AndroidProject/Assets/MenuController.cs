using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject logInMenu;
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject PlaneViewPrefab ;
    [SerializeField] private float animationSpeed;

    private bool loggedIn;
    public bool LoggedIn { get => loggedIn; set => loggedIn = value; }
    public float AnimationTimer { get => animationTimer; set => animationTimer = value; }

    private bool onGameMenu;
    [SerializeField] private float animationTimer;

    void Start()
    {
        
    }

    void setUpPlaneScrollView()
    {
        GameObject.Find("PlaneScrollView");
    }
    void Update()
    {
        if (loggedIn)
        {
            if (!onGameMenu)
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
                }
                else
                {
                    onGameMenu = true;
                    gameMenu.transform.localPosition = Vector3.zero;
                }
            }
            else
            {
                //Game menu logic
            }
        }
    }
}
