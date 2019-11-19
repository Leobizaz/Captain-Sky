using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public static float probabilidade_IA;
    public static int aveCountAto2;
    public static float missaoRadio;
    public static int buildingsDestroyed;
    public static int robosDestroyed;
    bool started;

    public SignalMission missao;
    public PlayerHealth playerHealth;

    public GameObject Robozão;
    public GameObject victoryScreen;
    public GameObject robozãoRadio;
    bool missaoComplete;

    public InstantiateDialogo marcusDica;
    public InstantiateDialogo marcusBoa;
    public InstantiateDialogo willburneQuasela;

    public DialogoSequence wave1_dialogos;
    public DialogoSequence wave2_dialogos;
    public DialogoSequence waveRadio_dialogos;
    public DialogoSequence ggRadio_dialogos;
    public DialogoSequence dialogoWaveFinal;
    public DialogoSequence dialogoCabo;
    public DialogoSequence deuRuimRadio;
   

    public InstantiateDialogo destruindo_base1;
    public InstantiateDialogo destruindo_base2;
    public DialogoSequence destruindo_base3;
    public DialogoSequence destruiram_base;

    public float test;
    bool once1;
    bool once2;
    bool once3;
    bool waveFinal;
    bool cabo;
    bool roboSpawned;

    bool dialogo1;
    bool dialogo2;
    bool dialogo3;


    public WaveSpawner spawner1;
    public WaveSpawner spawner2;
    public WaveSpawner spawner3;

    public GameObject spawnpoint1;
    public GameObject spawnpoint2;
    public GameObject spawnpoint3;
    public GameObject spawnpoint4;

    bool deuRUIM;
    public GameObject objetivo;
    public GameObject objetivo2;

    void Start()
    {
        buildingsDestroyed = 0;
        ControleSelect.W2 = true;
        PlayerPrefs.SetInt("Level", 2);
        probabilidade_IA = 0;


        aveCountAto2 = 0;
        missaoRadio = 0;
        robosDestroyed = 0;

        Invoke("StartWaves", 46f);
    }
    void Update()
    {
        test = missaoRadio;

        probabilidade_IA = ((ScoreSystem.enemysKill * 5)/100) / 2 + ScoreSystem.playerdeaths;

        if(buildingsDestroyed >= 7 && !deuRUIM)
        {
            deuRUIM = true;
            StartCoroutine(BaseDestruida());
        }

        if(buildingsDestroyed == 1 && !dialogo1)
        {
            dialogo1 = true;
            destruindo_base1.PlayDialogo();
        }

        if(buildingsDestroyed == 2 && !dialogo2)
        {
            dialogo2 = true;
            destruindo_base2.PlayDialogo();
        }

        if(buildingsDestroyed == 5 && !dialogo3)
        {
            dialogo3 = true;
            destruindo_base3.PlayDialogo();
        }



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

        if(missaoRadio >= 15 && !once3)
        {
            once3 = true;
            willburneQuasela.PlayDialogo();

        }

        if(robosDestroyed == 4 && !missaoComplete && !deuRUIM)
        {
            deuRUIM = true;
            StartCoroutine(DeuRuim());
        }


        if (missaoRadio >= 29 && !missaoComplete)
        {
            objetivo2.GetComponent<Animator>().Play("objetivoGone");
            missaoComplete = true;
            missao.Deactivate();
            missao.enabled = false;
            ggRadio_dialogos.PlayDialogo();
        }
        if(robosDestroyed == 4 && missaoComplete && !waveFinal)
        {
            waveFinal = true;
            StartCoroutine(WaveFinal());
        }
        if(robosDestroyed == 8 && missaoComplete && !cabo)
        {
            cabo = true;
            StartCoroutine(Cabo());
        }


    }

    public IEnumerator DeuRuim()
    {
        deuRuimRadio.PlayDialogo();
        yield return new WaitForSeconds(8f);
        playerHealth.Death();

    }
    public IEnumerator BaseDestruida()
    {
        destruiram_base.PlayDialogo();
        yield return new WaitForSeconds(10f);
        playerHealth.Death();
    }

    public void Wave1()
    {
        objetivo.GetComponent<Animator>().Play("objetivoGone");
        StartCoroutine(MarcusDica());
        roboSpawned = true;
        wave1_dialogos.PlayDialogo();
        Instantiate(Robozão, spawnpoint2.transform.position, spawnpoint2.transform.rotation);
    }

    public IEnumerator Cabo()
    {
        dialogoCabo.PlayDialogo();
        yield return new WaitForSeconds(18f);
        victoryScreen.SetActive(true);
    }

    public IEnumerator WaveFinal()
    {
        yield return new WaitForSeconds(6);
        dialogoWaveFinal.PlayDialogo();
        yield return new WaitForSeconds(6);
        Instantiate(Robozão, spawnpoint1.transform.position, spawnpoint1.transform.rotation);
        yield return new WaitForSeconds(0.4f);
        Instantiate(Robozão, spawnpoint2.transform.position, spawnpoint2.transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(Robozão, spawnpoint3.transform.position, spawnpoint3.transform.rotation);
        yield return new WaitForSeconds(0.8f);
        Instantiate(Robozão, spawnpoint4.transform.position, spawnpoint4.transform.rotation);
        yield return new WaitForSeconds(1);
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
