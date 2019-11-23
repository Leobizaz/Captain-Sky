using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGun : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public bool vulnerable;
    public bool destroyed;
    public ParticleSystem particleFX;
    Animator anim;
    bool cooldown;

    private void Start()
    {
        anim = GetComponent<Animator>();
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
        anim.Play("GunIn");
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
