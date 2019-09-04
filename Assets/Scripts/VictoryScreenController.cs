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
    public Text BonusDisplay;
    public GameObject obrigado;
    public AudioSource aud;

    void Start()
    {
        aud.Play();
        missaoconcluida.DOText("Missão Concluída", 3.5f, true, ScrambleMode.Uppercase);
        Invoke("UpdateScore", 6f);
        Invoke("UpdateInimigos", 8f);
        Invoke("UpdateTempo", 10f);
        Invoke("UpdateBonus", 13f);
        Invoke("UpdateScoreFinal", 15f);
        Invoke("Obrigado", 18f);
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
        inimigosDisplay.DOText("13", 1.5f, true, ScrambleMode.Numerals);
    }

    void UpdateTempo()
    {
        tempoDisplay.DOText("1:37", 1.5f, true, ScrambleMode.Numerals);
    }
    void UpdateBonus()
    {
        BonusDisplay.DOText("MAMA AQUI", 1.5f, true, ScrambleMode.Numerals);
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
