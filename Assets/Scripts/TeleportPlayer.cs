using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TeleportPlayer : MonoBehaviour
{
    public float storedPos;
    bool once;
    public CinemachineDollyCart playerDolly;

    private void OnTriggerEnter(Collider other)
    {
        if (!once)
            if (other.CompareTag("GameController"))
            {
                once = true;
                playerDolly.m_Position = storedPos;
            }
    }
}
