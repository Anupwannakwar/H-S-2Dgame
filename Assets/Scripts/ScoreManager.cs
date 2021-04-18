using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    int MainScore = 0;
    public Text scoretext;
    private void Start()
    {
        
    }
    private void Update()
    {
        scoretext.text = "Score : " + MainScore;
    }
    public void updateScore(int score)
    {
        MainScore += score;
    }
}
