using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAllies : MonoBehaviour
{
    public GameObject allies;
    public bool on;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            allies.SetActive(on);
        }
    }
}
