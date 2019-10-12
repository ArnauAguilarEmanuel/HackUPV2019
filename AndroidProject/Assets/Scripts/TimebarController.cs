using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimebarController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Gradient color;

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Image>().color = color.Evaluate(GameObject.Find("SuitcaseSpawner").GetComponent<SuitcaseSpawner>().globalTimer.Remap(0, _GameController.instance.gameDuration, 1, 0));
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(GameObject.Find("SuitcaseSpawner").GetComponent<SuitcaseSpawner>().globalTimer.Remap(0, _GameController.instance.gameDuration, Screen.width, 0), gameObject.GetComponent<RectTransform>().sizeDelta.y);
    }
}
