using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void changeText()
    {
        transform.GetChild(0).GetComponent<Text>().text = "IT WORKED";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
