using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


//{"freeChampionIds":[13,22,29,41,43,53,59,75,80,83,120,142,163,201,268],"freeChampionIdsForNewPlayers":[18,81,92,141,37,238,19,45,25,64],"maxNewPlayerLevel":10}
[System.Serializable]
public class RiotResponse
{
    public int[] freeChampionIds;
    public int[] freeChampionIdsForNewPlayers;
    public int maxNewPlayerLevel;
}

[System.Serializable]
public class UserInfo
{
    public int user_id;
    public int flight_id;
    public string flight_name;
}

[System.Serializable]
public class EndGameSendInfo
{
    public int user_id  ;
    public int flight_id;
    public int score;
    public int multiplayer;
}
[System.Serializable]
public class EndGameReturnInfo
{
    public int total_score;
    public EndGameSendInfo[] top_5;
}

public class TestScript : MonoBehaviour
{
    private const string URL = "https://euw1.api.riotgames.com/lol/platform/v3/champion-rotations";
    private const string LOG_IN_URL = "192.168.43.165/api/login";
    private const string END_GAME_URL = "192.168.43.165/api/endgame";
    private const string KEY = "RGAPI-7188bdaf-74c7-483d-a766-c4bb581a69ba";
    private Text response;

    public UserInfo myUser;
    public bool myUserAvaileable;

    public EndGameReturnInfo gameReturnedInfo;
    public bool gameReturnedInfoAvaileable;

    public void request()
    {
        StartCoroutine(OnResponse());
    }
    public void LogInUser(string userName, string Flight)
    {
        myUserAvaileable = false;
        StartCoroutine(LogIn(userName, Flight));
    }
    public void RequestEndGame(string userId, string FlightId, int score, float multiplayer)
    {
        gameReturnedInfoAvaileable = false;
        StartCoroutine(EndGame(userId, FlightId, score, multiplayer));
    }

    public IEnumerator LogIn(string userName, string Flight)
    {
        WWWForm form = new WWWForm();
        form.AddField("user_name", userName);
        form.AddField("flight_name", Flight);
        using (UnityWebRequest req = UnityWebRequest.Post(LOG_IN_URL, form))
        {
            yield return req.SendWebRequest();

            if (req.isNetworkError)
            {
                Debug.Log(req.error);
            }else if(req.isHttpError)
            {
                Debug.Log(req.error);
            }
            else
            {
                string data = req.downloadHandler.text;
                transform.root.GetChild(1).GetComponent<Text>().text = data;

                myUser = JsonUtility.FromJson<UserInfo>(data);
                myUserAvaileable = true;
            }
        }
    }

    public IEnumerator EndGame(string userId, string FlightId, int score, float multiplayer)
    {
        WWWForm form = new WWWForm();
        form.AddField("user_id", userId);
        form.AddField("flight_id", FlightId);
        form.AddField("score", score);
        form.AddField("multiplayer", multiplayer.ToString());
        using (UnityWebRequest req = UnityWebRequest.Post(END_GAME_URL, form))
        {
            yield return req.SendWebRequest();

            if (req.isNetworkError)
            {
                Debug.Log(req.error);
            }
            else if (req.isHttpError)
            {
                Debug.Log(req.error);
            }
            else
            {
                string data = req.downloadHandler.text;
                transform.root.GetChild(1).GetComponent<Text>().text = data;

                gameReturnedInfo = JsonUtility.FromJson<EndGameReturnInfo>(data);
                gameReturnedInfoAvaileable = true;
            }
        }
    }


    IEnumerator OnResponse()
    {
        UnityWebRequest req = UnityWebRequest.Get(URL);
        req.SetRequestHeader("X-Riot-Token", KEY);

        yield return req.SendWebRequest();
        string data = req.downloadHandler.text;
        transform.root.GetChild(1).GetComponent<Text>().text = data;

        RiotResponse resp = new RiotResponse();
        resp = JsonUtility.FromJson<RiotResponse>(data);

        Debug.Log(resp.maxNewPlayerLevel);
        Debug.Log(resp.freeChampionIds[2]);

        Debug.Log(req.downloadHandler.text);
        
    }

    // Update is called once per frame
    void Start()
    {
        LogInUser("Arnau", "Flight");
    }
    private bool auxiliar;
    private bool auxiliar2;
    private void Update()
    {
        if (myUserAvaileable)
        {
            if (!auxiliar)
            {
                Debug.Log(myUser.user_id);
                Debug.Log(myUser.flight_id);
                Debug.Log(myUser.flight_name);
                Debug.Log("________________________________________________");
                auxiliar = true;
                RequestEndGame(myUser.user_id.ToString(), myUser.flight_id.ToString(), 50000, 12);
            }
            if (gameReturnedInfoAvaileable && !auxiliar2)
            {
                Debug.Log(gameReturnedInfo.total_score);
                foreach(EndGameSendInfo inf in gameReturnedInfo.top_5)
                {
                    Debug.Log(inf.user_id);
                    Debug.Log(inf.flight_id);
                    Debug.Log(inf.score);
                    Debug.Log(inf.multiplayer);
                }
                auxiliar2 = true;
            }
        }
        else
        {
            Debug.Log("not availeable");
        }
    }
}
