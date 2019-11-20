using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnterDolly : MonoBehaviour
{

    public CinemachinePathBase dollyTrack;
    public GameObject player;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<PlayerOpenMovement>().enabled = false;
            player.GetComponent<CinemachineDollyCart>().enabled = true;
            player.GetComponentInChildren<RaycastMira>().enabled = true;
            player.GetComponent<CinemachineDollyCart>().m_Path = dollyTrack;
            player.GetComponent<CinemachineDollyCart>().m_Position = 0;
            player.GetComponentInChildren<PlayerMovement>().enabled = true;
            player.GetComponentInChildren<PlayerCollision>().enabled = false;
            player.GetComponentInChildren<PlayerMovement>().playerActive = true;
        }
    }
}
