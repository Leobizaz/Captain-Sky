using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLaser : MonoBehaviour
{
    public GameObject laserEmitter;

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
            if(laserEmitter.gameObject != null)
                laserEmitter.SetActive(true);
        }
    }
}
