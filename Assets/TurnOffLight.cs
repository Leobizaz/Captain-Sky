using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffLight : MonoBehaviour
{
    public bool on;
    bool once;

    private void OnTriggerEnter(Collider other)
    {
        if (!once)
        {
           
            if (other.CompareTag("Player"))
            {
                once = true;
                if (!on)
                {
                    //RenderSettings.fog = false;
                    RenderSettings.fogColor = new Color(0, 0, 0, 0);
                    RenderSettings.fogStartDistance = 100;
                    RenderSettings.fogEndDistance = 600;
                }
                else
                {
                    RenderSettings.fog = false;
                    RenderSettings.fogColor = new Color(255, 255, 255, 255);
                    RenderSettings.fogStartDistance = 1000;
                    RenderSettings.fogEndDistance = 2800;
                    RenderSettings.fog = true;

                }
            }
        }
    }
}
