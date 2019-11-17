using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public static int aveCountAto2;
    public static float missaoRadio;
    public static int robosDestroyed;
    bool started;

    public SignalMission missao;

    public GameObject Robozão;
    public GameObject robozãoRadio;
    bool missaoComplete;

    public InstantiateDialogo marcusDica;
    public InstantiateDialogo marcusBoa;
    public InstantiateDialogo willburneQuasela;

    public DialogoSequence wave1_dialogos;
    public DialogoSequence wave2_dialogos;
    public DialogoSequence waveRadio_dialogos;
    public DialogoSequence ggRadio_dialogos;

    public float test;
    bool once1;
    bool once2;
    bool once3;
    bool roboSpawned;


    public WaveSpawner spawner1;
    public WaveSpawner spawner2;
    public WaveSpawner spawner3;

    public GameObject spawnpoint1;
    public GameObject spawnpoint2;
    public GameObject spawnpoint3;
    public GameObject spawnpoint4;

    public GameObject objetivo;
    public GameObject objetivo2;

    void Start()
    {
        aveCountAto2 = 0;
        missaoRadio = 0;
        robosDestroyed = 0;

        Invoke("StartWaves", 46f);
    }
    void Update()
    {
        test = missaoRadio;

        if (started)
        {
            if (aveCountAto2 <= 20)
            {
                spawner1.Spawn(1);
                spawner2.Spawn(1);
                spawner3.Spawn(1);
                aveCountAto2 = aveCountAto2 + 3;
            }
        }

        if(robosDestroyed == 1 && !once1)
        {
            if(!DialogoSequence.isPlayingSequence)
                marcusBoa.PlayDialogo();
            once1 = true;
            StartCoroutine(Wave2());
        }

        if(robosDestroyed == 3 && !once2)
        {
            once2 = true;
            StartCoroutine(WaveRadio());
        }

        if(missaoRadio >= 30 && !once3)
        {
            once3 = true;
            willburneQuasela.PlayDialogo();

        }


        if (missaoRadio >= 59 && !missaoComplete)
        {
            objetivo2.GetComponent<Animator>().Play("objetivoGone");
            missaoComplete = true;
            missao.Deactivate();
            missao.enabled = false;
            ggRadio_dialogos.PlayDialogo();
        }


    }

    public void Wave1()
    {
        StartCoroutine(MarcusDica());
        roboSpawned = true;
        wave1_dialogos.PlayDialogo();
        Instantiate(Robozão, spawnpoint2.transform.position, spawnpoint2.transform.rotation);
    }

    public IEnumerator Wave2()
    {
        yield return new WaitForSeconds(14f);
        wave2_dialogos.PlayDialogo();
        Instantiate(Robozão, spawnpoint1.transform.position, spawnpoint1.transform.rotation);
        yield return new WaitForSeconds(2);
        Instantiate(Robozão, spawnpoint3.transform.position, spawnpoint3.transform.rotation);
    }

    public IEnumerator WaveRadio()
    {
        objetivo.GetComponent<Animator>().Play("objetivoGone");
        yield return new WaitForSeconds(4f);
        objetivo.SetActive(false);
        waveRadio_dialogos.PlayDialogo();
        yield return new WaitForSeconds(14f);
        objetivo2.SetActive(true);
        robozãoRadio.SetActive(true);

    }

    public IEnumerator MarcusDica()
    {
        while (robosDestroyed < 1)
        {
            yield return new WaitForSeconds(80f);
            if(robosDestroyed < 1)
                marcusDica.PlayDialogo();

        }

    }


    public void StartWaves()
    {
        Invoke("Wave1", 40f);
        objetivo.SetActive(true);
        started = true;
    }
}
