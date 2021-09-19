using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public static int coinsValue = 0;
    public static float distanceValue = 0;
    public Text coinsText;
    public Text distanceText;
    void Start()
    {
        coinsText.GetComponent<Text>();
        distanceText.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = "Coins: " + coinsValue.ToString("0");
        distanceText.text = "Score: " + distanceValue.ToString("0") + "m";
    }

    //public void UpdateCoins(int coins)
    //{
    //    scoreText.text = "Coins: " + scoreValue.ToString("0");
    //}

    //public void UpdateDistance(float distance)
    //{
    //    distanceText.text = "Score: " + distanceValue.ToString("0");
    //}
}
