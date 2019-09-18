using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public bool despawns;

    public bool targetFound;
    public GameObject head;
    public GameObject target;

    public ParticleSystem gun1;
    public ParticleSystem gun2;

    bool dead;

    void Start()
    {
        health = maxHealth;
        gun1.gameObject.SetActive(false);
        gun2.gameObject.SetActive(false);
    }

    void Update()
    {
        if (targetFound)
        {
            head.transform.LookAt(target.transform.position);
        }
        
        if(!dead && health <= 0)
        {
            Die();
        }


    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Shoot"))
        {
            GetHit();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player") || other.CompareTag("Ally")) && !targetFound)
        {
            TargetDetected();
            target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(target != null)
        if (other.name == target.name)
        {
            targetFound = false;
            gun1.gameObject.SetActive(false);
            gun2.gameObject.SetActive(false);
            if(despawns)
                Invoke("Despawn", 3f);
        }
    }

    public void TargetDetected()
    {
        targetFound = true;
        gun1.gameObject.SetActive(true);
        gun2.gameObject.SetActive(true);
        gun1.Play();
        gun2.Play();
    }

    public void GetHit()
    {
        health = health - 5;
    }

    public void Die()
    {
        Invoke("Despawn", 3f);
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }

}
