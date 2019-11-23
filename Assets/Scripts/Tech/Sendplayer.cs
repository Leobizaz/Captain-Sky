using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Sendplayer : MonoBehaviour
{
    public static bool bossFightended;
    public float storedPos;
    bool once;
    public CinemachineDollyCart playerDolly;
    bool once2;

    public GameObject boss;

    private void Start()
    {
        bossFightended = false;
        Ato3.ato3_passagem = 2;
    }

    private void Update()
    {
        if (!once2 && Ato3.ato3_passagem > 1)
        {
            once2 = true;
            boss.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!once)
        if (other.CompareTag("GameController"))
        {
            once = true;
            storedPos = other.GetComponent<CinemachineDollyCart>().m_Position;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("GameController") && !bossFightended)
        {
            playerDolly.m_Position = storedPos;
            Ato3.ato3_passagem++;
        }
    }
}
