using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ScoreSystem : MonoBehaviour
{
    public Text scoreHUDisplay;

    public static float currentScore;
    float displayedScore;
    private void Start()
    {
        UpdateScore();
    }

    void Update()
    {
        displayedScore = currentScore;
        //scoreDisplay.text = displayedScore.ToString();
        //UpdateScore();
    }

    public void UpdateScore()
    {
        scoreHUDisplay.DOText(displayedScore.ToString(), 1f, true, ScrambleMode.Numerals);
    }
}
