using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBehaviour : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float spawnTime;
    bool spawned;
    public GameObject explosionFX;
    public ParticleSystem gun1;
    public ParticleSystem gun2;

    private void Start()
    {
        currentHealth = maxHealth;
        Invoke("Spawn", spawnTime);
    }

    void Spawn()
    {
        spawned = true;
        gun1.Play();
        gun2.Play();
        //Invoke("StartShooting", Random.Range(0.1f, 2f));
    }

    void StartShooting()
    {
        StartCoroutine(threeBurst());
    }

    private void Update()
    {
        if (currentHealth <= 0) Death();



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
        Instantiate(explosionFX, transform.localPosition, transform.rotation);
        Destroy(this.gameObject);
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("COLIDIU");
        if (other.tag == "Shoot" && spawned)
        {
            //play hit fx
            currentHealth = currentHealth - 10f;
        }
    }
}
