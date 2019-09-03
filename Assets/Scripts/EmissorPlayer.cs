using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissorPlayer : MonoBehaviour
{
    public GameObject mira;
    public GameObject gira;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.LookAt(mira.transform);
        if (Input.GetButton("Right") || Input.GetButton("Right"))
            gira.transform.localEulerAngles = new Vector3(0, 0, -90);
        else
        if (Input.GetButton("Left") || Input.GetButton("Left2"))
            gira.transform.localEulerAngles = new Vector3(0, 0, 90);
        else
            gira.transform.localEulerAngles = new Vector3(0, 0, 0);
    }

}
