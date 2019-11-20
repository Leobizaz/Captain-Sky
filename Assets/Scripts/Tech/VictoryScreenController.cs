using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class VictoryScreenController : MonoBehaviour
{
    //public Text missaoconcluida;
    //public Text scoreDisplay;
    public PlayerMovement playerMovement;
    public PlayerOpenMovement playerMovementAberto;
    public Text inimigosDisplay;
    public Text tempoDisplay;
    public Text scoreFinalDisplay;
    //public Text BonusDisplay;
    public GameObject obrigado;
    public AudioSource aud;
    public int indexToLoad;
    MusicController music;

    public ScoreSystem scoreSys;

    void Start()
    {
        music = GameObject.Find("Music").GetComponent<MusicController>();
        playerMovement.playerActive = false;
        playerMovementAberto.playerActive = false;
        aud.Play();
        music.FadeOut();
        //missaoconcluida.DOText("Missão Concluída", 3.5f, true, ScrambleMode.Uppercase);
        //Invoke("UpdateScore", 6f);
        Invoke("UpdateInimigos", 8f);
        Invoke("UpdateTempo", 10f);
        //Invoke("UpdateBonus", 13f);
        Invoke("UpdateScoreFinal", 15f);
        Invoke("Obrigado", 26f);
    }

    void Update()
    {
        
    }

    void UpdateScore()
    {
        
        //scoreDisplay.DOText(ScoreSystem.currentScore.ToString(), 1.5f, true, ScrambleMode.Numerals);
    }

    void UpdateInimigos()
    {
        inimigosDisplay.DOText(ScoreSystem.enemysKill.ToString(), 1.5f, true, ScrambleMode.Numerals);
    }

    void UpdateTempo()
    {
        tempoDisplay.DOText(scoreSys.Tstring, 1.5f, true, ScrambleMode.Numerals);
    }
    void UpdateBonus()
    {
        //BonusDisplay.DOText("test", 1.5f, true, ScrambleMode.Numerals);
    }

    void UpdateScoreFinal()
    {
        scoreFinalDisplay.DOText((ScoreSystem.currentScore).ToString(), 3f, true, ScrambleMode.Numerals);
    }
    void Obrigado()
    {

        //obrigado.SetActive(true);
        Loading.levelIndex = indexToLoad;
        SceneManager.LoadScene(5);
    }
}
