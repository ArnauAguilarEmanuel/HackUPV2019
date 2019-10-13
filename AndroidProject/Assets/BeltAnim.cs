using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltAnim : MonoBehaviour
{

    public List<GameObject> myBelt;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Vector3 origialPosition;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject o in myBelt)
        {
            o.transform.position += new Vector3(1,0,0) * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Belt")
        {
            other.transform.position = origialPosition;
        }
    }
}
