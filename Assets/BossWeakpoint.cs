using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BossWeakpoint : MonoBehaviour
{
    public GameObject bossbody;
    public float maxHealth;
    public float health;
    public bool vulnerable;
    public bool destroyed;
    public ParticleSystem particleFX;
    bool cooldown;
    bool hitcooldown;

    public BossCore bossCore;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if (vulnerable)
        {
            gameObject.tag = "Enemy";
        }
        else
        {
            gameObject.tag = "Untagged";
        }



        if (!destroyed && health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Sendplayer.bossFightended = true;
        particleFX.Play();
        vulnerable = false;
        destroyed = true;
        gameObject.tag = "Untagged";
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Shoot" && vulnerable && !destroyed)
        {
            if (!hitcooldown)
            {
                hitcooldown = true;
                bossbody.transform.DOShakePosition(0.3f, 2);
                Invoke("ResetHitCooldown", 0.3f);
            }
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

    public void ResetHitCooldown()
    {
        hitcooldown = false;
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
