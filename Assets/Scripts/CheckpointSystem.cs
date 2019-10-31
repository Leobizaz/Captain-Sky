using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CheckpointSystem : MonoBehaviour
{
    public static string STAGEPHASE;

    public GameObject Gameplay3;
    public TurnOffLight fog;

    public static float storedTime;
    public static int storedKills;
    public static float storedScore;

    private void Start()
    {
        STAGEPHASE = "PHASE2";

        if(STAGEPHASE == "PHASE0") //começo
        {
            Ato3_Objetivo1.torres_restantes = 3;

            ScoreSystem.currentScore = 0;
            ScoreSystem.enemysKill = 0;
            ScoreSystem.time = 0;

        }

        if(STAGEPHASE == "PHASE1") //dentro dos tuneis depois de se separar
        {
            Ato3_Objetivo1.torres_restantes = 3;
            fog.Trigger();
            ScoreSystem.currentScore = storedScore;
            ScoreSystem.enemysKill = storedKills;
            ScoreSystem.time = storedTime;
            Gameplay3.GetComponentInChildren<UnderwaterEffects>().Enable();
            Gameplay3.GetComponent<CinemachineDollyCart>().m_Position = 11000;
        }

        if(STAGEPHASE == "PHASE2") //hora que sai do tunel
        {
            Ato3_Objetivo1.torres_restantes = 3;
            ScoreSystem.currentScore = storedScore;
            ScoreSystem.enemysKill = storedKills;
            ScoreSystem.time = storedTime;
            Gameplay3.GetComponent<CinemachineDollyCart>().m_Position = 29500;

        }


    }
}
