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

    public GameObject eye;
    public GameObject target;
    bool aiming;
    bool dead;
    public GameObject laserbeam;
    public ParticleSystem laserBall;
    public GameObject charging;

    DoCameraShake cameraShake;
    Coroutine fireLaser;
    AudioSource audio;

    bool once;
    bool gunsDestroyed;
    bool onceGuns;

    public BossWeakpoint olhoPrincipal;
    public BossWeakpoint turbinaTraseira;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        bossAnimator = GetComponent<Animator>();
        cameraShake = GameObject.Find("Game Manager").GetComponent<DoCameraShake>();
        Invoke("GunsPhase", 12f);
        GameObject.Find("Music").GetComponent<MusicController>().ChangeMusic(5, 5);
    }

    private void Update()
    {
        if(!dead)
            bossDolly.m_Position = playerDolly.m_Position + 200f;

        if (aiming)
        {
            eye.transform.LookAt(target.transform.position);
        }

        if(gun1.destroyed && gun2.destroyed && gun3.destroyed && gun4.destroyed && !gunsDestroyed)
        {
            gunsDestroyed = true;
            LaserPhase();
        }



        if(olhoPrincipal.destroyed && !once)
        {
            once = true;
            Death();
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
            yield return new WaitForSeconds(2);
            if (once)
            {
                yield break;
            }
            eyelidsAnimator.Play("EyelidOpen");
            charging.SetActive(true);
            aiming = true;
            olhoPrincipal.vulnerable = true;

            yield return new WaitForSeconds(5);
            if (once)
            {
                yield break;
            }
            aiming = false;
            yield return new WaitForSeconds(1);
            laserBall.Play();
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

    public void Death()
    {
        charging.SetActive(false);
        laserbeam.SetActive(false);
        bossAnimator.Play("Death");
        GameObject.Find("Music").GetComponent<MusicController>().ChangeMusic(1, 5);
        gunsAnimator.Play("LaserDestroyed");
        eyelidsAnimator.Play("EyelidClose");
        StopCoroutine(fireLaser);
    }

    public void HitGround()
    {
        audio.Play();
        cameraShake.ShakeAmplitude = 5;
        cameraShake.shakeElapsedTime = 2;
        dead = true;
    }




}
