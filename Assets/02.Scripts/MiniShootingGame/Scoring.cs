using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoring : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    //public TextMeshProUGUI bestScoreText;

    //public GameObject scoreBoardUI;
    public static int score;

    private void Start()
    {
        score = 0;
        gameObject.GetComponent<Shoot>().enabled = true;
        //scoreBoardUI = GameObject.FindGameObjectWithTag("ScoreCanvas");
        //scoreText = GameObject.FindGameObjectWithTag("ScoreOnBanner").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        scoreText.text = "" + score.ToString();
    }


}
