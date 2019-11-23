using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeakpoint : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public bool vulnerable;
    public bool destroyed;
    public ParticleSystem particleFX;
    bool cooldown;

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
