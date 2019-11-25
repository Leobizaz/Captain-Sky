using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endgame : MonoBehaviour
{
    public Ato3 ato3;
    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Player")
        {
            ato3.startEnemyAI = false;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject enemy in enemies)
            {
                if(enemy.name == "AveIA")
                {
                    Destroy(enemy);
                }
            }
        }
    }

}
