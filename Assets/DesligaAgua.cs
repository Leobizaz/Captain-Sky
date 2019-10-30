using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesligaAgua : MonoBehaviour
{
    public GameObject objeto;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objeto.SetActive(false);
        }
    }
}
