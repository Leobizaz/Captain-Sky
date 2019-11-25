using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableBuilding : MonoBehaviour
{
    public float health;
    public float maxHealth;
    bool dead;
    public int importance;
    public GameObject mesh;
    public GameObject destroyedMesh;
    public GameObject particles1;
    public GameObject particles2;
    public GameObject explosion;
    public GameObject mapIndicator;
    bool once;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if(health <= 0 && !dead)
        {
            dead = true;
        }

        if(health <= maxHealth / 2)
        {
            particles1.SetActive(true);
        }
        if(health <= maxHealth / 4)
        {
            particles2.SetActive(true);
        }

        if (dead && !once)
        {
            once = true;
            WaveSystem.buildingsDestroyed++;
            gameObject.tag = "Untagged";
            //explosion.SetActive(true);
            mesh.SetActive(false);
            destroyedMesh.SetActive(true);
            mapIndicator.SetActive(false);
        }
    }

    public void GetHit()
    {
        if(!Cheats.CHEAT_BaseIndestrutivel)
            health = health - 20;
    }



}

