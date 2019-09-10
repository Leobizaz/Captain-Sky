using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMission : MonoBehaviour
{
    public GameObject victoryScreen;
    public GameObject music;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pause.victory = true;
            victoryScreen.SetActive(true);
            music.SetActive(false);
        }
    }
}
