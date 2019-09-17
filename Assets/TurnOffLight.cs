using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffLight : MonoBehaviour
{
    public bool on;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!on)
            {
                RenderSettings.fogColor = new Color(0, 0, 0, 0);
                RenderSettings.fogStartDistance = 100;
                RenderSettings.fogEndDistance = 600;
            }
            else
            {
                RenderSettings.fogColor = new Color(255, 255, 255, 255);
                RenderSettings.fogStartDistance = 1000;
                RenderSettings.fogEndDistance = 2800;
            }
        }
    }
}
