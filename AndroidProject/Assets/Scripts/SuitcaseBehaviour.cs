using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitcaseBehaviour : MonoBehaviour
{

    [SerializeField] private Camera cam;

    [SerializeField]
    private float speed = 10;
    private Vector3 direction = new Vector3(0, -1, 0);


    void Start()
    {
        //Initializes();
    }

    public void Initializes()
    {
        cam = Camera.main;
        //Create random pos in screen
        Vector3 randPos = new Vector3(Random.Range(0, cam.pixelWidth), cam.pixelHeight+20, Vector3.Distance(cam.gameObject.transform.position, Vector3.zero));
        //Pass screen to world coordinates
        randPos = cam.ScreenToWorldPoint(randPos);
        randPos.z = 0;
        
        gameObject.SetActive(true);
        transform.position = randPos;
    }

    private void Update()
    {
        if (cam.WorldToScreenPoint(transform.position).y < -20) gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
         transform.position += direction * speed * Time.deltaTime;       
    }
}
