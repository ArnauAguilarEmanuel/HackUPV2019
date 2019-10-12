﻿using System.Collections;
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
    public int id;
    public int user_id;
    public int flight_id;
    public float score;
    public float multiplier;
}
[System.Serializable]
public class GameInfo
{
    public int id;
    public int user_id;
    public int flight_id;
    public float score;//score made 
    public float multiplier;
}

[System.Serializable]
public class EndGameReturnInfo
{
    public float total_score;
    public float flight_total_score;
    public float flight_multiplier;
    public EndGameSendInfo[] top_5;
}

[System.Serializable]
public class basicTop5
{
    public EndGameSendInfo[] top_5;
}

[System.Serializable]
public class playerScores
{
    public string number;
    public int user_id;
    public string name;
    public float total_score;
    public float total_multiplier;
}

[System.Serializable]
public class planeScore
{
    public int flight_id;
    public string number;
    public float total_score;
}
[System.Serializable]
public class allAirportScores
{
    public planeScore[] airport_scores;
}

[System.Serializable]
public class MyFlightScores
{
    public playerScores[] total_score;
}


public class API_Comunication : MonoBehaviour
{
    private const string URL = "https://euw1.api.riotgames.com/lol/platform/v3/champion-rotations";
    private const string LOG_IN_URL = "http://269d78cc.ngrok.io/api/login";         ////// "http://bb3b86bd.ngrok.io/api/login";
    private const string END_GAME_URL = "http://269d78cc.ngrok.io/api/endgame";
    private const string TOP5_URL = "http://269d78cc.ngrok.io/api/top5";
    private const string FLIGHT_SCORES_URL = "http://269d78cc.ngrok.io/api/flightscores";
    private const string ALL_FLIGHTS_SCORES_URL = "269d78cc.ngrok.io/api/airportscores";
    private const string KEY = "RGAPI-7188bdaf-74c7-483d-a766-c4bb581a69ba";
    private Text response;

    public basicTop5 top5;
    public bool top5Availeable;

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

    public void RequestBestScores(string userId, string FlightId)
    {
        myUserAvaileable = false;
        StartCoroutine(Top5(userId, FlightId));
    }

    public void RequestEndGame(string userId, string FlightId, float score, float multiplayer)
    {
        gameReturnedInfoAvaileable = false;
        StartCoroutine(EndGame(userId, FlightId, score, multiplayer));
    }

    public void RequestMyFlightScores(string FlightId)
    {
        flightScoresAvaileable = false;
        StartCoroutine(GetMyFlightScores(FlightId));
    }
    public void RequesAllFlightsScores(string FlightId)
    {
        StartCoroutine(GetAllFlightsScores(FlightId));
    }

    public IEnumerator Top5(string userId, string FlightId)
    {
        WWWForm form = new WWWForm();
        form.AddField("user_id", userId);
        form.AddField("flight_id", FlightId);
        using (UnityWebRequest req = UnityWebRequest.Post(TOP5_URL, form))
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

                top5 = JsonUtility.FromJson<basicTop5>(data);
                top5Availeable = true;
            }
        }
    }


    public IEnumerator LogIn(string userName, string Flight)
    {
        WWWForm form = new WWWForm();
        form.AddField("user_name", userName);
        form.AddField("flight_name", Flight);
        form.AddField("airport_id", 1.ToString());
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
        Debug.Log("helo?");
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

    public IEnumerator EndGame(string userId, string FlightId, float score, float multiplayer)
    {
        WWWForm form = new WWWForm();
        form.AddField("user_id", userId);
        form.AddField("flight_id", FlightId);
        form.AddField("score", score.ToString());
        form.AddField("multiplier", multiplayer.ToString());
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

    public IEnumerator GetAllFlightsScores(string FlightId)
    {
        WWWForm form = new WWWForm();
        form.AddField("airport_id", FlightId);
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
