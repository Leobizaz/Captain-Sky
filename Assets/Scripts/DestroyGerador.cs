using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGerador : MonoBehaviour
{
    public GameObject normalGenerator;
    public GameObject destroyedGenerator;
    public Renderer mat1;
    public Renderer mat2;
    public Material red;
    public Material amarelo;
    public float maxHealth;
    public float currentHealth;
    bool once;
    bool ded;
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

        if(Ato3_Objetivo2.geradores_restantes < 3 && !ded)
        {
            mat1.material = amarelo;
            mat2.material = amarelo;
        }

    }

    public void Ded()
    {
        ded = true;
        GameObject instance = Instantiate(explosionFX, transform.position, Quaternion.identity);
        instance.transform.localScale = new Vector3(35, 35, 35);
        gameObject.tag = "Untagged";
        Ato3_Objetivo2.geradores_restantes -= 1;
        GetComponent<AudioSource>().Play();
        Destroy(normalGenerator);
        mat1.material = red;
        mat2.material = red;
        destroyedGenerator.SetActive(true);
        //Destroy(gameObject);
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
