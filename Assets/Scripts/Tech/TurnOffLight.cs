using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffLight : MonoBehaviour
{
    public bool on;
    public bool turnoff;
    bool once;
    Color oldcolor;
    public Color color;
    void Start()
    {
        oldcolor = RenderSettings.fogColor;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!once)
        {
           
            if (other.CompareTag("Player"))
            {
                once = true;
                Trigger();
            }
        }
    }
    public void Trigger()
    {
        if (!on)
        {
            //RenderSettings.fog = false;
            RenderSettings.fogColor = new Color(0, 0, 0, 0);
            RenderSettings.fogStartDistance = 100;
            RenderSettings.fogEndDistance = 600;
            Debug.Log("Darkness");
        }
        else
        {
            //RenderSettings.fog = false;
            RenderSettings.fog = true;
            RenderSettings.fogColor = color;
            RenderSettings.fogStartDistance = 1000;
            RenderSettings.fogEndDistance = 2800;
            //RenderSettings.fog = true;

        }
        if (turnoff)
        {
            RenderSettings.fog = false;
        }
    }
}
