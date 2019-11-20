using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

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
            //player.GetComponentInChildren<RaycastMira>().enabled = false;
            player.GetComponentInChildren<RaycastMira>().gameObject.transform.DOLocalMove(new Vector3(0, 0, 9), 3f);
            player.GetComponent<CinemachineDollyCart>().enabled = false;
            player.GetComponent<CinemachineDollyCart>().m_Path = null;
            player.GetComponentInChildren<PlayerMovement>().enabled = false;
            player.GetComponentInChildren<PlayerMovement>().playerActive = false;
            player.GetComponentInChildren<PlayerCollision>().gameObject.transform.DOLocalMove(Vector3.zero, 8f);
        }
    }
}
