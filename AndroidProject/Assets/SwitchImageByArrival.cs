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
            for (int i = 0; i < CityName.Count; i++) if(_GameController.instance.API.myUser.arrival.Contains(CityName[i])) index = i;
            if (index < 0)
            {
                GetComponent<Image>().sprite = null;
                GetComponent<Image>().color -= new Color(0,0,0,1);
            }
            else
            {
                GetComponent<Image>().color += new Color(0,0,0,0.05f);
                GetComponent<Image>().sprite = sprites[index];
                //GetComponent<Image>().rectTransform.localScale
            }
            seted = true;
        }
    }
}
