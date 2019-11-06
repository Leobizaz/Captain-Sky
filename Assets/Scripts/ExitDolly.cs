using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ExitDolly : MonoBehaviour
{
    public CinemachinePathBase dollyTrack;
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<PlayerOpenMovement>().enabled = true;
            player.GetComponentInChildren<PlayerCollision>().enabled = true;
            player.GetComponentInChildren<RaycastMira>().enabled = false;
            player.GetComponent<CinemachineDollyCart>().enabled = false;
            player.GetComponent<CinemachineDollyCart>().m_Path = null;
            player.GetComponentInChildren<PlayerMovement>().enabled = false;
            player.GetComponentInChildren<PlayerMovement>().playerActive = false;
        }
    }
}
