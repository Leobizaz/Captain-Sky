using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffLight : MonoBehaviour
{
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
            RenderSettings.fogColor = new Color(0, 0, 0, 0);
            RenderSettings.fogStartDistance = 100;
            RenderSettings.fogEndDistance = 600;
        }
    }
}
