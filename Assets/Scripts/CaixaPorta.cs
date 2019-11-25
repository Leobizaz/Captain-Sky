using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaixaPorta : MonoBehaviour
{
    public PortaHexagonal porta;
    public float maxHealth;
    public float health;
    public ParticleSystem particulaFX;
    bool once;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        if(health <= 0 && !once)
        {
            once = true;
            Destroy();
        }
    }

    private void OnParticleCollision(GameObject other) 
    {
        if(other.tag == "Shoot" && !once)
        {
            health = health - 10;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "PlayerLaser")
        {
            Destroy();
        }
    }

    void Destroy()
    {
        this.gameObject.tag = "Untagged";
        porta.OpenDoor();
        particulaFX.Play();
        //Destroy(gameObject);

    }

}
