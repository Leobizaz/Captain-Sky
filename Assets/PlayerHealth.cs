using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float invincibilityTime;
    public Pause pauseScreen;
    public static bool dead;
    public ParticleSystem collectVidaFX;
    public Image HPBar;
    bool hittable;
    bool once;

    private void Start()
    {
        currentHealth = maxHealth;
        Invoke("GetHittable", 2f);
    }

    void GetHittable()
    {
        hittable = true;
    }

    private void Update()
    {
        if (currentHealth > maxHealth) currentHealth = maxHealth;

        if (currentHealth <= 0 && !dead && hittable)
        {
            Death();
        }



        UpdateHPSlider();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy") && hittable)
        {
            Debug.Log("Took a hit");
            hittable = false;
            Invoke("HitCooldown", invincibilityTime);
            currentHealth = currentHealth - 5f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickupVida") && !once)
        {
            once = true;
            Debug.Log("PEGO");
            Destroy(other.gameObject);
            //spawnfx
            Instantiate(collectVidaFX, other.transform.position, Quaternion.identity);
            currentHealth = currentHealth + 25f;
            Invoke("ResetPickupCooldown", 1f);
        }
    }

    void ResetPickupCooldown()
    {
        once = false;
    }

    void HitCooldown()
    {
        hittable = true;
    }

    void UpdateHPSlider()
    {
        //HPBar.DOValue((currentHealth / 100), 0.5f);
        HPBar.DOFillAmount((currentHealth / 100), 1.0f);
    }

    void Death()
    {
        Debug.Log("Player has been killed");
        dead = true;
        //insira animação de morte aki
        Invoke("OpenDeathMenu", 2f);
    }

    void OpenDeathMenu()
    {
        hittable = false;
        pauseScreen.LoadGameOverMenu();
    }
}
