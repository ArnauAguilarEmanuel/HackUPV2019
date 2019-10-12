﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitcaseSpawner : MonoBehaviour
{

    public GameObject[] suitcases;

    private List<GameObject> suitcasesList = new List<GameObject>();

    const int MAX_SUITCASES = 50;

    [SerializeField]
    private float spawnSpeed;

    private float globalTimer;
    private float spawnTimer;



    void Start()
    {
        spawnTimer = spawnSpeed;

        GameObject temp;
        foreach(GameObject o in suitcases)
        {
            for(int i = 0; i < (int)(MAX_SUITCASES/ suitcases.Length); i++)
            {
                temp = Instantiate(o);
                temp.SetActive(false);
                suitcasesList.Add(temp);
            }
        }
    }


    void Update()
    {
        //In game
        if(globalTimer < _GameController.instance.gameDuration)
        {
            //Time to spawn suitcase
            if (spawnTimer >= spawnSpeed)
            {
                if(suitcasesList.Count > 0)
                {
                    GameObject target;
                    do
                    {
                        target = suitcasesList[Random.Range(0, suitcasesList.Count)];
                    }
                    while (target.activeSelf != false);
                    target.GetComponent<SuitcaseBehaviour>().Initializes();
                }
                
                spawnTimer = 0;
            }

            spawnTimer += Time.deltaTime;
            globalTimer += Time.deltaTime;
        }
    }
}
