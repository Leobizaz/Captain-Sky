using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossCore : MonoBehaviour
{

    public CinemachineDollyCart bossDolly;
    public CinemachineDollyCart playerDolly;

    private Animator bossAnimator;
    public Animator gunsAnimator;
    public Animator eyelidsAnimator;

    public BossGun gun1;
    public BossGun gun2;
    public BossGun gun3;
    public BossGun gun4;

    public GameObject laserbeam;
    public GameObject charging;

    DoCameraShake cameraShake;
    Coroutine fireLaser;

    bool once;
    bool onceGuns;

    public BossWeakpoint olhoPrincipal;
    public BossWeakpoint turbinaTraseira;

    private void Start()
    {
        cameraShake = GameObject.Find("Game Manager").GetComponent<DoCameraShake>();
        Invoke("GunsPhase", 12f);
    }

    private void Update()
    {
        bossDolly.m_Position = playerDolly.m_Position + 200f;



        if(olhoPrincipal.destroyed && !once)
        {
            once = true;
            CancelLaser();
        }
    }


    public void LaserPhase()
    {
        fireLaser = StartCoroutine(FireLaser());
    }

    public void GunsPhase()
    {
        if (!onceGuns)
        {
            onceGuns = true;
            gunsAnimator.Play("GunsOut");
            gun1.vulnerable = true;
            gun2.vulnerable = true;
            gun3.vulnerable = true;
            gun4.vulnerable = true;
        }
    }



    IEnumerator FireLaser()
    {
        while (true)
        {
            yield return new WaitForSeconds(8);
            if (once)
            {
                yield break;
            }
            eyelidsAnimator.Play("EyelidOpen");
            charging.SetActive(true);
            olhoPrincipal.vulnerable = true;
            yield return new WaitForSeconds(5);
            if (once)
            {
                yield break;
            }
            charging.SetActive(false);
            cameraShake.ShakeAmplitude = 5;
            cameraShake.shakeElapsedTime = 4f;
            laserbeam.SetActive(true);
            yield return new WaitForSeconds(4);
            if (once)
            {
                yield break;
            }
            laserbeam.SetActive(false);
            yield return new WaitForSeconds(5);
            if (once)
            {
                yield break;
            }
            eyelidsAnimator.Play("EyelidClose");
            olhoPrincipal.vulnerable = false;

        }
    }

    public void CancelLaser()
    {
        eyelidsAnimator.Play("EyelidClose");
        StopCoroutine(fireLaser);
    }


}
