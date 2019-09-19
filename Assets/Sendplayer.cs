using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Sendplayer : MonoBehaviour
{
    public float ammount;
    bool once;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("GameController"))
        {
            if (!once)
            {
                once = true;

                other.GetComponent<CinemachineDollyCart>().m_Position = other.GetComponent<CinemachineDollyCart>().m_Position - ammount;
                Invoke("Reset", 2f);
            }
        }
    }

    private void Reset()
    {
        once = false;
    }
}
