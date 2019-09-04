﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobozaoBrasso : MonoBehaviour
{
    public float maxHealth;
    public float health;

    public RobozaoHealth mainRobo;
    public GameObject fumaça;
    public Material newMaterial;
    public Renderer rend;
    public GameObject sparkleFX;
    public bool brassodireito;
    bool dead;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if (health <= 0 && !dead)
        {
            GetKilled();
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Shoot"))
        {
            health = health - 10f;
        }
    }

    void GetKilled()
    {
        dead = true;
        sparkleFX.SetActive(true);
        fumaça.SetActive(true);
        if (brassodireito)
            mainRobo.bracoDIR_destroy = true;
        else
            mainRobo.bracoESQ_destroy = true;
        //playsound destroy
        gameObject.tag = "DeadEnemy";
        rend.material = newMaterial;
    }
}