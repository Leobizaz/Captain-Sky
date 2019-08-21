using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOpenMovement : MonoBehaviour
{
    Vector3 vectorinput;

    void Start()
    {
        
    }

    void Update()
    {
        vectorinput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        Vector3 vectorot = new Vector3(vectorinput.y, vectorinput.x, -vectorinput.x);

        transform.localRotation = Quaternion.Euler(vectorot * 2);

    }



}
