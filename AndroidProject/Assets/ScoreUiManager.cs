using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUiManager : MonoBehaviour
{
    private float currentScore = 0;
    private float originalSize;
    public float decreasingSpeed;

    public void add(float score)
    {
        GetComponent<TextMeshProUGUI>().fontSize = originalSize * 2f;
        currentScore += score;
    }
    // Start is called before the first frame update
    void Start()
    {
        originalSize = GetComponent<TextMeshProUGUI>().fontSize;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = currentScore.ToString();

        if (GetComponent<TextMeshProUGUI>().fontSize > originalSize)
        {
            GetComponent<TextMeshProUGUI>().fontSize -= Time.deltaTime * decreasingSpeed;
        }

        if (GetComponent<TextMeshProUGUI>().fontSize < originalSize) GetComponent<TextMeshProUGUI>().fontSize = originalSize;
    }
}
