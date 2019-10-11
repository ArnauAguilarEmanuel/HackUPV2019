using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitcaseSpawner : MonoBehaviour
{

    public GameObject[] Suitcases;

    const int MAX_SUITCASES = 50;

    [SerializeField]
    private float spawnSpeed;

    private float globalTimer;
    private float spawnTimer;



    void Start()
    {
        spawnTimer = 0;
    }


    void Update()
    {
        //In game
        if(globalTimer < _GameController.instance.gameDuration)
        {
            //Time to spawn suitcase
            if (spawnTimer >= spawnSpeed)
            {
                Instantiate(Suitcases[Random.Range(0, Suitcases.Length)]);
                spawnTimer = 0;
            }

            spawnTimer += Time.deltaTime;
            globalTimer += Time.deltaTime;
        }
    }
}
