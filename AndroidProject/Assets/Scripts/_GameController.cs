using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _GameController : MonoBehaviour
{
    public static _GameController instance = null;

    
    public float _gameDuration = 120;    
    public float gameDuration { get => _gameDuration; }

    private string targetTag;
    private int playerPoints;
    private int suitcasePoints;
    private int multiplier;
    private int combo;
    private int comboRequired;



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

    public void Start()
    {
       playerPoints = 0;
       suitcasePoints = 10;
       multiplier = 1;
       combo = 0;
       comboRequired = 10;
       targetTag = GameObject.Find("SuitcaseSpawner").GetComponent<SuitcaseSpawner>().suitcases[Random.Range(0, GameObject.Find("SuitcaseSpawner").GetComponent<SuitcaseSpawner>().suitcases.Length)].tag;
        Debug.Log(targetTag);
    }

    public void ProcessSuitcase(string tag, bool lose = false)
    {
        if (!lose)
        {
            if (tag == targetTag)
            {
                combo++;
                playerPoints += suitcasePoints * multiplier;
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
            multiplier++;
        }
        Debug.Log(playerPoints);

    }


}
