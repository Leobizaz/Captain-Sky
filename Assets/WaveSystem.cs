using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public WaveSpawner spawner1;
    public WaveSpawner spawner2;
    public WaveSpawner spawner3;

    void Start()
    {
        spawner1.Spawn(3);
        spawner2.Spawn(5);
        spawner3.Spawn(3);
    }
    void Update()
    {
        
    }
}
