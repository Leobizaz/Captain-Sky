using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobozaoBrasso : MonoBehaviour
{
    public float maxHealth;
    public float health;

    public RobozaoHealth mainRobo;
    public GameObject fumaça;
    public Material newMaterial;
    //public Renderer rend;
    public GameObject sparkleFX;
    public AudioClip[] audios;
    AudioSource audio;
    public bool brassodireito;
    public Animator meshAnim;
    bool dead;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
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
            //audio.PlayOneShot(audios[Random.Range(0, audios.Length)]);
            health = health - 10f;
        }
    }

    void GetKilled()
    {
        dead = true;
        meshAnim.Play("Hit");
        sparkleFX.SetActive(true);
        fumaça.SetActive(true);
        if (brassodireito)
            mainRobo.bracoDIR_destroy = true;
        else
            mainRobo.bracoESQ_destroy = true;
        //playsound destroy
        gameObject.tag = "DeadEnemy";
        //rend.material = newMaterial;
    }
}
