using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossGun : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public bool vulnerable;
    public bool destroyed;
    public ParticleSystem particleFX;
    public Animator anim;
    bool shoot;
    bool cooldown;
    public GameObject bossMesh;
    DoCameraShake cameraShake;
    public ParticleSystem gun;

    private void Start()
    {
        health = maxHealth;
        cameraShake = GameObject.Find("Game Manager").GetComponent<DoCameraShake>();
        StartCoroutine(Shooter());
    }

    private void Update()
    {
        if (vulnerable)
        {
            shoot = true;
            gameObject.tag = "Enemy";
        }
        else
        {
            shoot = false;
            gameObject.tag = "Untagged";
        }


        if (!destroyed && health <= 0)
        {
            Death();
        }
    }

    IEnumerator Shooter()
    {
        while (true)
        {
            if (shoot)
            {
                gun.Play();
            }
            yield return new WaitForSeconds(Random.Range(3,6));
        }
    }

    public void Death()
    {
        //bossMesh.transform.DOShakePosition(1, 10, 10, 90);
        cameraShake.ShakeAmplitude = 1;
        cameraShake.shakeElapsedTime = 1;
        anim.Play("GunIn");
        anim.gameObject.SetActive(false);
        particleFX.Play();
        vulnerable = false;
        destroyed = true;
        gameObject.tag = "Untagged";
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Shoot" && vulnerable)
        {
            health = health - 10f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "LaserPlayer" && !cooldown && vulnerable)
        {
            // Debug.Log("Cuu");
            LaserHit();
        }
    }

    public void LaserHit()
    {
        health = health - 60f;
        cooldown = true;
        Invoke("ResetCooldown", 2f);
    }

    public void ResetCooldown()
    {
        cooldown = false;
    }






}
