using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public GameObject objeto;

    void Update()
    {
        this.transform.LookAt(objeto.transform.position);
    }
}
