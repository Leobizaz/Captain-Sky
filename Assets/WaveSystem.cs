using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public static int aveCountAto2;
    public static int robosDestroyed;
    bool started;

    public GameObject Robozão;


    public WaveSpawner spawner1;
    public WaveSpawner spawner2;
    public WaveSpawner spawner3;

    public GameObject spawnpoint1;
    public GameObject spawnpoint2;
    public GameObject spawnpoint3;
    public GameObject spawnpoint4;

    public GameObject objetivo;

    void Start()
    {
        Invoke("StartWaves", 46f);
    }
    void Update()
    {
        if (started)
        {
            if (aveCountAto2 <= 30)
            {
                spawner1.Spawn(1);
                spawner2.Spawn(1);
                spawner3.Spawn(1);
                aveCountAto2 = aveCountAto2 + 3;
            }
        }
    }

    public void Wave1()
    {
        Instantiate(Robozão, spawnpoint2.transform.position, spawnpoint2.transform.rotation);
    }


    public void StartWaves()
    {
        Invoke("Wave1", 30f);
        objetivo.SetActive(true);
        started = true;
    }
}
