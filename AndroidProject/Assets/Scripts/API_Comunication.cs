using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


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
    private const string PATH = "http://c2c4f364.ngrok.io";
    private const string LOG_IN_URL = PATH  + "/api/login";         ////// "http://bb3b86bd.ngrok.io/api/login";
    private const string END_GAME_URL = PATH + "/api/endgame";
    private const string TOP5_URL = PATH  + "/api/top5";
    private const string FLIGHT_SCORES_URL = PATH + "/api/flightscores";
    private const string ALL_FLIGHTS_SCORES_URL = PATH + "/api/airportscores";


    public basicTop5 top5 = new basicTop5();
    public bool top5Availeable;

    public UserInfo myUser = new UserInfo(); 
    public bool myUserAvaileable;

    public EndGameReturnInfo gameReturnedInfo = new EndGameReturnInfo();
    public bool gameReturnedInfoAvaileable;

    public MyFlightScores flightScores = new MyFlightScores();
    public bool flightScoresAvaileable;

    public allAirportScores airportRanking = new allAirportScores();
    public bool airportRankingAvaileable;

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

                //top5 = JsonUtility.FromJson<basicTop5>(data);
                JsonUtility.FromJsonOverwrite(data, top5);
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

                //myUser = JsonUtility.FromJson<UserInfo>(data);
                JsonUtility.FromJsonOverwrite(data, myUser);
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
                //flightScores = JsonUtility.FromJson<MyFlightScores>(data);
                JsonUtility.FromJsonOverwrite(data, flightScores);
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

                //gameReturnedInfo = JsonUtility.FromJson<EndGameReturnInfo>(data);
                JsonUtility.FromJsonOverwrite(data, gameReturnedInfo);
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
                //airportRanking = JsonUtility.FromJson<allAirportScores>(data);
                JsonUtility.FromJsonOverwrite(data, airportRanking);
                airportRankingAvaileable = true;
            }
        }
    }
}
