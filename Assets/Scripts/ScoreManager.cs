using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    int MainScore = 0;
    public Text scoretext;

    public GameObject backGround;
    public bool value = true;
    public int x = 1000;

    private void Start()
    {
        
    }
    private void Update()
    {
        scoretext.text = "Score : " + MainScore;

        if(MainScore > x)
        {
            backGround.SetActive(value);
            value = !value;
            x = x + 1000;
        }
    }
    public void updateScore(int score)
    {
        MainScore += score;
    }
}
