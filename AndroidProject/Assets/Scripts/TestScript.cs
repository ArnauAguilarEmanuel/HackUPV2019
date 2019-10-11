using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

//{"freeChampionIds":[13,22,29,41,43,53,59,75,80,83,120,142,163,201,268],"freeChampionIdsForNewPlayers":[18,81,92,141,37,238,19,45,25,64],"maxNewPlayerLevel":10}
public class RiotResponse
{
    public int[] freeChampionIds;
    public int[] freeChampionIdsForNewPlayers;
    public int maxNewPlayerLevel;
}

public class TestScript : MonoBehaviour
{
    private const string URL = "https://euw1.api.riotgames.com/lol/platform/v3/champion-rotations";
    private const string KEY = "RGAPI-7188bdaf-74c7-483d-a766-c4bb581a69ba";
    private Text response;

    public void request()
    {
        StartCoroutine(OnResponse());
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
    void Update()
    {
        
    }
}
