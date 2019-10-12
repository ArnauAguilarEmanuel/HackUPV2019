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

[System.Serializable]
public class playerScores
{
    public int user_id;
    public int total_score;
    public int total_multiplayer;
}

public class planeScore
{
    public int flight_id;
    public int total_score;
    public int number;
}
public class allAirportScores
{
    public planeScore[] airport_scores;
}

[System.Serializable]
public class MyFlightScores
{
    public playerScores[] player_scores;
}

public class API_Comunication : MonoBehaviour
{
    private const string URL = "https://euw1.api.riotgames.com/lol/platform/v3/champion-rotations";
    private const string LOG_IN_URL = "http://269d78cc.ngrok.io/api/login";         ////// "http://bb3b86bd.ngrok.io/api/login";
    private const string END_GAME_URL = "192.168.43.165/api/endgame";
    private const string FLIGHT_SCORES_URL = "192.168.43.165/api/flightscores";
    private const string ALL_FLIGHTS_SCORES_URL = "2";
    private const string KEY = "RGAPI-7188bdaf-74c7-483d-a766-c4bb581a69ba";
    private Text response;

    public UserInfo myUser; 
    public bool myUserAvaileable;

    public EndGameReturnInfo gameReturnedInfo;
    public bool gameReturnedInfoAvaileable;

    public MyFlightScores flightScores;
    public bool flightScoresAvaileable;

    public allAirportScores airportRanking;
    public bool airportRankingAvaileable;

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

    public void RequestMyFlightScores(string FlightId)
    {
        StartCoroutine(GetMyFlightScores(FlightId));
    }
    public void RequesAllFlightsScores()
    {
        StartCoroutine(GetAllFlightsScores());
    }

    public IEnumerator LogIn(string userName, string Flight)
    {
        WWWForm form = new WWWForm();
        form.AddField("user_name", userName);
        form.AddField("flight_name", Flight);
        form.AddField("airport_id", 1);
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

                myUser = JsonUtility.FromJson<UserInfo>(data);
                myUserAvaileable = true;
            }
        }
    }

    public IEnumerator GetMyFlightScores(string FlightId)
    {
        WWWForm form = new WWWForm();
        form.AddField("flight_id", FlightId);
        using (UnityWebRequest req = UnityWebRequest.Post(FLIGHT_SCORES_URL, form))
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

                flightScores = JsonUtility.FromJson<MyFlightScores>(data);
                flightScoresAvaileable = true;
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

                gameReturnedInfo = JsonUtility.FromJson<EndGameReturnInfo>(data);
                gameReturnedInfoAvaileable = true;
            }
        }
    }

    public IEnumerator GetAllFlightsScores()
    {
        WWWForm form = new WWWForm();
        using (UnityWebRequest req = UnityWebRequest.Post(ALL_FLIGHTS_SCORES_URL, form))
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

                airportRanking = JsonUtility.FromJson<allAirportScores>(data);
                airportRankingAvaileable = true;
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

}
