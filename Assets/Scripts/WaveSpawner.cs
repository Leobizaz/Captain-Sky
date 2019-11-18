using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemy;
    public void Spawn(int quantity)
    {
        for(int i = quantity; i >= 0; i--)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
        }
    }
}
