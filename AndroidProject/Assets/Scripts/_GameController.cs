using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ExtensionMethods
{

    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

}
public class _GameController : MonoBehaviour
{
    public static _GameController instance = null;

    public string currentAirport = "1";
    
    public float _gameDuration = 5;
    public float _targgetChangeTime = 20;
    private float actualChangeTime = 20;
    private float colorOriginalSize;
    private float decresSpeed = 100;
    public string userName;

    public float gameDuration { get => _gameDuration; }

    private string targetTag;
    private float playerPoints;
    private int suitcasePoints;
    [SerializeField]private float multiplier;
    private int combo;
    private int comboRequired;
    public API_Comunication API;
    private GameObject scoreUI;


    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

        //if not, set instance to this
        instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

        //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
        Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnGameStart;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnGameStart;
    }
    public void OnGameStart(Scene scene, LoadSceneMode mode)
    {
        API = GetComponent<API_Comunication>();
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
           actualChangeTime = 0;
           playerPoints = 0;
           suitcasePoints = 10;
           multiplier = 1;
           combo = 0;
           comboRequired = 10;
           targetTag = GameObject.Find("SuitcaseSpawner").GetComponent<SuitcaseSpawner>().suitcases[Random.Range(0, GameObject.Find("SuitcaseSpawner").GetComponent<SuitcaseSpawner>().suitcases.Length)].tag;
           Debug.Log(targetTag);
           scoreUI = GameObject.Find("Score");
            colorOriginalSize = GameObject.Find("Color").GetComponent<TextMeshProUGUI>().fontSize;
           StartCoroutine(changeTargetTag());
        }
    }

    public void ChangeScene(int i){
        SceneManager.LoadScene(i,LoadSceneMode.Single);
    }

    IEnumerator changeTargetTag()
    {
        yield return new WaitForSeconds(actualChangeTime);
        targetTag = GameObject.Find("SuitcaseSpawner").GetComponent<SuitcaseSpawner>().suitcases[Random.Range(0, GameObject.Find("SuitcaseSpawner").GetComponent<SuitcaseSpawner>().suitcases.Length)].tag;
        TextMeshProUGUI text = GameObject.Find("Color").GetComponent<TextMeshProUGUI>();
        text.fontSize *= 1.5f;
        if(targetTag == "S_Green")
        {
            text.text = "GREEN";
            text.color = new Color32(0, 202, 21, 255);
        }
        else if(targetTag == "S_White")
        {
            text.text = "WHITE";
            text.color = new Color32(255, 255, 255, 255);
        }
        else if(targetTag == "S_Red")
        {
            text.text = "RED";
            text.color = new Color32(255, 0, 0, 255);
        }
        else
        {
            text.text = "BLUE";
            text.color = new Color32(0, 139, 255, 255);
        }
        actualChangeTime = _targgetChangeTime;
        StartCoroutine(changeTargetTag());
    }

    private bool state1 = false, state2 = false, state3 = false;
    private bool requestedEndGame;

    [SerializeField]
    private float speed = 10;
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 && GameObject.Find("Color").GetComponent<TextMeshProUGUI>().fontSize > colorOriginalSize) GameObject.Find("Color").GetComponent<TextMeshProUGUI>().fontSize -= Time.deltaTime * decresSpeed;
        if (GameObject.Find("SuitcaseSpawner").GetComponent<SuitcaseSpawner>().globalTimer > gameDuration)
        {
            if (!requestedEndGame)
            {
                API.RequestEndGame(API.myUser.user_id.ToString(), API.myUser.flight_id.ToString(), playerPoints, multiplier);
                requestedEndGame = true;
            }
            if (!state1)
            {
                if (GameObject.Find("Score").transform.localPosition.y > 450)
                {
                    GameObject.Find("Score").transform.localPosition -= new Vector3(0, speed * Time.deltaTime, GameObject.Find("Score").transform.localPosition.z);
                }
                else state1 = true;
            }
            else if (!state2)
            {
                GameObject.Find("Score").GetComponent<TextMeshProUGUI>().text = GameObject.Find("Score").GetComponent<TextMeshProUGUI>().text + "<b> x" + API.gameReturnedInfo.flight_multiplier + "<b>";
                state2 = true;
            }
            else if (!state3)
            {
                state3 = true;
                StartCoroutine(SetMultiplier());
            }
        }
    }

    public IEnumerator SetMultiplier()
    {
        yield return new WaitForSeconds(1.5f);
        GameObject.Find("Score").GetComponent<TextMeshProUGUI>().text = GameObject.Find("Score").GetComponent<TextMeshProUGUI>().text += "\n" +API.gameReturnedInfo.total_score;
    }

    public void ProcessSuitcase(string tag, bool lose = false)
    {
        if (!lose)
        {
            if (tag == targetTag)
            {
                combo++;
                playerPoints += suitcasePoints * multiplier;
                scoreUI.GetComponent<ScoreUiManager>().add(suitcasePoints * multiplier);

            }
        }
        else
        {
            if (tag == targetTag)
            {
                combo = 0;
            }
        }
        
        if(combo >= comboRequired)
        {
            combo = 0;
            multiplier+=0.1f;
        }

    }


}
