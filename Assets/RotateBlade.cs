using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBlade : MonoBehaviour
{
    public float speed;
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, speed));
    }
}
