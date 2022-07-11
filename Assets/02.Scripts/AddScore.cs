using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class AddScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    int playerScore = 0;

    Explode explode;

    void Start()
    {
        scoreText.text = "" + playerScore;
    }

    void Update()
    {
        playerScore += 10;
        scoreText.text = "" + playerScore;

    }
}
