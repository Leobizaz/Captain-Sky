using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnIA : MonoBehaviour
{
    public Ato3 ato3;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            ato3.startEnemyAI = true;
        }
    }
}
