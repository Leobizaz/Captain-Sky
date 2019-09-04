using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobozaoCabess : MonoBehaviour
{
    public float maxHealth;
    public float health;

    public RobozaoHealth mainRobo;
    public GameObject laser;
    public GameObject fumaça;
    public Material newMaterial;
    public Renderer rend;
    public GameObject light;
    public GameObject spark;
    bool died;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if(health <= 0 && !died)
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
        died = true;
        spark.SetActive(true);
        light.SetActive(false);
        fumaça.SetActive(true);
        mainRobo.cabessa_destroy = true;
        if(laser.gameObject != null)
            laser.SetActive(false);
        //playsound destroy
        gameObject.tag = "DeadEnemy";
        rend.material = newMaterial;
    }

}
