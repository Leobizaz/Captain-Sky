﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CheckpointSystem : MonoBehaviour
{
    public static string STAGEPHASE = "PHASE0";
    public MusicController music;
    public GameObject Gameplay3;
    public GameObject cenarioMontanha;
    public GameObject cenarioEsfera;
    public TurnOffLight fog;
    public Ato3 ato3;
    public GameObject water;

    public CinemachinePathBase trackEsfera;

    public static float storedTime;
    public static int storedKills;
    public static float storedScore;

    private void Start()
    {
        PlayerHealth.dead = false;
        PlayerMovement.ded = 1;
        PlayerOpenMovement.ded = 1;
      STAGEPHASE = "PHASE1";

        if(STAGEPHASE == "PHASE0") //começo
        {
            cenarioEsfera.SetActive(false);
            cenarioMontanha.SetActive(false);
            Ato3_Objetivo1.torres_restantes = 3;
            Ato3_Objetivo2.geradores_restantes = 4;
            Ato3.ato3_passagem = 0;
            ScoreSystem.currentScore = 0;
            ScoreSystem.enemysKill = 0;
            ScoreSystem.time = 0;
            music.ChangeMusic(0, 0);

        }

        if(STAGEPHASE == "PHASE1") //dentro dos tuneis depois de se separar
        {
            Ato3.ato3_passagem = 0;
            cenarioMontanha.SetActive(false);
            cenarioEsfera.SetActive(false);
            Ato3_Objetivo1.torres_restantes = 3;
            Ato3_Objetivo2.geradores_restantes = 4;
            fog.Trigger();
            ScoreSystem.currentScore = storedScore;
            ScoreSystem.enemysKill = storedKills;
            ScoreSystem.time = storedTime;
            Gameplay3.GetComponentInChildren<UnderwaterEffects>().Enable();
            Gameplay3.GetComponent<CinemachineDollyCart>().m_Position = 11000;
            Gameplay3.GetComponent<CinemachineDollyCart>().m_Speed = 100f;
            music.ChangeMusic(1, 0);
        }

        if(STAGEPHASE == "PHASE2") //hora que sai do tunel
        {
            cenarioEsfera.SetActive(false);
            cenarioMontanha.SetActive(true);
            Ato3_Objetivo1.torres_restantes = 3;
            Ato3_Objetivo2.geradores_restantes = 4;
            RenderSettings.fog = true;
            ato3.startEnemyAI = true;
            water.SetActive(false);
            ScoreSystem.currentScore = storedScore;
            ScoreSystem.enemysKill = storedKills;
            ScoreSystem.time = storedTime;
            Gameplay3.GetComponent<CinemachineDollyCart>().m_Position = 29000;
            Gameplay3.GetComponent<CinemachineDollyCart>().m_Speed = 100f;

            music.ChangeMusic(2, 0);

        }
        if (STAGEPHASE == "PHASE3") //hora que entra na esfera
        {
            cenarioMontanha.SetActive(false);
            RenderSettings.fog = false;
            Ato3_Objetivo1.torres_restantes = 0;
            Ato3_Objetivo2.geradores_restantes = 4;
            ScoreSystem.currentScore = storedScore;
            water.SetActive(false);
            ScoreSystem.enemysKill = storedKills;
            ScoreSystem.time = storedTime;
            Gameplay3.GetComponent<CinemachineDollyCart>().m_Path = trackEsfera;
            Gameplay3.GetComponent<CinemachineDollyCart>().m_Position = 300f;
            Gameplay3.GetComponent<CinemachineDollyCart>().m_Speed = 100f;

            music.ChangeMusic(2, 0);
        }

        if (STAGEPHASE == "PHASE4") //reator e escape
        {
            cenarioMontanha.SetActive(false);
            Ato3_Objetivo1.torres_restantes = 0;
            Ato3_Objetivo2.geradores_restantes = 4;
            RenderSettings.fog = false;
            ScoreSystem.currentScore = storedScore;
            water.SetActive(false);
            ScoreSystem.enemysKill = storedKills;
            ScoreSystem.time = storedTime;
            Gameplay3.GetComponent<CinemachineDollyCart>().m_Path = trackEsfera;
            Gameplay3.GetComponent<CinemachineDollyCart>().m_Position = 6200f;
            Gameplay3.GetComponent<CinemachineDollyCart>().m_Speed = 100f;

            music.ChangeMusic(2, 0);
        }


    }
}