using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDebris : MonoBehaviour
{
    bool falling;
    public float speed;
    void Update()
    {
        if(falling)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            falling = true;
        }
    }

}
