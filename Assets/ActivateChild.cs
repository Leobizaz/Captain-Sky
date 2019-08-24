using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateChild : MonoBehaviour
{
    public GameObject child;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            child.SetActive(true);
        }
    }

}
