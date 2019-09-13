using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ato3 : MonoBehaviour
{
    public bool startWithFog;

    void Start()
    {
        if (startWithFog)
        {
            RenderSettings.fog = true;
        }
            
    }

    void Update()
    {
        
    }
}
