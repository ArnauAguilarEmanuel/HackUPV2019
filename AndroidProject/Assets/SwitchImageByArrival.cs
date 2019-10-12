using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchImageByArrival : MonoBehaviour
{
    public List<Sprite> sprites;
    public List<string> CityName;
    void Start()
    {
        
    }
    public int index = -1;
    private bool seted;
    // Update is called once per frame
    void Update()
    {
        if (_GameController.instance.API.myUserAvaileable&& !seted)
        {
            for (int i = 0; i < CityName.Count; i++) if (_GameController.instance.API.myUser.arrival.Contains(CityName[i])) index = i;
            if (index < 0)
            {
                GetComponent<Image>().sprite = null;
                GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                float originalWidth = GetComponent<RectTransform>().rect.width;
                float multFactor = GameObject.Find("FlightScrollView").GetComponent<RectTransform>().rect.width / originalWidth;
                GetComponent<RectTransform>().sizeDelta = new Vector2( (GameObject.Find("Canvas").GetComponent<RectTransform>().rect.width * 3)/2, GetComponent<RectTransform>().rect.height*multFactor/2 );
                GetComponent<Image>().sprite = sprites[index];
            }
            seted = true;
        }
    }
}
