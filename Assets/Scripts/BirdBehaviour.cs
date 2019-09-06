using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BirdBehaviour : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float spawnTime;
    bool spawned;
    public GameObject explosionFX;
    public ParticleSystem gun1;
    public ParticleSystem gun2;
    public GameObject model;
    private BoxCollider col;
    public bool dontshoot;
    public AudioClip[] audios;
    public AudioClip explosionSFX;
    bool tooClose;
    public bool cantGoDownOrLeftOrRight;
    AudioSource audio;

    Rigidbody rb;
    bool died;
    public GameObject smoke;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
        currentHealth = maxHealth;
        Invoke("Spawn", spawnTime);
        col.enabled = false;
    }

    void Spawn()
    {
        spawned = true;
        if (!dontshoot)
        {
            gun1.Play();
            gun2.Play();
        }
        col.enabled = true;
        //Invoke("StartShooting", Random.Range(0.1f, 2f));
    }

    void StartShooting()
    {
        StartCoroutine(threeBurst());
    }

    private void Update()
    {
        if (currentHealth <= 0 && spawned) Death();


    }

    IEnumerator threeBurst()
    {
        while (true)
        {
            gun1.Play();
            gun2.Play();
            yield return new WaitForSeconds(0.3f);
            gun1.Play();
            gun2.Play();
            yield return new WaitForSeconds(0.3f);
            gun1.Play();
            gun2.Play();
            yield return new WaitForSeconds(0.3f);
            gun1.Stop();
            gun2.Stop();


            yield return new WaitForSeconds(4f);
        }
    }



    void Death()
    {
        if (!died)
        {
            ScoreSystem.currentScore += 1000f;
            GameObject.Find("Game Manager").GetComponent<ScoreSystem>().UpdateScore();
            Instantiate(explosionFX, model.transform.position, model.transform.localRotation);
            audio.pitch = Random.Range(0.8f, 1.2f);
            audio.PlayOneShot(explosionSFX);
            gameObject.tag = "DeadEnemy";
            ScoreSystem.enemysKill++;
        }
        rb.isKinematic = false;
        smoke.SetActive(true);

        died = true;
        rb.useGravity = true;
        gun1.Stop();
        gun2.Stop();
        GetComponent<BirdForward>().enabled = false;
        model.transform.DOLocalRotate(new Vector3(360, 0, 0), 10f, RotateMode.LocalAxisAdd);
        Invoke("Despawn", 10f);
    }

    public void Despawn()
    {
        Destroy(this.gameObject);
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("COLIDIU");
        if (other.tag == "Shoot" && spawned)
        {
            //play hit fx
            audio.pitch = 0.7f;
            audio.PlayOneShot(audios[Random.Range(0, audios.Length)]);


            currentHealth = currentHealth - 10f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DespawnWall"))
        {
            //Destroy(gameObject);
        }
    }
}
