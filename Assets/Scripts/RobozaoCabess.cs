using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobozaoCabess : MonoBehaviour
{
    public float maxHealth;
    public float health;

    public RobozaoHealth mainRobo;
    public Animator meshAnim;
    public GameObject laser;
    public GameObject fumaça;
    public Material newMaterial;
    public Renderer rend;
    public GameObject light;
    public GameObject spark;

    public AudioClip[] audios;
    AudioSource audio;
    bool died;

    private void Start()
    {
        health = maxHealth;
        audio = GetComponent<AudioSource>();
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
            //audio.PlayOneShot(audios[Random.Range(0, audios.Length)]);
            health = health - 10f;
        }
    }

    public void GetKilled()
    {
        died = true;
        meshAnim.Play("Hit");
        spark.SetActive(true);
        light.SetActive(false);
        fumaça.SetActive(true);
        mainRobo.cabessa_destroy = true;
        if(laser.gameObject != null)
            //laser.SetActive(false);
        //playsound destroy
        gameObject.tag = "DeadEnemy";
        rend.material = newMaterial;
    }

}
