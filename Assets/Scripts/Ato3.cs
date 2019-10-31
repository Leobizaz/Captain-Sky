using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ato3 : MonoBehaviour
{
    public static int aveCountAto3;
    bool spawning;
    public bool startEnemyAI;
    public bool startWithFog;
    public static int ato3_passagem;

    public GameObject aves_SP1;
    public GameObject waymaster_sp1;
    public GameObject aves_SP2;
    public GameObject waymaster_sp2;
    public GameObject aves_SP3;
    public GameObject waymaster_sp3;
    public GameObject aves_SP4;
    public GameObject waymaster_sp4;

    public GameObject ave_prefab;


    void Start()
    {
        if (startWithFog)
        {
            RenderSettings.fog = true;
        }
            
    }

    void Update()
    {
        if (startEnemyAI)
        {
            if (aveCountAto3 < 20 && !spawning)
            {
                StartCoroutine(Spawner());
            }

            if (aveCountAto3 >= 20)
            {
                spawning = false;
                StopCoroutine(Spawner());
            }
        }
    }

    IEnumerator Spawner()
    {
        spawning = true;
        while (true)
        {
            GameObject randomSP = aves_SP1;
            GameObject randomWM = waymaster_sp1;
            int i = Random.Range(0, 3);
            switch (i)
            {
                case 0:
                    randomSP = aves_SP1;
                    randomWM = waymaster_sp1;
                    break;
                case 1:
                    randomSP = aves_SP2;
                    randomWM = waymaster_sp2;
                    break;
                case 2:
                    randomSP = aves_SP3;
                    randomWM = waymaster_sp3;
                    break;
                case 3:
                    randomSP = aves_SP4;
                    randomWM = waymaster_sp4;
                    break;
            }

            GameObject instanced_Ave = Instantiate(ave_prefab, randomSP.transform.position, randomSP.transform.rotation);
            //instanced_Ave.GetComponent<AveIA>().wayfather = randomWM;
            yield return new WaitForSeconds(3);

        }
    }
}
