using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class VictoryScreenController : MonoBehaviour
{
    public Text missaoconcluida;
    public Text scoreDisplay;
    public Text inimigosDisplay;
    public Text tempoDisplay;
    public Text scoreFinalDisplay;
    public GameObject obrigado;


    void Start()
    {
        missaoconcluida.DOText("Missão Concluída", 2f, true, ScrambleMode.Uppercase);
        Invoke("UpdateScore", 4f);
        Invoke("UpdateInimigos", 6f);
        Invoke("UpdateTempo", 8f);
        Invoke("UpdateScoreFinal", 10f);
        Invoke("Obrigado", 14f);
    }

    void Update()
    {
        
    }

    void UpdateScore()
    {
        scoreDisplay.DOText(ScoreSystem.currentScore.ToString(), 1.5f, true, ScrambleMode.Numerals);
    }

    void UpdateInimigos()
    {
        inimigosDisplay.DOText("5/13", 1.5f, true, ScrambleMode.Numerals);
    }

    void UpdateTempo()
    {
        tempoDisplay.DOText("1m37s", 1.5f, true, ScrambleMode.Numerals);
    }

    void UpdateScoreFinal()
    {
        scoreFinalDisplay.DOText((ScoreSystem.currentScore + 3000 + 10000).ToString(), 3f, true, ScrambleMode.Numerals);
    }
    void Obrigado()
    {
        obrigado.SetActive(true);
    }
}
