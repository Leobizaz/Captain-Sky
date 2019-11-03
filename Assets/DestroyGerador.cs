using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGerador : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    bool once;
    public GameObject explosionFX;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0 && !once)
        {
            once = true;
            Ded();
        }
    }

    public void Ded()
    {
        GameObject instance = Instantiate(explosionFX, transform.position, Quaternion.identity);
        instance.transform.localScale = new Vector3(25, 25, 25);
        Ato3_Objetivo2.geradores_restantes -= 1;
        GetComponent<AudioSource>().Play();
        Destroy(gameObject);
    }
    private void OnParticleCollision(GameObject other)
    {
        //Debug.Log("COLIDIU");
        if (other.tag == "Shoot")
        {
            currentHealth = currentHealth - 10;
        }
    }

}
