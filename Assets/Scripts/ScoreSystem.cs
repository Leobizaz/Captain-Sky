using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ScoreSystem : MonoBehaviour
{
    public Text scoreHUDisplay;
    public Text EnemyK;
    public Text Tempo;

    public static float time;

    public static float currentScore;
    public static int enemysKill;
    float displayedScore;
    public GameObject TelaVitoria;
    public string Tstring;

    private void Start()
    {
        time = 0;
        enemysKill = 0;
        UpdateScore();
    }

    void Update()
    {
        scoreHUDisplay.text = "" + currentScore;
        //EnemyK.text = "" + enemysKill;
        displayedScore = currentScore;
        //scoreDisplay.text = displayedScore.ToString();
        //UpdateScore();
        if (!TelaVitoria.active)
        {
            time += Time.deltaTime;
        }
        int sec = (int) (time % 60);
        int min = (int) (time / 60) % 60;
        Tstring = string.Format("{0:0}:{1:00}", min, sec);

        //Tempo.text = "" + Tstring;

    }

    public void UpdateScore()
    {
        scoreHUDisplay.DOText(displayedScore.ToString(), 1f, true, ScrambleMode.Numerals);
    }
}
