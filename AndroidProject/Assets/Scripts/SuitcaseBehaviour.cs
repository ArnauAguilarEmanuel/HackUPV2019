using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitcaseBehaviour : MonoBehaviour
{

    [SerializeField] private Camera cam;

    [SerializeField]
    private float speed = 10;
    private Vector3 direction = new Vector3(0, -1, 0);

    private bool _picked;
    public bool picked { get => _picked; set => _picked = value; }


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
        //If arrive bottom
        if (cam.WorldToScreenPoint(transform.position).y < -20)
        {
            _GameController.instance.ProcessSuitcase(gameObject.tag, true);
            gameObject.SetActive(false);
        }
        if (picked)
        {
            transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Vector3.Distance(cam.gameObject.transform.position, Vector3.zero)));
        }
    }

    void FixedUpdate()
    {
         if(!picked)transform.position += direction * speed * Time.deltaTime;       
    }
}
