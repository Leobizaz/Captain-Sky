using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEndless : MonoBehaviour
{
    public static float probabilidade_IA;
    public static int aveCountAto2;
    public static float missaoRadio;
    public static int buildingsDestroyed;
    bool started;

    public SignalMission missao;
    public PlayerHealth playerHealth;

    public GameObject Robozão;
    public GameObject victoryScreen;
    public GameObject robozãoRadio;
    bool missaoComplete;
   

    public InstantiateDialogo destruindo_base1;
    public InstantiateDialogo destruindo_base2;
    public DialogoSequence destruindo_base3;
    public DialogoSequence destruiram_base;

    bool once1;
    bool once2;
    bool once3;
    bool once4;
    bool once5;
    bool once6;
    bool once7;
    bool once8;
    bool once9;
    bool waveFinal;
    bool cabo;
    bool roboSpawned;


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
        WaveSystem.robosDestroyed = 0;

        Invoke("StartWaves", 46f);
    }
    void Update()
    {

        probabilidade_IA = ((ScoreSystem.enemysKill * 5)/100) / 2 + ScoreSystem.playerdeaths;

        if(buildingsDestroyed >= 7 && !deuRUIM)
        {
            deuRUIM = true;
            StartCoroutine(BaseDestruida());
        }

        if(buildingsDestroyed == 1)
        {
       
            destruindo_base1.PlayDialogo();
        }

        if(buildingsDestroyed == 2)
        {
        
            destruindo_base2.PlayDialogo();
        }

        if(buildingsDestroyed == 5)
        {
            destruindo_base3.PlayDialogo();
        }



        if (started)
        {
            if (aveCountAto2 <= 45)
            {
                spawner1.Spawn(1);
                spawner2.Spawn(1);
                spawner3.Spawn(1);
                aveCountAto2 = aveCountAto2 + 3;
            }
        }

        if(WaveSystem.robosDestroyed == 1 && !once1)
        {
            once1 = true;
            StartCoroutine(Wave2());
        }

        if(WaveSystem.robosDestroyed == 3 && !once2)
        {
            once2 = true;
            StartCoroutine(Wave3());
        }

        if(WaveSystem.robosDestroyed == 7 && !once3)
        {
            once3 = true;
            StartCoroutine(Wave4());
        }

        if(WaveSystem.robosDestroyed == 10 && !once4)
        {
            once4 = true;
            StartCoroutine(Wave5());
        }

        if(WaveSystem.robosDestroyed == 16 && !once5)
        {
            once5 = true;
            StartCoroutine(Wave6());
        }
        if(WaveSystem.robosDestroyed == 18 && !once6)
        {
            once6 = true;
            StartCoroutine(Wave7());
        }

        if(WaveSystem.robosDestroyed == 23 && !once7)
        {
            once7 = true;
            StartCoroutine(Wave8());
        }
        if(WaveSystem.robosDestroyed == 29 && !once8)
        {
            once8 = true;
            StartCoroutine(Wave9());
        }

       if(WaveSystem.robosDestroyed == 32 && !once9)
        {
            once9 = true;
            StartCoroutine(Wave10());
        }

        if (missaoRadio >= 29 && !missaoComplete)
        {
            objetivo2.GetComponent<Animator>().Play("objetivoGone");
            missaoComplete = true;
            missao.Deactivate();
            missao.enabled = false;
        }
        if(WaveSystem.robosDestroyed == 4 && missaoComplete && !waveFinal)
        {
            waveFinal = true;
            StartCoroutine(WaveFinal());
        }
        if(WaveSystem.robosDestroyed == 8 && missaoComplete && !cabo)
        {
            cabo = true;
            StartCoroutine(Cabo());
        }


    }

    public IEnumerator DeuRuim()
    {
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
        roboSpawned = true;
        Instantiate(Robozão, spawnpoint2.transform.position, spawnpoint2.transform.rotation);
    }

    public IEnumerator Cabo()
    {
        yield return new WaitForSeconds(18f);
        victoryScreen.SetActive(true);
    }

    public IEnumerator WaveFinal()
    {
        yield return new WaitForSeconds(6);
        Instantiate(Robozão, spawnpoint1.transform.position, spawnpoint1.transform.rotation);
        yield return new WaitForSeconds(0.4f);
        Instantiate(Robozão, spawnpoint2.transform.position, spawnpoint2.transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(Robozão, spawnpoint3.transform.position, spawnpoint3.transform.rotation);
        yield return new WaitForSeconds(0.8f);
        Instantiate(Robozão, spawnpoint4.transform.position, spawnpoint4.transform.rotation);
        yield return new WaitForSeconds(1);
        Wave10();
    }

    public IEnumerator Wave2()
    {
        yield return new WaitForSeconds(10f);
        Instantiate(Robozão, spawnpoint1.transform.position, spawnpoint1.transform.rotation);
        yield return new WaitForSeconds(2);
        Instantiate(Robozão, spawnpoint3.transform.position, spawnpoint3.transform.rotation);
    }

    public IEnumerator WaveRadio()
    {
        objetivo.GetComponent<Animator>().Play("objetivoGone");
        yield return new WaitForSeconds(4f);
        objetivo.SetActive(false);
        yield return new WaitForSeconds(14f);
        objetivo2.SetActive(true);
        robozãoRadio.SetActive(true);

    }


    public void StartWaves()
    {
        Invoke("Wave1", 40f);
        objetivo.SetActive(true);
        started = true;
    }

        public IEnumerator Wave3()
    {
        yield return new WaitForSeconds(14f);
        Instantiate(Robozão, spawnpoint1.transform.position, spawnpoint1.transform.rotation);
        yield return new WaitForSeconds(2);
        Instantiate(Robozão, spawnpoint2.transform.position, spawnpoint3.transform.rotation);
                yield return new WaitForSeconds(2);
        Instantiate(Robozão, spawnpoint3.transform.position, spawnpoint3.transform.rotation);
                yield return new WaitForSeconds(2);
        Instantiate(Robozão, spawnpoint4.transform.position, spawnpoint3.transform.rotation);
    }

            public IEnumerator Wave4()
    {
        yield return new WaitForSeconds(14f);
        Instantiate(Robozão, spawnpoint1.transform.position, spawnpoint1.transform.rotation);
        yield return new WaitForSeconds(2);
        Instantiate(Robozão, spawnpoint2.transform.position, spawnpoint3.transform.rotation);
                yield return new WaitForSeconds(2);
        Instantiate(Robozão, spawnpoint3.transform.position, spawnpoint3.transform.rotation);
    }
            public IEnumerator Wave5()
    {
        yield return new WaitForSeconds(14f);
        Instantiate(Robozão, spawnpoint2.transform.position, spawnpoint1.transform.rotation);
        yield return new WaitForSeconds(2);
        Instantiate(Robozão, spawnpoint3.transform.position, spawnpoint3.transform.rotation);
                yield return new WaitForSeconds(2);
        Instantiate(Robozão, spawnpoint1.transform.position, spawnpoint3.transform.rotation);
                yield return new WaitForSeconds(2);
        Instantiate(Robozão, spawnpoint4.transform.position, spawnpoint3.transform.rotation);
                        yield return new WaitForSeconds(4);
        Instantiate(Robozão, spawnpoint3.transform.position, spawnpoint3.transform.rotation);
                yield return new WaitForSeconds(4);
        Instantiate(Robozão, spawnpoint3.transform.position, spawnpoint3.transform.rotation);
    }
                public IEnumerator Wave6()
    {
        yield return new WaitForSeconds(14f);
        Instantiate(Robozão, spawnpoint1.transform.position, spawnpoint1.transform.rotation);
        yield return new WaitForSeconds(2);
        Instantiate(Robozão, spawnpoint4.transform.position, spawnpoint3.transform.rotation);

    }
                public IEnumerator Wave7()
    {
        yield return new WaitForSeconds(14f);
        Instantiate(Robozão, spawnpoint1.transform.position, spawnpoint1.transform.rotation);
        yield return new WaitForSeconds(2);
        Instantiate(Robozão, spawnpoint3.transform.position, spawnpoint3.transform.rotation);
                yield return new WaitForSeconds(2);
        Instantiate(Robozão, spawnpoint2.transform.position, spawnpoint3.transform.rotation);
                yield return new WaitForSeconds(2);
        Instantiate(Robozão, spawnpoint3.transform.position, spawnpoint3.transform.rotation);
                        yield return new WaitForSeconds(4);
        Instantiate(Robozão, spawnpoint4.transform.position, spawnpoint3.transform.rotation);
        

    }
                public IEnumerator Wave8()
    {
        yield return new WaitForSeconds(14f);
        Instantiate(Robozão, spawnpoint1.transform.position, spawnpoint1.transform.rotation);
        yield return new WaitForSeconds(2);
        Instantiate(Robozão, spawnpoint3.transform.position, spawnpoint3.transform.rotation);
                yield return new WaitForSeconds(2);
        Instantiate(Robozão, spawnpoint2.transform.position, spawnpoint3.transform.rotation);
                yield return new WaitForSeconds(2);
        Instantiate(Robozão, spawnpoint3.transform.position, spawnpoint3.transform.rotation);
                        yield return new WaitForSeconds(4);
        Instantiate(Robozão, spawnpoint4.transform.position, spawnpoint3.transform.rotation);
                yield return new WaitForSeconds(4);
        Instantiate(Robozão, spawnpoint3.transform.position, spawnpoint3.transform.rotation);
    }
                public IEnumerator Wave9()
    {
        yield return new WaitForSeconds(14f);
        Instantiate(Robozão, spawnpoint1.transform.position, spawnpoint1.transform.rotation);
        yield return new WaitForSeconds(2);
        Instantiate(Robozão, spawnpoint4.transform.position, spawnpoint3.transform.rotation);
                yield return new WaitForSeconds(2);
        Instantiate(Robozão, spawnpoint3.transform.position, spawnpoint3.transform.rotation);

    }
                public IEnumerator Wave10()
    {
        yield return new WaitForSeconds(14f);
        Instantiate(Robozão, spawnpoint4.transform.position, spawnpoint1.transform.rotation);
        yield return new WaitForSeconds(2);
        Instantiate(Robozão, spawnpoint1.transform.position, spawnpoint3.transform.rotation);
                yield return new WaitForSeconds(2);
        Instantiate(Robozão, spawnpoint2.transform.position, spawnpoint3.transform.rotation);
                yield return new WaitForSeconds(2);
        Instantiate(Robozão, spawnpoint3.transform.position, spawnpoint3.transform.rotation);
        WaveFinal();
    }
}
