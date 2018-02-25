using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public Text scoreText;
    public Text hiScoreText;

    public float scoreCount;
    public float hiScoreCount;

    public float pointsPerSecond;

    public bool scoreIncreasing;

    private HighScore theScoreManager;
    

	// Use this for initialization
	void Start () {

        theScoreManager = FindObjectOfType<HighScore>();

	}
	
	// Update is called once per frame
	void Update () {

        if(scoreIncreasing)
        {
            scoreCount += pointsPerSecond * Time.deltaTime;
        }
        
        if(scoreCount > hiScoreCount)
        {
            hiScoreCount = scoreCount;
            PlayerPrefs.SetFloat("HighScore", hiScoreCount);
        }
        
        scoreText.text = "Score: " + Mathf.Round (scoreCount);
        hiScoreText.text = "High Score: " + Mathf.Round (hiScoreCount);

	}

    public void AddScore(int pointsToAdd)
    {
        scoreCount += pointsToAdd;
    }


}
