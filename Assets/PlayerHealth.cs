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

    public Slider HPBar;
    bool hittable;

    private void Start()
    {
        hittable = true;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0 && !dead)
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
            currentHealth = currentHealth - 10f;
        }
    }

    void HitCooldown()
    {
        hittable = true;
    }

    void UpdateHPSlider()
    {
        HPBar.DOValue((currentHealth / 100), 0.5f);
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
        pauseScreen.LoadGameOverMenu();
    }
}
