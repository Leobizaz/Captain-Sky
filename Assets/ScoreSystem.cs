using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ScoreSystem : MonoBehaviour
{
    public Text scoreDisplay;
    public static float currentScore;
    float displayedScore;

    void Update()
    {
        displayedScore = currentScore;
        //scoreDisplay.text = displayedScore.ToString();
        //UpdateScore();
    }

    public void UpdateScore()
    {
        scoreDisplay.DOText(displayedScore.ToString(), 1f, true, ScrambleMode.Numerals);
    }
}
