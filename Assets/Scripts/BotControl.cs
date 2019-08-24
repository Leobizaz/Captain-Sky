using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BotControl : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    Vector3 vectorinput;
    float rotation;
    float forwardSpeed;
    float yawSpeed;
    float rollSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        Movimento();
    }

    void Movimento()
    {
        vectorinput = new Vector3(horizontalInput, verticalInput, 0);
        rotation = horizontalInput;

        transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);
        Quaternion rot = Quaternion.Euler(new Vector3(0, rotation, 0));
        Quaternion yawRot = Quaternion.Euler(new Vector3(vectorinput.y * yawSpeed, 0, 0));
        Quaternion rollRot = Quaternion.Euler(new Vector3(0, 0, -vectorinput.x * rollSpeed));

        transform.localRotation = transform.localRotation * rot * yawRot * rollRot;
    }
}
