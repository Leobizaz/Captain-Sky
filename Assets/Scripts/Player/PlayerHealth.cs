using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealth : MonoBehaviour
{
    public Color Red = Color.red;
    public Color Green = Color.green;
    public float maxHealth;
    public float currentHealth;
    public float invincibilityTime;
    public Pause pauseScreen;
    public static bool dead;
    public ParticleSystem collectVidaFX;
    public Image HPBar;
    bool hittable;
    bool once;
    public GameObject explosionFX;
    public AudioSource audio;
    public AudioClip[] audios;
    public GameObject playerMesh;

    public Image Feedback;
    private Animation anim;


    private void Start()
    {
        currentHealth = maxHealth;
        Invoke("GetHittable", 2f);
        anim = Feedback.GetComponent<Animation>();
       // audio = GetComponent<AudioSource>();
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

        if (Input.GetKeyDown(KeyCode.F))
        {
            Death();
        }

        UpdateHPSlider();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy") && hittable && !Pause.victory)
        {
            Debug.Log("Took a hit");
            audio.pitch = 1f;
            anim.Play();
            Feedback.DOColor(Red,0.1f);
            audio.PlayOneShot(audios[Random.Range(0, audios.Length)]);
            //transform.DOShakePosition(0.27f,1.4f,16,10,false,false);
            transform.DOShakeRotation(0.25f, 1f, 10, 10, false);
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
        playerMesh.transform.DOLocalRotate(new Vector3(90, 0, 90), 7f, RotateMode.LocalAxisAdd);
        playerMesh.transform.DOLocalMoveY(-45f,3.5f, true);
        Instantiate(explosionFX, transform.position, transform.localRotation);
        Invoke("OpenDeathMenu", 3.6f);
    }

    void OpenDeathMenu()
    {
        hittable = false;
        pauseScreen.LoadGameOverMenu();
    }
}
