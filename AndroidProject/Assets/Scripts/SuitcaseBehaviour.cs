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

    private bool _left;
    public bool left { get => _left; set => _left = value; }


    void Start()
    {
        //Initializes();
    }
    
    public void Initializes()
    {
        left = false;
        cam = Camera.main;
        //Create random pos in screen                   //  6%                  -32%
        Vector3 randPos = new Vector3(Random.Range(cam.pixelWidth/100*6, cam.pixelWidth - cam.pixelWidth / 100 * 6), cam.pixelHeight+20, Vector3.Distance(cam.gameObject.transform.position, Vector3.zero));
        //Pass screen to world coordinates
        randPos = cam.ScreenToWorldPoint(randPos);
        randPos.z = 0;
        
        transform.position = randPos;
    }

    private void Update()
    {
        //If arrive bottom
        if (cam.WorldToScreenPoint(transform.position).y < -100)
        {
            _GameController.instance.ProcessSuitcase(gameObject.tag, true);
            gameObject.SetActive(false);
            picked = false;
        }
        if (picked)
        {
            transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Vector3.Distance(cam.gameObject.transform.position, Vector3.zero)));
        }
        if (left)
        {
            picked = false;
        }
    }

    void FixedUpdate()
    {
        if(left) transform.position += Vector3.right * speed * Time.deltaTime;
        else if(!picked)transform.position += direction * speed * Time.deltaTime;  
        if(transform.position.x > 10)
        {
            _GameController.instance.ProcessSuitcase(gameObject.tag);
            gameObject.SetActive(false);
        }
    }
}
