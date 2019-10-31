using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMinas : MonoBehaviour
{
    public GameObject minas;

    void Update()
    {
        if (Ato3.ato3_passagem == 1)
        {
            minas.SetActive(true);
        }
        else
        {
            minas.SetActive(false);
        }
    }
}
