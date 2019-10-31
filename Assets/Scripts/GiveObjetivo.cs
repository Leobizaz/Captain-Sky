using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveObjetivo : MonoBehaviour
{
    public Ato3_Objetivo1 objetivo;
    public float delay;
    bool once;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !once)
        {
            once = true;
            Invoke("Objetivo", delay);
        }
    }

    void Objetivo()
    {
        objetivo.enabled = true;
    }
}
