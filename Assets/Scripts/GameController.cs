using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int score { get; set; }
    float elapsedTime;
    public int UpperBound = 1000;
    public int timeDiff = 0;
    public int additionPoints = 0;

    void Start()
    {
        Time.timeScale = 1.0f;
        score = 0;
    }
    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        setScore();
    }
    public void setScore()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        score = additionPoints + UpperBound - (minutes * 60 + seconds) * 5;
        //Debug.Log("score: " + score);
        //Debug.Log(score);
    }
    public void AddScore(int points)
    {
        Debug.Log("Current Score " + score+"adding points " + points.ToString() + "new score" + (score +points).ToString());
        additionPoints += points;

    }
    
}
