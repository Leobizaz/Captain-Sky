using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApagaMontanhas : MonoBehaviour
{
    public GameObject montanhas;
    public bool on;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            montanhas.SetActive(on);
        }
    }
}
