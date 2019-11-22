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
        if (other.CompareTag("GameController") && Ato3.ato3_passagem < 1 && !bossFightended)
        {
            playerDolly.m_Position = storedPos;
            Ato3.ato3_passagem++;
        }
    }
}
