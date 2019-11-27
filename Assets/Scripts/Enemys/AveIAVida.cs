using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AveIAVida : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float fakeHealth;
    bool died;

    public GameObject explosionFX;
    public GameObject smokeFX;
    public GameObject model;

    public AudioClip[] audios;
    public AudioClip explosionSFX;
    AudioSource audio;
   // Rigidbody rb;

    private void Start()
    {
      //  rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        fakeHealth = maxHealth;
    }

    private void Update()
    {
        if (!died)
        {
            if (fakeHealth <= 0) FakeDeath();

            if (currentHealth <= 0) Death();
        }
    }

    void FakeDeath()
    {
        Instantiate(explosionFX, model.transform.position, model.transform.localRotation);
        audio.pitch = Random.Range(0.8f, 1.2f);
        audio.PlayOneShot(explosionSFX);
        gameObject.tag = "DeadEnemy";
        //rb.isKinematic = false;
        //smokeFX.SetActive(true);

        died = true;
      //  rb.useGravity = true;
        GetComponent<AveNovaIA>().enabled = false;
        model.transform.DOLocalRotate(new Vector3(360, 0, 0), 10f, RotateMode.LocalAxisAdd);
        Invoke("Despawn", 10f);
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
        //rb.isKinematic = false;
        //smokeFX.SetActive(true);

        died = true;
      //  rb.useGravity = true;
        GetComponent<AveNovaIA>().enabled = false;
        model.transform.DOLocalRotate(new Vector3(360, 0, 0), 10f, RotateMode.LocalAxisAdd);
        Invoke("Despawn", 1f);
    }

    private void OnParticleCollision(GameObject other)
    {
        //Debug.Log("COLIDIU");
        if (other.tag == "Shoot")
        {
            //play hit fx
            audio.pitch = 0.7f;
            audio.PlayOneShot(audios[Random.Range(0, audios.Length)]);


            currentHealth = currentHealth - 10f;
        }

        if (other.tag == "Ally")
        {
            audio.pitch = 0.7f;
            audio.PlayOneShot(audios[Random.Range(0, audios.Length)]);


            fakeHealth = fakeHealth - 10f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("LaserPlayer"))
        {
            currentHealth = currentHealth - 100f;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("LaserPlayer"))
        {
            currentHealth = currentHealth - 100f;
        }
    }

    public void Despawn()
    {
        Ato3.aveCountAto3--;
        WaveSystem.aveCountAto2--;
        Destroy(this.gameObject);
    }


}
