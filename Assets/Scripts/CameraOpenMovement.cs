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
        float x = Input.GetAxis("Horizontal");
        float y = -Input.GetAxis("Vertical");

        Vector3 vectorot = new Vector3(vectorinput.y, vectorinput.x, -vectorinput.x);

        transform.localRotation = Quaternion.Euler(vectorot * 2);

    }



}
