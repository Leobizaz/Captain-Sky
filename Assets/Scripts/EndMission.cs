using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMission : MonoBehaviour
{
    public GameObject victoryScreen;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pause.victory = true;
            victoryScreen.SetActive(true);
        }
    }
}
